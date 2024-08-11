using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Loot")]
public class Loot : ScriptableObject
{
    public enum InventoryLayout
    {
        One_by_One,
        One_by_Two,
        One_by_Three,
        Two_by_One,
        Two_by_Two,
        Two_by_Three,
        Three_by_One,
        Three_by_Two,
        Three_by_Three,
        L_Shaped_0,
        L_Shaped_90,
        L_Shaped_180,
        L_Shaped_270
    }

    public InventoryLayout layoutInInventory;
    public Sprite lootSprite;

    [HideInInspector] public int minValue;
    [HideInInspector] public int maxValue;

    public int[,] GetShape()
    {
        switch (layoutInInventory)
        {
            case InventoryLayout.One_by_One:
                return new int[,] { { 1 } };
            case InventoryLayout.One_by_Two:
                return new int[,] { { 1 }, { 1 } };
            case InventoryLayout.One_by_Three:
                return new int[,] { { 1 }, { 1 }, { 1 } };
            case InventoryLayout.Two_by_One:
                return new int[,] { { 1, 1 } };
            case InventoryLayout.Two_by_Two:
                return new int[,] { { 1, 1 }, { 1, 1 } };
            case InventoryLayout.Two_by_Three:
                return new int[,] { { 1, 1 }, { 1, 1 }, { 1, 1 } };
            case InventoryLayout.Three_by_One:
                return new int[,] { { 1, 1, 1 } };
            case InventoryLayout.Three_by_Two:
                return new int[,] { { 1, 1, 1 }, { 1, 1, 1 } };
            case InventoryLayout.Three_by_Three:
                return new int[,] { { 1, 1, 1 }, { 1, 1, 1 }, { 1, 1, 1 } };
            case InventoryLayout.L_Shaped_0:
                return new int[,] { { 1, 0 }, { 1, 0 }, { 1, 1 } };
            case InventoryLayout.L_Shaped_90:
                return new int[,] { { 1, 1, 1 }, { 1, 0, 0 } };
            case InventoryLayout.L_Shaped_180:
                return new int[,] { { 1, 1 }, { 0, 1 }, { 0, 1 } };
            case InventoryLayout.L_Shaped_270:
                return new int[,] { { 0, 0, 1 }, { 1, 1, 1 } };
            default:
                return new int[,] { { 1 } };
        }
    }
}