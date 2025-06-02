using System;
using UnityEngine;
using UnityEngine.AI;

public class CharacterMovement : MonoBehaviour
{
    NavMeshAgent agent;
    Character character;
    [SerializeField] float defaultMoveSpeed = 3.5f;
    StatsValue moveSpeedStat;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        character = GetComponent<Character>();
    }

    private void Start()
    {
        moveSpeedStat = character.TakeStats(Statistic.MoveSpeed);
    }

    private void Update()
    {
        float calcSpeed = moveSpeedStat.float_value * defaultMoveSpeed;

        if (calcSpeed != agent.speed) 
        { 
            agent.speed = calcSpeed;
        }
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
