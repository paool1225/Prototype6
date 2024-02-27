using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ButtonManager : MonoBehaviour
{
    public void PlayGame(){
        SceneManager.LoadSceneAsync("Game");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void BackToMain()
    {
        SceneManager.LoadSceneAsync("MainMenu");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
        else if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
