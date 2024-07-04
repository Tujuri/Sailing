using System;
using UnityEngine;

public class LootTable : MonoBehaviour
{
    public LootOption[] lootOptions; // Array of loot options

    // Example method to get a random loot option
    public LootOption GetRandomLootOption()
    {
        if (lootOptions.Length == 0)
            return null;

        int randomIndex = UnityEngine.Random.Range(0, lootOptions.Length);
        return lootOptions[randomIndex];
    }
}
