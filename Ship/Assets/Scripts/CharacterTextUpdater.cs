using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class CharacterTextUpdater : MonoBehaviour
{
    public TMP_Text textField; // Public field to assign the TMP_Text component in the Inspector
    private HashSet<Collider2D> collidingSeaNameObjects = new HashSet<Collider2D>(); // HashSet to store colliders of objects with "SeaName" tag

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the object entering the collider has the tag "SeaName"
        if (other.CompareTag("SeaName"))
        {
            collidingSeaNameObjects.Add(other); // Add collider to HashSet
            UpdateTextField(); // Update text field based on current state
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        // Check if the object exiting the collider has the tag "SeaName"
        if (other.CompareTag("SeaName"))
        {
            collidingSeaNameObjects.Remove(other); // Remove collider from HashSet
            UpdateTextField(); // Update text field based on current state
        }
    }

    private void UpdateTextField()
    {
        // Check if there are any "SeaName" objects currently colliding
        if (collidingSeaNameObjects.Count > 0)
        {
            // Find a "SeaName" object to display its name
            foreach (var collider in collidingSeaNameObjects)
            {
                string collidedObjectName = collider.gameObject.name;
                if (!string.IsNullOrEmpty(collidedObjectName))
                {
                    textField.text = collidedObjectName; // Update the TMP_Text field with the name of the collided object
                    return; // Exit the method after updating once
                }
            }
        }
        else
        {
            textField.text = "lost"; // Update the text field to "lost" if no "SeaName" objects are colliding
        }
    }
}