using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyMovement : MonoBehaviour
{
    private NavMeshAgent agent;
    bool logOnce = false;
    public int numberOfAttackAnimations = 1;
    public Animator anim;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        
        if (anim)
            anim.SetLayerWeight(1, 0f);//Set full body layer to zero so that only arms and legs move.
    }


    void Update()
    {
        if (agent.navMeshOwner)
        {
            agent.SetDestination(GameManager.Instance.player.transform.position);
        }
        else if (!logOnce)
        {
            Debug.LogWarning("No navmesh owner");
            logOnce = true;
        }
        UpdateAnimator();
    }


    // called by enemy controller.
    public void OnFire()
    {
        if (!anim) return;
        int randomAttack = Random.Range(1, numberOfAttackAnimations);
        anim.CrossFade("Attack" + randomAttack.ToString(), 0.1f);
    }

    private void UpdateAnimator()
    {
        if (!anim) return;
        anim.SetFloat("forward", agent.desiredVelocity.magnitude);
        //anim.SetFloat("up", agent.desiredVelocity.y);
        anim.SetBool("grounded", true);
    }
}
