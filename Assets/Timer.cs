using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timerText;
    float elapsedTime;
    bool stopTimer = false;

    // Update is called once per frame
    void Update()
    {
        if (stopTimer) return;

        elapsedTime += Time.deltaTime;
        int minutes = Mathf.FloorToInt(elapsedTime / 60);
        int seconds = Mathf.FloorToInt(elapsedTime % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void endTimer()
    {
        stopTimer = true;

        // Save current run values
        PlayerPrefs.SetFloat("CurrentElapsedTime", elapsedTime);
        PlayerPrefs.SetString("CurrentScore", timerText.text);

        // Get existing best (use a very large default if none exists)
        float best = PlayerPrefs.GetFloat("BestElapsedTime", float.MaxValue);

        // Lower time is better — update best if current is lower
        if (elapsedTime < best)
        {
            PlayerPrefs.SetFloat("BestElapsedTime", elapsedTime);
            PlayerPrefs.SetString("HighScore", timerText.text);
        }

        PlayerPrefs.Save();
    }

}