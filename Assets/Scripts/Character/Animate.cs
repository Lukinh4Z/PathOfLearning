using UnityEngine;
using UnityEngine.AI;

public class Animate : MonoBehaviour
{
    Animator _animator;
    NavMeshAgent _agent;
    Character _character;

    private void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
        _agent = GetComponent<NavMeshAgent>();
        _character = GetComponent<Character>();

        _character.OnDeath += PlayDeathAnimation;
    }

    private void Update()
    {
        float motion = _agent.velocity.magnitude;

        _animator.SetFloat("motion", motion);
    }

    private void PlayDeathAnimation()
    {
        _animator.SetTrigger("death");
    }
}
