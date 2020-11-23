using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreController : MonoBehaviour
{
    public GameController gameController;
    public TextMeshProUGUI loseScreenScoreText;
    public TextMeshProUGUI inGameScoreText;

    private int _score;

    public int Score
    {
        get => _score;
        private set
        {
            _score = value;
            inGameScoreText.SetText(_score.ToString());
        }
    }

    private void OnEnable()
    {
        gameController.GameStarted += OnGameStarted;
        gameController.GameEnded += OnGameEnded;
    }

    private void OnDisable()
    {
        gameController.GameStarted -= OnGameStarted;
        gameController.GameEnded -= OnGameEnded;
    }

    private void OnGameEnded(object sender, EventArgs e)
    {
        loseScreenScoreText.SetText(Score.ToString());
    }

    private void OnGameStarted(object sender, EventArgs e)
    {
        Score = 0;
    }

    public void AddScore(int scoreToAdd = 1)
    {
        Score += scoreToAdd;
    }
}
