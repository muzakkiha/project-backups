using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour {

    public int score = 0;
    public int winScore;

    UnityEngine.UI.Text scoreText;
    GameManager gm;

    //TO DO Implementasi sistem timeout

    // Use this for initialization
    void Start()
    {
        scoreText = GetComponent<UnityEngine.UI.Text>();
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = "Score: " + score + "/" + winScore;

        if (score >= winScore)
        {
            gm.Win();
        }
    }
}
