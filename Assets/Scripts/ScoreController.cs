using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using TMPro;
using UnityEngine;

public class ScoreController : MonoBehaviour
{
    public GameController gameController;
    public TextMeshProUGUI loseScreenScoreText;
    public TextMeshProUGUI inGameScoreText;

    public TMP_InputField nameInputField;

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

    public void SendScoreAndPlayAgain()
    {
        SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
        builder.DataSource = "awlodarczyk.database.windows.net";
        builder.UserID = "awlodarczyk";
        builder.Password = "";
        builder.InitialCatalog = "awlodarczykdb";
        try
        {
            // connect to the databases
            using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
            {
                // if open then the connection is established
                connection.Open();
                Debug.Log("connection established");
                // sql command
                SqlCommand cmd = new SqlCommand($"INSERT INTO highscores(player_name, score) VALUES('{nameInputField.text}',{Score})", connection);

                //execute the SQLCommand
                SqlDataReader dr = cmd.ExecuteReader();

                dr.Close();
            }
        }
        catch (Exception ex)
        {
            //display error message
            Debug.Log("Exception: " + ex.Message);
        }

        gameController.StartGame();
    }
}
