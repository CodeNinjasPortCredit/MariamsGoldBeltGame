using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;

public class Buttons : MonoBehaviour
{
    // Newly Added
    public GameObject HintPanel;
    public CinemachineVirtualCamera gameplayCamera;

    public GameObject BobBody;

    public GameObject BobMainCamera;

    public GameObject TimerText;

    public GameObject MenuButton;
    public GameObject StartButton;

    public GameObject Guard;
    public void LoadMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void LoadGamePlay()
    {
        // Finalize this: Either load the gameplay directly or the start scene
        // SceneManager.LoadScene("New Gameplay");
        SceneManager.LoadScene("New Gameplay (Jacky)");
    }

    public void LoadStart()
    {
        // Finalize this: Either load the gameplay directly or the start scene
        // SceneManager.LoadScene("Start Scene");
        SceneManager.LoadScene("New Gameplay (Jacky)");
    }

    public void LoadLeaderboard()
    {
        SceneManager.LoadScene("Leaderboard");
    }

    // Newly Added
    public void StartGameplay() {
        HintPanel.SetActive(true);
        gameplayCamera.Follow = BobBody.transform;
        TimerText.SetActive(true);
        MenuButton.SetActive(false);
        StartButton.SetActive(false);  
        Guard.SetActive(false);
    }
}
