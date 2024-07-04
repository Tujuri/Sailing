using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public Transform inventoryPanel; // The panel where inventory items are displayed
    public GameObject inventoryItemPrefab; // Prefab for inventory items

    private InventoryManager inventoryManager;

    public void Initialize(InventoryManager manager)
    {
        inventoryManager = manager;
        UpdateInventoryUI();
    }

    public void UpdateInventoryUI()
    {
        // Clear existing items in the UI
        foreach (Transform child in inventoryPanel)
        {
            Destroy(child.gameObject);
        }

        // Update the UI with current inventory items
        for (int x = 0; x < inventoryManager.width; x++)
        {
            for (int y = 0; y < inventoryManager.height; y++)
            {
                var item = inventoryManager.GetItem(x, y);
                if (item != null)
                {
                    var itemUI = Instantiate(inventoryItemPrefab, inventoryPanel).GetComponent<InventoryItemUI>();
                    itemUI.SetItem(item);
                }
            }
        }
    }
}