using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    public GameObject pauseMenuUI;
    private bool isPaused = false;  // To track if the game is paused
    void Update(){
        if (Input.GetKeyDown(KeyCode.Escape))
        {

            TogglePause();
        }
    }

    // Method to toggle pause
    public void TogglePause()
    {
        if (isPaused)
        {
            ResumeGame();
        }
        else
        {
            
            PauseGameFunction();
        }
    }

    // Method to pause the game
    void PauseGameFunction()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;  // Freeze time
        isPaused = true;
    }

    // Method to resume the game
    void ResumeGame()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;  // Resume time
        isPaused = false;
    }

    public void ReturnMenu()
    {
        isPaused = false;
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        SceneManager.LoadScene("StartMenu");
    }
}
