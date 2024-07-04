using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryItemUI : MonoBehaviour
{
    public Image itemImage; // Image component for the item icon
    public TMP_Text itemCountText; // Text component for item count

    private Item item;

    public void SetItem(Item newItem)
    {
        item = newItem;
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