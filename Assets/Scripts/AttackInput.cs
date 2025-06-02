using UnityEngine;

public class AttackInput : MonoBehaviour
{
    AttackHandler _attackHandler;

    private void Awake()
    {
        MouseInput mouseInput = Camera.main.GetComponent<MouseInput>();
        mouseInput.OnInteractWithGameObject += AttackTarget;

        _attackHandler = GetComponent<AttackHandler>();
    }

    private void AttackTarget(Vector3 _, GameObject target)
    {
        Character targetCharacter = target.GetComponent<Character>();
        if(targetCharacter != null) _attackHandler.Attack(targetCharacter);
    }
}
