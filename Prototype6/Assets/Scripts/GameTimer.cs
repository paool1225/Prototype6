using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
public class GameTimer : MonoBehaviour
{
    public float gameDuration = 10f; // 1 minute
    public TextMeshProUGUI timerText; // Reference to your TMP text for the timer

    void Start()
    {
        StartCoroutine(UpdateTimer());
    }

    IEnumerator UpdateTimer()
    {
        while (gameDuration > 0)
        {
            gameDuration -= Time.deltaTime;
            UpdateTimerText();
            yield return null;
        }

        SceneManager.LoadScene("GameWinScene"); // Load the win scene when time runs out
    }

    void UpdateTimerText()
    {
        // Format the time as you prefer, here it's minutes:seconds
        int minutes = Mathf.FloorToInt(gameDuration / 60);
        int seconds = Mathf.FloorToInt(gameDuration % 60);
        timerText.text = $"Time Remaining: {minutes:00}:{seconds:00}";
    }

    public void AddTimeToTimer(float seconds) // for add x seconds to timer card
    {
        gameDuration += seconds;
        UpdateTimerText();
    }
}