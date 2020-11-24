using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using UnityEngine;

public class HighscoreTable : MonoBehaviour
{
    private HighscoreEntry[] entries;

    private void OnEnable()
    {
        entries = GetComponentsInChildren<HighscoreEntry>();
        int currentEntry = 0;

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
                SqlCommand cmd = new SqlCommand("SELECT * FROM highscores;", connection);

                //execute the SQLCommand
                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        if (currentEntry < entries.Length)
                        {
                            entries[currentEntry].nameText.SetText(dr.GetString(0));
                            entries[currentEntry].scoreText.SetText(dr.GetInt32(1).ToString());
                            currentEntry++;
                        }
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
