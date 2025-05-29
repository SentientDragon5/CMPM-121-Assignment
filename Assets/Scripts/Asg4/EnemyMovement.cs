using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyMovement : MonoBehaviour
{
    private NavMeshAgent agent;
    private CharacterController characterController;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }


    void Update()
    {
        agent.SetDestination(GameManager.Instance.player.transform.position);
    }
}
