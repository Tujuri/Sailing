using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class GameManager : MonoBehaviour
{
    public static GameManager manager;
    public static List<Interactable> interactables = new();

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
    }
}
