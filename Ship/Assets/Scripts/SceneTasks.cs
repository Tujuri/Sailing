using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTasks : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void QuitFromScene()
    {
        Application.Quit();
        Debug.Log("Quit Game");
    }

    void Home()
    {
        SceneManager.LoadScene("Menu");
    }
}
