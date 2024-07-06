using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public enum Systems { InventoryPanel }

public class SystemRegister : MonoBehaviour
{
    [SerializeField]
    public Systems system;
    void Awake()
    {
        GameManager.RegisterSystem(gameObject, (int)system);
        gameObject.SetActive(false);
    }
}
