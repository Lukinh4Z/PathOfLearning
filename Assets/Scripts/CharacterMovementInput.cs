using UnityEngine;

public class CharacterMovementInput : MonoBehaviour
{
    public MouseInput mouseInput;
    CharacterMovement characterMovement;

    private void Awake()
    {
        characterMovement = GetComponent<CharacterMovement>();
        //mouseInput = Camera.main.GetComponent<MouseInput>();

        mouseInput.OnLeftClickWithGameObject += OnInteractionClick;
    }

    private void OnInteractionClick(Vector3 clickPosition, GameObject _)
    {
        Debug.Log($"clicked on {clickPosition}");
        characterMovement.SetDestination(clickPosition);
    }
}
