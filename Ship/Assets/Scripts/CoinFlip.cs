using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

public class CoinFlip : MonoBehaviour, IPointerClickHandler
{
    public Transform[] coins; // Array of coin transforms for the flip effect
    public InventoryManager inventoryManager; // Reference to the InventoryManager script

    private List<Transform> coinFlips = new(); // List to store the coin flip transforms

    public void Initialize(InventoryManager inventoryManager, Transform coinRenderers)
    {
        this.inventoryManager = inventoryManager;

        // Setup the coinRenderers for flipping
        for (var i = 0; i < coins.Length; i++)
        {
            coinFlips.Add(coinRenderers.GetChild(i).GetChild(0));
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        // Handle coin flip when clicking on the coin
        FlipCoin(coins[0]);  // Assuming we flip the first coin for simplicity
    }

    public void FlipCoin(Transform coin)
    {
        var result = UnityEngine.Random.value > 0.5f;
        var coinFlip = coinFlips[Array.IndexOf(coins, coin)];

        const float duration = 0.6f;
        const int flips = 2;

        coinFlip.DOLocalMoveZ(0.8f, duration / 2).SetEase(Ease.OutQuad);
        coinFlip.DOLocalMoveZ(1, duration / 2).SetEase(Ease.InQuad).SetDelay(duration / 2);
        coinFlip.DOLocalRotate(new Vector3(0, 360 * flips, 0), duration, RotateMode.FastBeyond360)
            .SetEase(Ease.Linear).OnComplete(() =>
            {
                coinFlip.localRotation = Quaternion.Euler(new Vector3(0, result ? 0 : 180, 0));
                Debug.Log(result ? "Heads" : "Tails");

                // Get the loot based on the result of the coin flip
                Loot lootData = GetLootBasedOnResult(result);
                // Add the loot to the inventory
                AddLootToInventory(lootData);
            });

        // Remove the EventTrigger component, as we use IPointerClickHandler
        Destroy(coin.GetComponent<EventTrigger>());
    }

    private Loot GetLootBasedOnResult(bool result)
    {
        // Determine loot data based on the result of the coin flip
        // Replace with your actual logic to get loot data
        return result ? /* Get heads loot */ null : /* Get tails loot */ null;
    }

    private void AddLootToInventory(Loot lootData)
    {
        // Example coordinates, you should calculate where to place the item
        int x = UnityEngine.Random.Range(0, inventoryManager.width);
        int y = UnityEngine.Random.Range(0, inventoryManager.height);
        inventoryManager.AddItemToSlot(x, y, lootData);
    }

    private void OnDestroy()
    {
        GameManager.LockPlayer(false);

        // Remove the coin renderers when CoinFlip is destroyed
        if (transform.childCount > 0)
        {
            Destroy(transform.GetChild(0).gameObject);
        }
    }
}