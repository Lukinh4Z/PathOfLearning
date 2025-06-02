using UnityEngine;
using UnityEngine.UI;

public class CharacterUI : MonoBehaviour
{
    [SerializeField] UIPoolBar poolBar;
    ValuePool lifePool;
    Character character;

    private void Awake()
    {
        character = GetComponent<Character>();
    }

    private void Update()
    {
        lifePool = character.lifePool;
        poolBar.Show(lifePool);
    }
}
