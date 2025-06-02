using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    [SerializeField] string sceneName;
    public void Interact()
    {
        if (sceneName != "")
        {
            GameSceneManager.instance.StartTransition(sceneName);
        }
    }
}
