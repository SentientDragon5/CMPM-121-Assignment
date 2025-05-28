using UnityEngine;
using System.Collections;


public class EnemyController : MonoBehaviour
{

    public Transform target;
    public int speed;
    public int damage;
    public Hittable hp;
    public HealthBar healthui;
    public bool dead;

    public float last_attack;

    // Slow effect variables
    private bool isSlowed = false;
    private float slowFactor = 1f; 
    private Coroutine slowCoroutine;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        target = GameManager.Instance.player.transform;
        hp.OnDeath += Die;
        healthui.SetHealth(hp);
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
        if (!dead)
        {
            dead = true;
            GameManager.Instance.RemoveEnemy(gameObject);
            Destroy(gameObject);
        }
    }
}
