using UnityEngine;
using UnityEngine.AI;

public class CharDefeatHandler : MonoBehaviour
{
    AIEnemy AIEnemy;
    Collider Collider;
    NavMeshAgent NavMeshAgent;
    Character _character;

    private void Awake()
    {
        AIEnemy = GetComponent<AIEnemy>();
        Collider = GetComponent<Collider>();
        NavMeshAgent = GetComponent<NavMeshAgent>();

        _character = GetComponent<Character>();
        _character.OnDeath += HandleDefeat;
    }

    private void HandleDefeat()
    {
        AIEnemy.enabled = false;
        Collider.enabled = false;

        NavMeshAgent.isStopped = true;
        NavMeshAgent.enabled = false;
    }
}
