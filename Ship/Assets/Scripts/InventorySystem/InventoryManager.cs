using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public GameObject itemPrefab; // Assign the InventoryItem prefab in the Inspector
    public Transform[,] slots; // 2D array for slots in the grid-based inventory
    public int width = 5; // Width of the grid
    public int height = 5; // Height of the grid

    public void InitializeInventory()
    {
        slots = new Transform[width, height];
        // Instantiate the inventory grid and assign slot positions
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                // Create a new slot (could be a UI element or just a placeholder for positions)
                GameObject slot = new GameObject($"Slot_{x}_{y}");
                slot.transform.SetParent(transform);
                slot.transform.localPosition = new Vector3(x * 32, y * 32, 0); // 32 is the assumed slot size
                slots[x, y] = slot.transform;
            }
        }
    }

    public void AddItemToSlot(int x, int y, Loot lootData)
    {
        if (x < width && y < height)
        {
            GameObject newItem = Instantiate(itemPrefab, slots[x, y]);
            newItem.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;

            // Set the loot data on the new item
            LootHolder lootHolder = newItem.GetComponent<LootHolder>();
            lootHolder.loot = lootData;

            // Update the item's sprite
            newItem.GetComponent<UnityEngine.UI.Image>().sprite = lootData.lootSprite;

            // Adjust size and layout based on the loot's shape
            AdjustItemSize(newItem, lootData);
        }
    }

    private void AdjustItemSize(GameObject item, Loot lootData)
    {
        RectTransform rectTransform = item.GetComponent<RectTransform>();
        int[,] shape = lootData.GetShape();

        // Adjust size based on shape
        rectTransform.sizeDelta = new Vector2(shape.GetLength(1) * 32, shape.GetLength(0) * 32); // Assuming each slot is 32x32
    }
}
