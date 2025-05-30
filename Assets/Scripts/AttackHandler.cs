using System;
using UnityEngine;

public class AttackHandler : MonoBehaviour
{
    Animator animator;
    CharacterMovement characterMovement;
    Character character;
    GameObject target;
    private bool isAttacking;

    [SerializeField]
    float attackRange = 1.0f;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        characterMovement = GetComponent<CharacterMovement>();  
        character = GetComponent<Character>();  

    }

    private void Update()
    {
        if (target != null)
        {
            ProcessAttack();
        }
    }

    private void ProcessAttack()
    {
        float distance = Vector3.Distance(target.transform.position, gameObject.transform.position);

        if (distance < attackRange)
        {
            characterMovement.Stop();
            animator.SetTrigger("attacking");

            int damage = character.TakeStats(Statistic.Damage).value;
            Character targetCharacter = target.GetComponent<Character>();
            targetCharacter.TakeDamage(damage);

            target = null;
        }
        else 
        { 
            characterMovement.SetDestination(target.transform.position);
        }
    }

    public void Attack(InteractableObject interactableObject)
    {
        target = interactableObject.gameObject;
    }
}
