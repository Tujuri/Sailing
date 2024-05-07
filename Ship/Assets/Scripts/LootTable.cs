using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootTable : MonoBehaviour
{
    public LootOption loot_0;
    public LootOption loot_1;
    public LootOption loot_2;
    public LootOption loot_3;
    public LootOption loot_4;
    public LootOption loot_5;
}

[Serializable]
public class LootOption
{
    public Loot loot;
    public int minValue;
    public int maxValue;
}
