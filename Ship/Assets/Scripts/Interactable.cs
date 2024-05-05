using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public delegate void Interact();
    public event Interact OnInteract;
    
    public string interactText;
    public float textHeight;

    private GameObject overheadText;
    
    private void Start()
    {
        GameManager.interactables.Add(this);
    }

    public void Trigger()
    {
        OnInteract?.Invoke();
    }

    public void ShowText(bool show)
    {
        if(interactText == "")
            return;
        
        if (show)
        {
            if (overheadText == null)
            {
                overheadText = Instantiate(Resources.Load<GameObject>($"HUD/OverheadText"),
                    transform.position + Vector3.up * textHeight, Quaternion.identity);
                overheadText.transform.GetChild(0).GetComponent<TMP_Text>().text = interactText;
            }
        }
        else if(overheadText != null)
            Destroy(overheadText);
    }
}
