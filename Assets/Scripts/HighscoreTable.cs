using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HighscoreTable : MonoBehaviour {

    public Transform entryContainer;
    public Transform entryTemplate;
    private List<Transform> highscoreEntryTransformList;

    private void Awake()
    {

        entryTemplate.gameObject.SetActive(false);

        // getting saved highscores
        string jsonString = PlayerPrefs.GetString("scoreTable");
        Highscores highscores = JsonUtility.FromJson<Highscores>(jsonString);

        if (highscores == null)
        {
            // Initialize table if it doesn't exist
            AddHighscoreEntry(1000, "ANNA");
            AddHighscoreEntry(999, "NICO");
            AddHighscoreEntry(0, "O");
            AddHighscoreEntry(0, "O");
            AddHighscoreEntry(0, "O");
            // Reload
            jsonString = PlayerPrefs.GetString("scoreTable");
            highscores = JsonUtility.FromJson<Highscores>(jsonString);
        }

        // Sort entry list by score value
        for (int i = 0; i < highscores.highscoreEntryList.Count; i++)
        {
            for (int j = i + 1; j < highscores.highscoreEntryList.Count; j++)
            {
                if (highscores.highscoreEntryList[j].score > highscores.highscoreEntryList[i].score)
                {
                    // Swap
                    HighscoreEntry tmp = highscores.highscoreEntryList[i];
                    highscores.highscoreEntryList[i] = highscores.highscoreEntryList[j];
                    highscores.highscoreEntryList[j] = tmp;
                }
            }
        }

        // Limit the score list to top 5
        var top5List = highscores.highscoreEntryList.GetRange(0, 5);

        // Transform score values list into Highscore list
        highscoreEntryTransformList = new List<Transform>();
        foreach (HighscoreEntry highscoreEntry in top5List)
        {
            CreateHighscoreEntryTransform(highscoreEntry, entryContainer, highscoreEntryTransformList);
        }

    }

    // Create Highscore entry
    private void CreateHighscoreEntryTransform(HighscoreEntry highscoreEntry, Transform container, List<Transform> transformList)
    {
        // Create entry from the temlate and place it in the container
        float templateHeight = 30f;
        Transform entryTransform = Instantiate(entryTemplate, container);
        RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();
        entryRectTransform.anchoredPosition = new Vector2(0, -templateHeight * transformList.Count);
        entryTransform.gameObject.SetActive(true);

        // ranks starts from 1 not 0
        int rank = transformList.Count + 1;

        // converting the rank name
        string rankString;

        // changin rank name
        switch (rank)
        {
            case 1: rankString = "1ST"; break;
            case 2: rankString = "2ND"; break;
            case 3: rankString = "3RD"; break;
            default: rankString = rank + "TH"; break;
        }

        // Assign values to the fields 
        entryTransform.Find("positonText").GetComponent<Text>().text = rankString;

        int score = highscoreEntry.score;
        entryTransform.Find("scoreText").GetComponent<Text>().text = score.ToString();

        string name = highscoreEntry.name;
        entryTransform.Find("nameText").GetComponent<Text>().text = name;

        // Set background visible, odds and evens, easier to read
        entryTransform.Find("background").gameObject.SetActive(rank % 2 == 1);

        // Highlight Top score
        if (rank == 1)
        {
            entryTransform.Find("positonText").GetComponent<Text>().color = Color.green;
            entryTransform.Find("scoreText").GetComponent<Text>().color = Color.green;
            entryTransform.Find("nameText").GetComponent<Text>().color = Color.green;
        }

        // Set tropy (star) with color
        switch (rank)
        {
            default:
                entryTransform.Find("trophy").gameObject.SetActive(false);
                break;
            case 1:
                entryTransform.Find("trophy").GetComponent<Image>().color = new Color32(255, 210, 0, 100);
                break;
            case 2:
                entryTransform.Find("trophy").GetComponent<Image>().color = new Color32(198, 198, 198, 100);
                break;
            case 3:
                entryTransform.Find("trophy").GetComponent<Image>().color = new Color32(183, 111, 86, 100);
                break;

        }

        // Add transformed Highscore to the list
        transformList.Add(entryTransform);

    }

    // Add Highscore entry
    public static void AddHighscoreEntry(int score, string name)
    {
        // Create HighscoreEntry
        HighscoreEntry highscoreEntry = new HighscoreEntry { score = score, name = name };

        // Load saved Highscores
        string jsonString = PlayerPrefs.GetString("scoreTable");
        Highscores highscores = JsonUtility.FromJson<Highscores>(jsonString);

        // Initialize highscore table if it doesn't exist
        if (highscores == null)
        {
            highscores = new Highscores()
            {
                highscoreEntryList = new List<HighscoreEntry>()
            };
        }

        // Add new entry to Highscores
        highscores.highscoreEntryList.Add(highscoreEntry);

        // Save updated Highscores
        string json = JsonUtility.ToJson(highscores);
        PlayerPrefs.SetString("scoreTable", json);
        PlayerPrefs.Save();
    }

    // Class for Highscores list
    private class Highscores
    {
        public List<HighscoreEntry> highscoreEntryList;
    }

    // Class for a single High score entry
    [System.Serializable] // class can be serialized.
    private class HighscoreEntry
    {
        public int score;
        public string name;
    }

    // navigation button
    public void MainMenu()
    {
        // "Display Main Menu
        SceneManager.LoadScene("Menu");
    }
}
