using UnityEngine;

public class CharacterMovementInput : MonoBehaviour
{
    public MouseInput mouseInput;
    CharacterMovement characterMovement;
    GameObject targetGameObject;
    public float interactRange = 2.0f;

    private void Awake()
    {
        characterMovement = GetComponent<CharacterMovement>();
        mouseInput = Camera.main.GetComponent<MouseInput>();

        mouseInput.OnInteractWithGameObject += OnInteractionClick;
    }

    public void OnInteractionClick(Vector3 clickPosition, GameObject gameObject)
    {
        InteractableObject interactableObject = gameObject.GetComponent<InteractableObject>();
        Character character = gameObject.GetComponent<Character>();
        if (interactableObject != null && character == null) 
        { 
            targetGameObject = gameObject;
            characterMovement.SetDestination(targetGameObject.transform.position);
        }
        else if (interactableObject == null && character == null)
        {
            characterMovement.SetDestination(clickPosition);
        }
    }

    private void Update()
    {
        if (targetGameObject != null)
        {
            CheckDistance();
        }
    }

    private void CheckDistance()
    {
        float distance = Vector3.Distance(targetGameObject.transform.position, gameObject.transform.position);

        if (distance < interactRange)
        {
            characterMovement.Stop();
            targetGameObject.GetComponent<InteractableObject>().Interact();
            targetGameObject = null;
        }
    }
}
