using UnityEngine;
using UnityEngine.AI;

public class CharacterMovement : MonoBehaviour
{
    NavMeshAgent agent;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    public void SetDestination(Vector3 destinatioPosition)
    {
        agent.isStopped = false;
        agent.SetDestination(destinatioPosition);
    }

    public void Stop()
    {
        agent.isStopped = true;
    }
}
