using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventorySlot : MonoBehaviour
{
    public Image itemImage; // Image component for the item icon
    public TMP_Text itemCountText; // Text component for item count

    private Item item;
    private int x;
    private int y;

    public void Initialize(int x, int y)
    {
        this.x = x;
        this.y = y;
    }

    public void AddItem(Item newItem)
    {
        item = newItem;
        UpdateUI();
    }

    public Item GetItem()
    {
        return item;
    }

    public void SetItem(Item newItem)
    {
        item = newItem;
        UpdateUI();
    }

    private void UpdateUI()
    {
        if (item != null)
        {
            itemImage.sprite = item.loot.lootSprite;
            itemCountText.text = item.count.ToString();
        }
        else
        {
            itemImage.sprite = null;
            itemCountText.text = "";
        }
    }
}