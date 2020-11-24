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
        SendScore();
    }

    public void AddScore(int scoreToAdd = 1)
    {
        Score += scoreToAdd;
    }

    public void SendScore()
    {
        SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
        builder.DataSource = "awlodarczyk.database.windows.net";
        builder.UserID = "awlodarczyk";
        builder.Password = "pass";
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
                SqlCommand cmd = new SqlCommand("SELECT * FROM highscores;", connection);

                //execute the SQLCommand
                SqlDataReader dr = cmd.ExecuteReader();

                //check if there are records
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        //display retrieved record (first column only/string value)
                        Debug.Log(dr.GetString(0));
                    }
                }
                else
                {
                    Debug.Log("No data found.");
                }
                dr.Close();
            }
        }
        catch (Exception ex)
        {
            //display error message
            Debug.Log("Exception: " + ex.Message);
        }
    }
}
