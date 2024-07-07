using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Scurvy : MonoBehaviour
{
    [Header("Fruit Usage")]
    public float fruit = 100; // Initial movement points
    public float pointsPerUnitDistance = 0.1f; // Points deducted per unit of distance traveled

    private Vector3 lastPosition;

    public TMP_Text fruitTotal; // Text component for fruit totals

    private void Start()
    {
        lastPosition = transform.position;
    }

    private void FixedUpdate()
    {
        DeductPoints();
       
        // Update the fruit total text to display the rounded integer value
        fruitTotal.text = "Fruit: " + Mathf.RoundToInt(fruit).ToString();
    }

    private void DeductPoints()
    {
        float distanceTraveled = Vector3.Distance(lastPosition, transform.position);

        if (distanceTraveled > 0)
        {
            // Deduct points based on the distance traveled
            fruit -= distanceTraveled * pointsPerUnitDistance;

            // Ensure movementPoints do not go below zero
            fruit = Mathf.Max(fruit, 0);

            // Update last position
            lastPosition = transform.position;

            Debug.Log("Remaining Fruit Points" + fruit);
        }
    }
}
