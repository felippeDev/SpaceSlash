using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameScore : MonoBehaviour {

    Text scoreTextUI;

    int score;

    public int Score
    {
        get
        {
            return this.score;
        }

        set
        {
            this.score = value;
            UpdateScoreTextUI();
        }
    }

    // Use this for initialization
    void Start () {
        scoreTextUI = GetComponent<Text>();
	}
	
	void UpdateScoreTextUI()
    {
        string scoreText = string.Format("{0:000000}", score);

        scoreTextUI.text = scoreText;
    }
}
