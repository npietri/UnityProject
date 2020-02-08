using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManageScore : MonoBehaviour {

    public static int score;

    Text text;

    void Awake()
    {
        text = GetComponent<Text>();
        DontDestroyOnLoad(transform.gameObject);
    }

    // updating score on the screen with every enemy killed
    void Update()
    {
        text.text = "SCORE: " + score;
    }

    public static int GetScore()
    {
        return score;
    }

}
