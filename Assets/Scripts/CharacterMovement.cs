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
        agent.SetDestination(destinatioPosition);
    }

}
