using UnityEngine;

public class UIPanelManager : MonoBehaviour
{
    [SerializeField] GameObject characterPanel;
    [SerializeField] GameObject inventoryPanel;
    [SerializeField] GameObject questsPanel;


    void Update()
    {
        if(Input.GetKeyDown(KeyCode.I)) ToggleInventory();
        if(Input.GetKeyDown(KeyCode.C)) ToggleCharacter();
        if(Input.GetKeyDown(KeyCode.Q)) ToggleQuests();
        
    }

    private void ToggleInventory()
    {
        inventoryPanel.SetActive(!inventoryPanel.activeInHierarchy);
    }
    
    private void ToggleCharacter()
    {
        characterPanel.SetActive(!characterPanel.activeInHierarchy);
        questsPanel.SetActive(false);
    }
    
    private void ToggleQuests()
    {
        questsPanel.SetActive(!questsPanel.activeInHierarchy);
        characterPanel.SetActive(false);
    }
}
