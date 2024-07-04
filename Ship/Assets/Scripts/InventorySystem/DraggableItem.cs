using UnityEngine;
using UnityEngine.EventSystems;

public class DraggableItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    private Transform originalParent;
    private Loot lootData;
    private int[,] currentShape;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        originalParent = transform.parent;
        lootData = GetComponent<LootHolder>().loot; // Assuming you have a LootHolder script to hold Loot data
        currentShape = lootData.GetShape();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = false;
        transform.SetParent(transform.root); // Move to the root to ensure it's on top
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta;

        // Check for the "R" key press to rotate the item
        if (Input.GetKeyDown(KeyCode.R))
        {
            RotateItem();
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = true;

        // Check if the item can be placed in the current position
        if (IsValidPosition())
        {
            // Snap to the nearest valid position (you can implement snapping logic)
            transform.SetParent(originalParent); // Set to the correct parent based on snapping
        }
        else
        {
            // Return to the original position if placement is invalid
            rectTransform.anchoredPosition = Vector2.zero;
            transform.SetParent(originalParent);
        }
    }

    private void RotateItem()
    {
        currentShape = RotateShape(currentShape);
        transform.Rotate(0, 0, 90); // Rotate the item 90 degrees
    }

    private int[,] RotateShape(int[,] shape)
    {
        int rows = shape.GetLength(0);
        int cols = shape.GetLength(1);
        int[,] rotatedShape = new int[cols, rows];

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                rotatedShape[j, rows - i - 1] = shape[i, j];
            }
        }

        return rotatedShape;
    }

    private bool IsValidPosition()
    {
        // Implement logic to check if the current position is valid based on the item shape
        // This involves checking the slots under the item's shape to see if they are occupied

        // For now, let's return true as a placeholder
        return true;
    }
}