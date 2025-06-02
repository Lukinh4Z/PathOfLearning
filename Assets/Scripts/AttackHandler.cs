using System;
using UnityEngine;
using UnityEngine.Rendering;

public class AttackHandler : MonoBehaviour
{
    Animator animator;
    CharacterMovement playerCharacterMovement;
    Character playerCharacter;
    Character targetCharacter;
    private bool isAttacking;

    [SerializeField]
    float attackRange = 1.0f;
    [SerializeField]
    float defaultTimetoAttack = 2.0f;
    float attackTimer;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        playerCharacterMovement = GetComponent<CharacterMovement>();  
        playerCharacter = GetComponent<Character>();

        //attackTimer = (defaultTimetoAttack / GetAttackSpeed());
    }

    private void Update()
    {
        if (targetCharacter != null)
        {
            ProcessAttack();
        }

        AttackTimerTick();
    }

    private void AttackTimerTick()
    {
        if (attackTimer > 0f)
        {
            attackTimer -= Time.deltaTime;
        }
    }

    private void ProcessAttack()
    {
        float distance = Vector3.Distance(targetCharacter.transform.position, gameObject.transform.position);

        if (distance < attackRange)
        {
            if(attackTimer > 0f) { return; }
            attackTimer = (defaultTimetoAttack / GetAttackSpeed());

            playerCharacterMovement.Stop();
            animator.SetTrigger("attacking");

            int damage = playerCharacter.TakeStats(Statistic.Damage).int_value;
            targetCharacter.TakeDamage(damage);

            this.targetCharacter = null;
        }
        else 
        { 
            playerCharacterMovement.SetDestination(targetCharacter.transform.position);
        }
    }

    public void Attack(Character character)
    {
        targetCharacter = character;
    }

    float GetAttackSpeed() 
    {
        float attackSpeed = playerCharacter.TakeStats(Statistic.AttackSpeed).float_value;
        return attackSpeed; 
    }
}
