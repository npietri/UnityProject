using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {

    public static bool IsGamePaused = false;
    public GameObject pauseMenuUI;
    public GameObject gameover;

    // turnig of the menu at the beginnig of the game
    void Awake()
    {
        pauseMenuUI.SetActive(false);
        gameover.SetActive(false);
        Time.timeScale = 1f;
        IsGamePaused = false;
    }

    // turnig on and of the menu
    void Update ()
    {
		if (Input.GetKeyDown(KeyCode.Escape) || Input.GetButtonDown("Jump"))
        {
            if (IsGamePaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
	}

    // coming back to the paused game
    public void Resume ()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        IsGamePaused = false;

    }

    // pausing the game
    void Pause ()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        IsGamePaused = true;
    }

    // exit to the menu
    public void ExitToMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    // restarting the game
    public void RestartGame()
    {
        SceneManager.LoadScene("Stage1");
        Button[] buttons = PauseMenu.FindObjectsOfType<Button>();
        // turning ON "Resume button" in the menu after death of the hero
        buttons[1].interactable = true;
        // hide game over 
        gameover.SetActive(false);
        // Reset score:
        ManageScore.score = 0;
    }

    // exit the game
    public void ExitGame ()
    {
        Application.Quit();
    }
}
