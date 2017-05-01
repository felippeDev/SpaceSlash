using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public GameObject PlayerShipGO;
    public GameObject EnemySpawnerGO;
    public GameObject scoreTextUIGO;
    public GameObject TimeCounterGO;
    public GameObject PauseButton;
    public GameObject PausePanel;
    public GameObject BackgroundMusicGO;

    public enum GameManagerState
    {
        GamePlay,
        GamePaused,
        GameResumed,
        QuitToMenu
    }

    GameManagerState GMState;

    // Use this for initialization
    void Start () {
        GMState = GameManagerState.GamePlay;
        FindObjectOfType<BackgroundMusic>().PlayMusic("Stage1");
    }

    void UpdateGameManagerState()
    {
        switch (GMState)
        {
            case GameManagerState.GamePlay:
                scoreTextUIGO.GetComponent<GameScore>().Score = 0;

                PausePanel.SetActive(false);

                PauseButton.SetActive(true);

                //PlayerShipGO.GetComponent<PlayerControl>().Init();

                //EnemySpawnerGO.GetComponent<EnemySpawner>().StartEnemySpawner();

                //TimeCounterGO.GetComponent<TimeCounter>().StartTimeCounter();

                break;
            case GameManagerState.GamePaused:
                Time.timeScale = 0;

                //BackgroundMusicGO.GetComponent<AudioSource>().Pause();

                PausePanel.SetActive(true);

                PauseButton.SetActive(false);

                break;
            case GameManagerState.GameResumed:
                PausePanel.SetActive(false);

                PauseButton.SetActive(true);

                //BackgroundMusicGO.GetComponent<AudioSource>().Play();

                Time.timeScale = 1;

                break;
            case GameManagerState.QuitToMenu:
                PausePanel.SetActive(false);

                PauseButton.SetActive(true);

                Time.timeScale = 1;

                SceneManager.LoadScene("MainMenu");

                break;
            default:
                break;
        }
    }

    public void SetGameManagerState(GameManagerState state)
    {
        GMState = state;
        UpdateGameManagerState();
    }

    public void PauseGame()
    {
        GMState = GameManagerState.GamePaused;
        UpdateGameManagerState();
    }

    public void ResumeGame()
    {
        GMState = GameManagerState.GameResumed;
        UpdateGameManagerState();
    }

    public void QuitToMenu()
    {
        GMState = GameManagerState.QuitToMenu;
        UpdateGameManagerState();
    }
}
