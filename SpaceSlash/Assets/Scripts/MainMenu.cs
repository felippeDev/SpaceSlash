using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    void Start()
    {
        FindObjectOfType<BackgroundMusic>().PlayMusic("Menu");
    }

    public void PlayGame()
    {
        Debug.Log("Play Game");
        SceneManager.LoadScene("GameEngine");
    }

    public void QuitGame()
    {
        Debug.Log("Quit Game");
        Application.Quit();
    }
}
