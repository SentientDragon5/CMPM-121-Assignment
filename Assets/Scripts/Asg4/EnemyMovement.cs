using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyMovement : MonoBehaviour
{
    private NavMeshAgent agent;
    bool logOnce = false;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
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
    }
}
