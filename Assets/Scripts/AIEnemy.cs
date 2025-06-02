using UnityEngine;
using UnityEngine.SearchService;

public class AIEnemy : MonoBehaviour
{
    AttackHandler attackHandler;
    [SerializeField] Character player;
    float attackTime = 4f;

    private void Awake()
    {
        attackHandler = GetComponent<AttackHandler>();
    }

    private void Start()
    {
        player = Object.FindFirstObjectByType<CharacterMovementInput>().gameObject.GetComponent<Character>();
    }

    private void Update()
    {
        attackTime -= Time.deltaTime;

        if(attackTime < 0)
        {
            attackTime = 4f;

            attackHandler.Attack(player);
        }
        
    }
}
