using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public GameObject PlayButtonGO;
    public GameObject FireButtonGO;
    public GameObject JoystickGO;
    public GameObject PlayerShipGO;
    public GameObject EnemySpawnerGO;
    public GameObject GameOverGO;
    public GameObject scoreTextUIGO;
    public GameObject TimeCounterGO;
    public GameObject GameLogoGO;
    public GameObject PauseButton;
    public GameObject PausePanel;
    public GameObject BackgroundMusicGO;

    public enum GameManagerState
    {
        Opening,
        GamePlay,
        GameOver,
        GamePaused,
        GameResumed,
    }

    GameManagerState GMState;

    // Use this for initialization
    void Start () {
        GMState = GameManagerState.Opening;
	}

    void UpdateGameManagerState()
    {
        switch (GMState)
        {
            case GameManagerState.Opening:

                // Hide "You died" and show play button
                GameOverGO.SetActive(false);

                GameLogoGO.SetActive(true);

                PlayButtonGO.SetActive(true);

                PausePanel.SetActive(false);

                PauseButton.SetActive(false);

                FireButtonGO.SetActive(false);

                JoystickGO.SetActive(false);

                break;
            case GameManagerState.GamePlay:

                // Set zero score, hide playButton and start Player and enemy spawner
                scoreTextUIGO.GetComponent<GameScore>().Score = 0;

                PausePanel.SetActive(false);

                GameLogoGO.SetActive(false);

                PlayButtonGO.SetActive(false);

                FireButtonGO.SetActive(true);

                JoystickGO.SetActive(true);

                PauseButton.SetActive(true);

                PlayerShipGO.GetComponent<PlayerControl>().Init();

                EnemySpawnerGO.GetComponent<EnemySpawner>().StartEnemySpawner();

                TimeCounterGO.GetComponent<TimeCounter>().StartTimeCounter();

                break;
            case GameManagerState.GameOver:
                TimeCounterGO.GetComponent<TimeCounter>().StopTimeCounter();    

                EnemySpawnerGO.GetComponent<EnemySpawner>().StopEnemySpawner();

                FireButtonGO.SetActive(false);

                JoystickGO.SetActive(false);

                PauseButton.SetActive(false);

                GameOverGO.SetActive(true);

                Invoke("ChangeToOpeningState", 3f);

                break;
            case GameManagerState.GamePaused:
                Time.timeScale = 0;

                BackgroundMusicGO.GetComponent<AudioSource>().Pause();

                PausePanel.SetActive(true);

                PauseButton.SetActive(false);

                break;
            case GameManagerState.GameResumed:
                PausePanel.SetActive(false);

                PauseButton.SetActive(true);

                BackgroundMusicGO.GetComponent<AudioSource>().Play();

                Time.timeScale = 1;

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

    public void StartGamePlay()
    {
        GMState = GameManagerState.GamePlay;
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

    public void ChangeToOpeningState()
    {
        SetGameManagerState(GameManagerState.Opening);
    }
}
