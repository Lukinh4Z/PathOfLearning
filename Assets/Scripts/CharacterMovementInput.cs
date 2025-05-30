using UnityEngine;

public class CharacterMovementInput : MonoBehaviour
{
    public MouseInput mouseInput;
    CharacterMovement characterMovement;

    private void Awake()
    {
        characterMovement = GetComponent<CharacterMovement>();
        //mouseInput = Camera.main.GetComponent<MouseInput>();

        mouseInput.OnInteract += OnInteractionClick;
    }

    private void OnInteractionClick(Vector3 clickPosition)
    {
        Debug.Log($"clicked on {clickPosition}");
        characterMovement.SetDestination( clickPosition );
    }
}
