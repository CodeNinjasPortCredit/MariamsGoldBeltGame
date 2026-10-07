using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LeaderBoard : MonoBehaviour
{
    [SerializeField]
    private TMP_Text textName;
    [SerializeField]
    private TMP_Text score;

    [SerializeField]
    private TMP_Text highScore;
    [SerializeField]
    TMP_InputField myInput;

    public GameObject HighScoreField;

    // Run once when the leaderboard scene/obj becomes active
    private void OnEnable()
    {
        GetLeaderboard();
    }

    public void GetLeaderboard()
    {
        // Show current player's formatted score string (saved by Timer)
        score.text = PlayerPrefs.GetString("CurrentScore", "00:00");

        // Prefer the saved HighScore string; if missing, format BestElapsedTime
        string highScoreString = PlayerPrefs.GetString("HighScore", string.Empty);
        if (!string.IsNullOrEmpty(highScoreString))
        {
            highScore.text = highScoreString;
        }
        else
        {
            float best = PlayerPrefs.GetFloat("BestElapsedTime", float.MaxValue);
            if (best == float.MaxValue)
            {
                highScore.text = "--:--";
            }
            else
            {
                int minutes = Mathf.FloorToInt(best / 60f);
                int seconds = Mathf.FloorToInt(best % 60f);
                highScore.text = string.Format("{0:00}:{1:00}", minutes, seconds);
            }
        }

        // Determine whether the current run is a new best.
        float current = PlayerPrefs.GetFloat("CurrentElapsedTime", float.MaxValue);
        float bestTime = PlayerPrefs.GetFloat("BestElapsedTime", float.MaxValue);

        // Show name input if current run is a new best (lower time is better)
        HighScoreField.SetActive(current <= bestTime);

        // Populate name display with the input value (or any previously saved name)
        string savedName = PlayerPrefs.GetString("HighScoreName", string.Empty);
        if (!string.IsNullOrEmpty(savedName))
        {
            textName.text = savedName;
            myInput.text = savedName;
        }
        else
        {
            textName.text = myInput.text;
        }
    }
}
