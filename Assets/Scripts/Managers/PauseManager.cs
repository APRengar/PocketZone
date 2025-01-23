using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    public static PauseManager Instance { get; private set; }

    [SerializeField] private GameObject pauseMenuUI; // Assign your Pause Menu UI GameObject in the Inspector.

    private bool isPaused = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        // Ensure the game starts unpaused
        Time.timeScale = 1f;
        isPaused = false;

        if (pauseMenuUI != null)
            pauseMenuUI.SetActive(false);
    }

    public void TogglePause()
    {
        // Toggle pause with the Escape key
        if (isPaused)
            ResumeGame();
        else
            PauseGame();
    }


    public void PauseGame()
    {
        if (isPaused) return;

        Time.timeScale = 0f; // Freeze game time
        isPaused = true;

        if (pauseMenuUI != null)
            pauseMenuUI.SetActive(true);

        Debug.Log("Game Paused");
    }

    public void ResumeGame()
    {
        if (!isPaused) return;

        Time.timeScale = 1f; // Resume game time
        isPaused = false;

        if (pauseMenuUI != null)
            pauseMenuUI.SetActive(false);

        Debug.Log("Game Resumed");
    }

    public bool IsGamePaused()
    {
        return isPaused;
    }
}
