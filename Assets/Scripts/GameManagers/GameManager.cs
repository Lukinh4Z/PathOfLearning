using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameObject playerObject;

    private void Awake()
    {
        instance = this;
    }
}
