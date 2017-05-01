using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour {

    public GameObject PlayButtonGO;
    public GameObject QuitButtonGO;

    void Start()
    {
        FindObjectOfType<BackgroundMusic>().PlayMusic("GameOver");
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("GameEngine");
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
