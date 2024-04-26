using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    private bool isGamePaused = false;
    public GameObject pauseMenuUI;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isGamePaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void PauseGame()
    {
        isGamePaused = true;
        Time.timeScale = 0f; 
        pauseMenuUI.SetActive(true); 
    }
    public void ResumeGame()
    {
        isGamePaused = false;
        Time.timeScale = 1f; 
        pauseMenuUI.SetActive(false); 
    }
}
