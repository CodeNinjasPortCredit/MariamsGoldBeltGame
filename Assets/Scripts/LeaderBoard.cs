using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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

    public void GetLeaderboard()
    {
        textName.text = myInput.text;

        // Set CURRENT player's score
        score.text = PlayerPrefs.GetString("CurrentScore");

        // Set HIGH score and player's name
        highScore.text = PlayerPrefs.SetFloat("ElapsedTime", );

        // If player's current score is NOT greater than high score, do NOT show name field


    }
}
