using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour {
    public float currentTime = 20;
    public bool finished;
    public float increaseTime;

    UnityEngine.UI.Text timerText;

    GameManager gm;
    //TO DO Implementasi sistem timeout

    // Use this for initialization
    void Start ()
    {
        timerText = GetComponent<UnityEngine.UI.Text>();
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

	// Update is called once per frame
	void Update ()
    {
        //your code here
        //Hint: use Time.deltaTime to obtain real time values
        //Hint2: Text component is used to display text in Unity UI system.
        currentTime -= Time.deltaTime;
        timerText.text = "" + Mathf.Round(currentTime);
        if (currentTime < 0)
        {
            gm.Lose();
        }

        // time warning
        if (currentTime <= 10f)
        {
            timerText.color = Color.red;
        }
        else
        {
            timerText.color = Color.white;
        }
    }

    // increase time when bar is full
    public void increaseTheTime ()
    {
        currentTime += increaseTime;
    }
}
