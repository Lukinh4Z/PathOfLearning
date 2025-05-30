using UnityEngine;

public class InteractInput : MonoBehaviour
{
    MouseInput mouseInput;

    [SerializeField]
    TMPro.TextMeshProUGUI textOnScreen;

    private void Awake()
    {
        mouseInput = Camera.main.GetComponent<MouseInput>();

        mouseInput.OnLeftClickWithGameObject += OnInteractionPerformed;
        mouseInput.OnHoverInteractable += IsHoveringInteractable;
    }

    private void OnInteractionPerformed(Vector3 position, GameObject gameObject)
    {
        InteractableObject interactableObject = gameObject.GetComponent<InteractableObject>();
        if (interactableObject != null)
        {
            interactableObject.Interact();
        }
    }

    private void IsHoveringInteractable(InteractableObject interactableObject)
    {
        if (interactableObject != null)
        {
            textOnScreen.text = interactableObject.gameObject.name;
            textOnScreen.gameObject.SetActive(true);
        }
        else
        {
            textOnScreen.text = "";
            textOnScreen.gameObject.SetActive(false);
        }

    }
}
