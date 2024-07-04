using System;
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
                      
            // Instantiate the CoinFlip and set it up with InventoryManager
            var coinFlip = Instantiate(Resources.Load<GameObject>($"HUD/CoinFlip_Loot"),
                GameManager.HUD, false);
            var coinFlips = coinFlip.GetComponentsInChildren<Transform>(); // Get all coin flip transforms

            var coinRenderer = coinFlips[coinFlips.Length - 1]; // Get the last child transform
            coinRenderer.SetParent(null);

            coinFlip.GetComponent<CoinFlip>().Initialize(GameManager.inventoryManager);
            coinFlip.GetComponent<CoinFlip>().FlipCoin(lootTable);
        }
    }

    public void ShowText(bool show)
    {
        if (interactText == "")
            return;

        if (show)
        {
            if (overheadText == null)
            {
                overheadText = Instantiate(Resources.Load<GameObject>($"HUD/OverheadText"),
                    transform.position + Vector3.up * textHeight, Quaternion.identity);
                overheadText.transform.GetChild(0).GetComponent<TMP_Text>().text = interactText;
            }
        }
        else if (overheadText != null)
            Destroy(overheadText);
    }
}