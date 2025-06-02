using UnityEngine;
using UnityEngine.AI;

public class InteractInput : MonoBehaviour
{
    MouseInput mouseInput;

    [SerializeField]
    TMPro.TextMeshProUGUI textOnScreen;
    [SerializeField]
    UIPoolBar uiLifePoolBar;

    [HideInInspector]
    public InteractableObject hoveringObject;
    Character hoveringCharacter;

    //[SerializeField] NavMeshAgent playerAgent;

    private void Awake()
    {
        mouseInput = Camera.main.GetComponent<MouseInput>();

        //mouseInput.OnInteractWithGameObject += OnInteractionPerformed;
        mouseInput.OnHoverInteractable += IsHoveringInteractable;
    }

    /*private void OnInteractionPerformed(Vector3 position, GameObject gameObject)
    {
        InteractableObject interactableObject = gameObject.GetComponent<InteractableObject>();
        Character character = gameObject.GetComponent<Character>();
        if (interactableObject != null && character == null)
        {
            interactableObject.Interact();
            playerAgent.SetDestination(gameObject.transform.position);
        }
    }*/

    private void IsHoveringInteractable(InteractableObject interactableObject)
    {
        if (interactableObject != null)
        {
            textOnScreen.text = interactableObject.gameObject.name;
            textOnScreen.gameObject.SetActive(true);
            hoveringObject = interactableObject;
            hoveringCharacter = interactableObject.gameObject.GetComponent<Character>();
        }
        else
        {
            textOnScreen.text = "";
            textOnScreen.gameObject.SetActive(false);
            hoveringObject = null;
            hoveringCharacter = null;
        }

        UpdateHPBar();
    }

    private void UpdateHPBar()
    {
        if(hoveringCharacter != null)
        {
            uiLifePoolBar.Show(hoveringCharacter.lifePool);
        }
        else
        {
            uiLifePoolBar.Clear();
        }
    }
}
