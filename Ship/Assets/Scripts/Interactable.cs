using UnityEngine;
using TMPro;

public class Interactable : MonoBehaviour
{
    public delegate void Interact();
    public event Interact OnInteract;

    public string interactText;
    public float textHeight;

    private GameObject overheadText;

    private void Start()
    {
        GameManager.interactables.Add(this);
    }

    public void Trigger()
    {
        OnInteract?.Invoke();

        if (TryGetComponent(out LootTable lootTable))
        {
            GameManager.LockPlayer(true);

            // Instantiate the InventoryManager and setup the grid
            var inventoryManagerObject = Instantiate(Resources.Load<GameObject>($"HUD/InventoryManager"),
                GameManager.HUD, false);
            var inventoryManager = inventoryManagerObject.GetComponent<InventoryManager>();
            inventoryManager.InitializeInventory(); // Initialize the inventory grid

            // Instantiate the CoinFlip and set it up with InventoryManager
            var coinFlip = Instantiate(Resources.Load<GameObject>($"HUD/CoinFlip_Loot"),
                GameManager.HUD, false);
            var coinRenderers = coinFlip.transform.GetChild(coinFlip.transform.childCount - 1);
            coinRenderers.SetParent(null);

            coinFlip.GetComponent<CoinFlip>().Initialize(inventoryManager, coinRenderers);
        }
    }

    public void ShowText(bool show)
    {
        if (string.IsNullOrEmpty(interactText)) // Check for null or empty text
            return;

        if (show)
        {
            if (overheadText == null)
            {
                overheadText = Instantiate(Resources.Load<GameObject>($"HUD/OverheadText"),
                    transform.position + Vector3.up * textHeight, Quaternion.identity);
                var textMeshPro = overheadText.transform.GetChild(0).GetComponent<TMP_Text>();  // Get TMP_Text component
                if (textMeshPro != null)
                {
                    textMeshPro.text = interactText;
                }
            }
        }
        else if (overheadText != null)
            Destroy(overheadText);
    }
}