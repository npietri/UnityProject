using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour {

    public GameObject NameInput;
    public GameObject BlackBackground;

    // Hide Player name input
    void Awake()
    {
        NameInput.SetActive(false);
        BlackBackground.SetActive(false);
    }

    public void StartGame()
    {
        // "Stage1" is the name of the first scene we created.
        SceneManager.LoadScene("Stage1");
        // Reset score:
        ManageScore.score = 0;
    }

    public void Highscore()
    {
        // Display highscores
        SceneManager.LoadScene("Hightscores");
    }

    public void MainMenu()
    {
        // "Display Main Menu
        SceneManager.LoadScene("Menu");
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    // Save Player Name
    public void ChangePlayerName(string playerName)
    {
        string playerNameUpperCase = playerName.ToUpper();
        PlayerPrefs.SetString("playerName", playerNameUpperCase);
        PlayerPrefs.Save();
        Debug.Log(playerNameUpperCase);
        NameInput.SetActive(false);
        BlackBackground.SetActive(false);
    }

    // Display field to edit Player Name
    public void EditPlayerName()
    {
        NameInput.SetActive(true);
        BlackBackground.SetActive(true);
    }

}
