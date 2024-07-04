using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class GameManager : MonoBehaviour
{
    public static GameManager manager;
    public static Transform HUD;
    public static Player player;
    public static List<Interactable> interactables = new();
    public static InventoryManager inventoryManager;
    public static int currency = 0;

    [Header("Main Menu")] 
    public Interactable continueInteractable;
    public Interactable newGameInteractable;
    public Interactable settingsInteractable;
    public Interactable quitInteractable;

    private void Awake()
    {
        manager = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        newGameInteractable.OnInteract += () => LoadScene("Main Scene");
        quitInteractable.OnInteract += Application.Quit;
    }

    public void LoadScene(string sceneName)
    {
        interactables.Clear();
        
        SceneManager.LoadScene(sceneName);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    
    private static void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;

        GetReferences();
    }

    private static void GetReferences()
    {
        if(GameObject.Find("HUD"))
            HUD = GameObject.Find("HUD").transform;
        if(GameObject.Find("Player"))
            player = GameObject.Find("Player").GetComponent<Player>();
    }

    public static void LockPlayer(bool isLocked)
    {
        player.isLocked = isLocked;
    }

    // Method to add currency (Tyson Added)
    public static void AddCurrency(int amount)
    {
        currency += amount;
        Debug.Log($"Currency updated: +{amount}, Total: {currency}");
    }

    // Method to subtract currency (if Tyson Added)
    public static void SubtractCurrency(int amount)
    {
        currency -= amount;
        Debug.Log($"Currency updated: -{amount}, Total: {currency}");
    }
}
