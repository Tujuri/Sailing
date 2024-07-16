using UnityEngine;
using TMPro;

public class CurrencyDisplay : MonoBehaviour
{
    public TMP_Text textField; // Drag and drop the TextMeshPro text component here in the Inspector

    private void Start()
    {
        UpdateCurrencyDisplay();
    }

    private void UpdateCurrencyDisplay()
    {
        if (textField != null)
        {
            // Update the text field with current currency value from GameManager
            textField.text = $"Currency: {GameManager.currency}";
        }
        else
        {
            Debug.LogWarning("TextField is not assigned in the CurrencyDisplay script.");
        }
    }

    private void Update()
    {
        // Update the currency display continuously (optional)
        //UpdateCurrencyDisplay();
    }
}
