using UnityEngine;
using System.Collections;


public class EnemyController : MonoBehaviour
{
    [SerializeField] private RuntimeAnimatorController myController;
    public GameObject sprite;
    public Transform modelRoot;
    public Transform target;
    public int speed;
    public int damage;
    public Hittable hp;
    public HealthBar healthui;
    public bool dead;

    public string child;
    public int childNum;
    public string childWhen;
    public int myRand;

    public float last_attack;

    // Slow effect variables
    private bool isSlowed = false;
    private float slowFactor = 1f;
    private Coroutine slowCoroutine;
    public void Init(Enemy enemyInfo)
    {
        GameObject model = GameManager.Instance.enemySpriteManager.GetModel(enemyInfo.sprite);
        if (!model)
        {
            // use sprite if no 3D model avalible
            spriteRenderer.sprite = GameManager.Instance.enemySpriteManager.Get(enemyInfo.sprite);
        }
        else
        {
            sprite.SetActive(false);
            GameObject modelInstance = Instantiate(model, modelRoot);
            modelInstance.GetComponent<Animator>().applyRootMotion = false;
            modelInstance.transform.localPosition = Vector3.zero;
            modelInstance.transform.localRotation = Quaternion.identity;

            modelInstance.GetComponent<Animator>().runtimeAnimatorController = myController;
            enemyMovement = GetComponent<EnemyMovement>();
            enemyMovement.SetAnimator(modelInstance.GetComponent<Animator>());
        }
    }

    public SpriteRenderer spriteRenderer;
    // public GameObject model;
    EnemyMovement enemyMovement;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // enemyMovement = GetComponent<EnemyMovement>();
        target = GameManager.Instance.player.transform;
        hp.OnDeath += Die;
        EventBus.Instance.OnDamage += OnDamage;
        healthui.SetHealth(hp);
        myRand = UnityEngine.Random.Range(0, 314);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = target.position - transform.position;
        if (direction.magnitude < 2f)
        {
            DoAttack();
        }
        else
        {
            float currentSpeed = speed * slowFactor;
            GetComponent<Unit>().movement = direction.normalized * currentSpeed;
            //Unstuck();
        }
    }

    void DoAttack()
    {
        if (last_attack + 2 < Time.time)
        {
            last_attack = Time.time;
            Debug.Assert(target.gameObject.GetComponent<PlayerController>() != null);
            Debug.Assert(target.gameObject.GetComponent<PlayerController>().hp != null);
            target.gameObject.GetComponent<PlayerController>().hp.Damage(new Damage(damage, Damage.Type.PHYSICAL));
            if (TryGetComponent(out EnemyMovement m)) m.OnFire();
        }
    }

    public void ApplySlow(float duration, float factor)
    {
        if (slowCoroutine != null)
        {
            StopCoroutine(slowCoroutine);
        }

        if (!isSlowed || factor < slowFactor)
        {
            slowFactor = factor;
        }

        isSlowed = true;
        slowCoroutine = StartCoroutine(SlowEffect(duration));
    }

    private IEnumerator SlowEffect(float duration)
    {
        yield return new WaitForSeconds(duration);

        // Remove slow effect
        isSlowed = false;
        slowFactor = 1f;
        slowCoroutine = null;
    }

    public void Die()
    {
        if (dead)
            return;
        if (childWhen == "death")
            SpawnChildren();
        dead = true;
        GameManager.Instance.RemoveEnemy(gameObject);
        Destroy(gameObject);
    }

    public void OnDamage(Vector3 where, Damage dmg, Hittable target)
    {
        if (childWhen == "hit" && hp == target)
            SpawnChildren();
    }

    public void SpawnChildren()
    {
        if (string.IsNullOrEmpty(child))
            return;
        var spawner = FindFirstObjectByType<EnemySpawner>();
        var childToSpawn = DataLoader.Instance.FindEnemy(child);
        for (int i = 0; i < childNum; i++)
            spawner.SpawnEnemyAtPosition(childToSpawn, transform.position);
    }

    void Unstuck()
    {
        GetComponent<Collider2D>().enabled = false;
        Vector3 pos3 = GetComponent<Unit>().transform.position;
        Vector2 pos = new Vector2(pos3.x, pos3.y);
        Vector2 tmp0 = new Vector2(-0.45f, -0.45f);
        Vector2 tmp1 = new Vector2(-0.6f, -0.6f);
        //if (Physics2D.OverlapArea(pos+tmp,pos-tmp) != null)
        //    Debug.Log(GetComponent<Unit>().transform.position);
        if (Physics2D.OverlapArea(pos + tmp0, pos - tmp0) != null)
            for (int i = 0; (Physics2D.OverlapArea(pos + tmp1, pos - tmp1) != null) && (i < 300); i++)
            {
                float tempA = i + myRand;
                float tempX = Mathf.Cos(tempA) * i * 0.3f;
                float tempY = Mathf.Sin(tempA) * i * 0.3f;
                GetComponent<Unit>().transform.position = pos3 + new Vector3(tempX, tempY, 0);
                pos += new Vector2(tempX, tempY);
            }
        GetComponent<Collider2D>().enabled = true;
    }
}
