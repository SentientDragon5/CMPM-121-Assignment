using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{

    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float sprintMultiplier = 1.4f;
    public float jumpHeight = 2f;
    public float gravity = -20f;
    public float lookSensitivity = 1f;
    public Transform cameraTransform;

    private CharacterController controller;
    private Animator anim;
    public PlayerInput input;
    Unit unit;
    private float cameraPitch = 0f;
    private float verticalVelocity = 0f;
    Vector2 moveInput;
    public float fov = 60;
    public float sprintFovMult = 1.5f;
    public float aimFovMult = 0.25f;
    public float fovSmoothing = 2f;
    public float aimMoveMult = 0.5f;
    public bool Aiming => input!.actions["Aiming"].IsPressed();

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        anim = GetComponentInChildren<Animator>();
        unit = GetComponent<Unit>();

        anim.SetLayerWeight(1, 0f);//Set full body layer to zero so that only arms and legs move.
    }

    private void OnEnable()
    {
        if (!Cursor.visible) return;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        input.actions["Attack"].performed += _ => OnFire();
    }

    private void OnDisable()
    {
        if (Cursor.visible) return;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        if (input == null) return;
        input!.actions["Attack"].performed -= _ => OnFire();
    }

    private void Update()
    {
        if (GameManager.Instance.state != GameManager.GameState.COUNTDOWN && GameManager.Instance.state != GameManager.GameState.INWAVE){
            OnDisable();
            return;
        }
        OnEnable();
        UpdateMovement();
        UpdateCamera();
        UpdateAnimator();
    }

    void UpdateMovement()
    {
        // Sprint
        bool isSprinting = input.actions["Sprint"] != null && input.actions["Sprint"].ReadValue<float>() > 0.1f;

        float fovGoal = fov * (Aiming ? aimFovMult : isSprinting ? sprintFovMult : 1);
        Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, fovGoal, Time.deltaTime * fovSmoothing);

        float currentSpeed = isSprinting ? moveSpeed * sprintMultiplier : moveSpeed;
        currentSpeed = Aiming ? moveSpeed * aimMoveMult : currentSpeed;

        // Movement
        moveInput = input.actions["Move"].ReadValue<Vector2>();
        Vector3 move = transform.right * moveInput.x + transform.forward * moveInput.y;
        move.Normalize();

        // Jump & Gravity
        if (controller.isGrounded)
        {
            verticalVelocity = -2f; // Small downward force to keep grounded
            if (input.actions["Jump"] != null && input.actions["Jump"].triggered)
            {
                verticalVelocity = Mathf.Sqrt(jumpHeight * -2f * gravity);
            }
        }
        else
        {
            verticalVelocity += gravity * Time.deltaTime;
        }

        Vector3 velocity = move * currentSpeed + Vector3.up * verticalVelocity;
        Vector3 motion = velocity * Time.deltaTime;
        unit.RegisterMotion(motion);
        controller.Move(velocity * Time.deltaTime);
    }
    void UpdateCamera()
    {
        Vector2 lookInput = input.actions["Look"].ReadValue<Vector2>();
        float mouseX = lookInput.x * lookSensitivity;
        float mouseY = lookInput.y * lookSensitivity;

        cameraPitch -= mouseY;
        cameraPitch = Mathf.Clamp(cameraPitch, -90f, 90f);
        cameraTransform.localRotation = Quaternion.Euler(cameraPitch, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);
    }

    private void OnFire()
    {
        Debug.Log("Attack Spell!");
        int randomAttack = Random.Range(1, 2);
        anim.CrossFade("Attack" + randomAttack.ToString(), 0.1f);
        GetComponent<PlayerController>().OnAttack(null);
    }

    public float moveAnimSmoothTime = 10f;
    private void UpdateAnimator()
    {
        anim.SetFloat("forward", Mathf.Lerp(anim.GetFloat("forward"), moveInput.magnitude, Time.deltaTime * moveAnimSmoothTime));
        //anim.SetFloat("right", moveInput.x);
        anim.SetFloat("up", controller.velocity.y);
        anim.SetBool("grounded", controller.isGrounded);
    }
}