using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
public class GameTimer : MonoBehaviour
{
    public float gameDuration = 90f; // 1 minute and 30 seconds
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
}