using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public GameObject itemPrefab; // Assign the InventoryItem prefab in the Inspector
    public Transform inventoryPanel; // Panel to hold inventory slots
    public int width = 4; // Width of the inventory grid
    public int height = 4; // Height of the inventory grid
    private InventorySlot[,] slots; // 2D array of inventory slots

    public void InitializeInventory()
    {
        slots = new InventorySlot[width, height];
        // Create slots and set up the grid
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                var slotObject = new GameObject($"Slot_{x}_{y}");
                var rectTransform = slotObject.AddComponent<RectTransform>();
                rectTransform.SetParent(inventoryPanel, false);
                rectTransform.sizeDelta = new Vector2(32, 32); // Adjust size as needed
                rectTransform.anchoredPosition = new Vector2(x * 32, y * 32); // Adjust position

                var slot = slotObject.AddComponent<InventorySlot>();
                slot.Initialize(x, y);
                slots[x, y] = slot;
            }
        }
    }

    public void AddItemToSlot(int x, int y, Item item)
    {
        if (x < width && y < height)
        {
            var slot = slots[x, y];
            if (slot != null)
            {
                slot.AddItem(item);
                SetUIItem(x, y, item); // Update UI to reflect item changes
            }
        }
    }

    public Item GetItem(int x, int y)
    {
        if (x < width && y < height)
        {
            var slot = slots[x, y];
            return slot?.GetItem();
        }
        return null;
    }

    public void SetUIItem(int x, int y, Item item)
    {
        var slot = slots[x, y];
        if (slot != null)
        {
            slot.SetItem(item);
        }
    }
}
