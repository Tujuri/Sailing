using System;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CoinFlip : MonoBehaviour
{
    public Transform[] coins; // Array of coin transforms for flipping animation

    private InventoryManager inventoryManager;
    private List<Transform> coinFlips = new(); // List of coin flip transforms

    public void Initialize(InventoryManager inventoryManager)
    {
        this.inventoryManager = inventoryManager;

        for (var i = 0; i < coins.Length; i++)
        {
            coinFlips.Add(coins[i].GetChild(0));
        }
    }

    public void FlipCoin(LootTable lootTable)
    {
        var lootOptions = lootTable.lootOptions;
        if (lootOptions.Length == 0) return; // No loot options available

        var chosenLootOption = ChooseLootOption(lootOptions);
        var item = CreateItemFromLoot(chosenLootOption);

        AddItemToInventory(item);

        var result = UnityEngine.Random.value > 0.5f;
        var coinFlip = coinFlips[Array.IndexOf(coins, coins[0])]; // Choose the first coin flip for demonstration

        const float duration = 0.6f;
        const int flips = 2;

        coinFlip.DOLocalMoveZ(0.8f, duration / 2).SetEase(Ease.OutQuad);
        coinFlip.DOLocalMoveZ(1, duration / 2).SetEase(Ease.InQuad).SetDelay(duration / 2);
        coinFlip.DOLocalRotate(new Vector3(0, 360 * flips, 0), duration, RotateMode.FastBeyond360)
            .SetEase(Ease.Linear).OnComplete(() =>
            {
                coinFlip.localRotation = Quaternion.Euler(new Vector3(0, result ? 0 : 180, 0));
                Debug.Log(result ? "Heads" : "Tails");
            });
    }

    private LootOption ChooseLootOption(LootOption[] lootOptions)
    {
        // Example of choosing a random loot option; adjust as needed
        int randomIndex = UnityEngine.Random.Range(0, lootOptions.Length);
        return lootOptions[randomIndex];
    }

    private Item CreateItemFromLoot(LootOption lootOption)
    {
        var item = new Item
        {
            loot = lootOption.loot,
            count = UnityEngine.Random.Range(lootOption.minValue, lootOption.maxValue + 1)
        };
        return item;
    }

    private void AddItemToInventory(Item item)
    {
        // Example: Add item to the first available slot in the inventory
        for (int x = 0; x < inventoryManager.width; x++)
        {
            for (int y = 0; y < inventoryManager.height; y++)
            {
                if (inventoryManager.GetItem(x, y) == null)
                {
                    inventoryManager.AddItemToSlot(x, y, item);
                    return;
                }
            }
        }

        Debug.Log("No available slots in the inventory.");
    }

    private void OnDestroy()
    {
        GameManager.LockPlayer(false);

        // Destroy the CoinFlip object
        Destroy(gameObject);
    }
}