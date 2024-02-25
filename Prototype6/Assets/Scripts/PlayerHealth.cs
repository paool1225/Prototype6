using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
public class PlayerHealth : MonoBehaviour
{
    public int health = 10;
    public TextMeshProUGUI healthText; // Reference to TMP text for health

    void Start()
    {
        UpdateHealthText(); // Update health text on game start
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            health -= 1;
            UpdateHealthText(); // Update health text when taking damage

            if (health <= 0)
            {
                Die();
            }
        }
    }

    void UpdateHealthText()
    {
        healthText.text = "Health Points: " + health.ToString(); // Update the text to show current health
    }

    void Die()
    {
        SceneManager.LoadScene("GameOverScene"); // Load the Game Over scene
    }
}