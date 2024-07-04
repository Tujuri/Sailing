using System;

[Serializable]
public class LootOption
{
    public Loot loot; // The Loot object that represents the item
    public int minValue; // Minimum quantity of the loot
    public int maxValue; // Maximum quantity of the loot
}
