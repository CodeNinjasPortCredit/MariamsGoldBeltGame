using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour
{
    public void LoadMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void LoadGamePlay()
    {
        SceneManager.LoadScene("New Gameplay");
    }

    public void LoadStart()
    {
        SceneManager.LoadScene("Start Scene");
    }

    public void LoadLeaderboard()
    {
        SceneManager.LoadScene("Leaderboard");
    }
}
