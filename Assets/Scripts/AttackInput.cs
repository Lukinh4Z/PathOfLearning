using UnityEngine;

public class AttackInput : MonoBehaviour
{
    [SerializeField]
    InteractInput _interactInput;

    AttackHandler _attackHandler;

    private void Awake()
    {
        MouseInput mouseInput = Camera.main.GetComponent<MouseInput>();
        mouseInput.OnInteractWithGameObject += AttackTarget;

        _attackHandler = GetComponent<AttackHandler>();
    }

    private void AttackTarget(Vector3 _, GameObject target)
    {
        InteractableObject interactableObject = target.GetComponent<InteractableObject>();
        if(interactableObject != null) _attackHandler.Attack(interactableObject);
    }
}
