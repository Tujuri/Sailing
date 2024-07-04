using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class CustomInventoryManager : MonoBehaviour
{
    public GameObject slotPrefab; // Assign the InventorySlot prefab in the Inspector
    public Transform slotParent; // Assign the InventoryPanel in the Inspector
    public List<Vector2> slotPositions; // Define positions for each slot in the Inspector

    void Start()
    {
        foreach (var position in slotPositions)
        {
            GameObject newSlot = Instantiate(slotPrefab, slotParent);
            newSlot.GetComponent<RectTransform>().anchoredPosition = position;
        }
    }
}

