using UnityEngine;

public class CharacterMovementInput : MonoBehaviour
{
    public MouseInput mouseInput;
    CharacterMovement characterMovement;

    private void Awake()
    {
        characterMovement = GetComponent<CharacterMovement>();
        //mouseInput = Camera.main.GetComponent<MouseInput>();

        mouseInput.OnInteractWithGameObject += OnInteractionClick;
    }

    private void OnInteractionClick(Vector3 clickPosition, GameObject gameObject)
    {
        //Debug.Log($"clicked on {clickPosition}");

        InteractableObject interactableObject = gameObject.GetComponent<InteractableObject>();
        if(interactableObject == null) characterMovement.SetDestination(clickPosition);
    }
}
