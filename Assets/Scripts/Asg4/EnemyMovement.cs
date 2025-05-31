using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyMovement : MonoBehaviour
{
    private NavMeshAgent agent;

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
        else Debug.LogWarning("No navmesh owner");
    }
}
