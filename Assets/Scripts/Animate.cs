using UnityEngine;
using UnityEngine.AI;

public class Animate : MonoBehaviour
{
    Animator _animator;
    NavMeshAgent _agent;

    private void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
        _agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        float motion = _agent.velocity.magnitude;

        _animator.SetFloat("motion", motion);
    }
}
