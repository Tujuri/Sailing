using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(menuName = "Loot")]
public class Loot : ScriptableObject
{
    public enum InventoryLayout
    {
        One_by_One,
        //[]
        One_by_Two,
        //[]
        //[]
        One_by_Three,
        //[]
        //[]
        //[]
        Two_by_One,
        //[][]
        Two_by_Two,
        //[][]
        //[][]
        Two_by_Three,
        //[][]
        //[][]
        //[][]
        Three_by_One,
        //[][][]
        Three_by_Two,
        //[][][]
        //[][][]
        Three_by_Three,
        //[][][]
        //[][][]
        //[][][]
        L_Shaped_0,
        //[]
        //[]
        //[][]
        L_Shaped_90,
        //[][][]
        //[]
        L_Shaped_180,
        //[][]
        //  []
        //  []
        L_Shaped_270,
        //    []
        //[][][]
    }
    public InventoryLayout layoutInInventory;
    public Sprite lootSprite;
    
    [HideInInspector] public int minValue;
    [HideInInspector] public int maxValue;
}