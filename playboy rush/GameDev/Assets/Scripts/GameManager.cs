using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    //TODO Implementasi Lose Condition
    UnityEngine.UI.Text winLoseText;
    AudioSource musicBackground;

    void Start() {
        winLoseText = GameObject.Find("Win/Lose").GetComponent<Text>();
        musicBackground = GameObject.Find("Background Music").GetComponent<AudioSource>();
    }

    void Update()
    {
        // press 'space' to restart game
        if (Input.GetKey(KeyCode.Space))
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene("TestScene");
        }
    }

    public void Win()
    {
        //TODO
        Time.timeScale = 0f;
        winLoseText.text = "YOU WIN!";
        musicBackground.Stop();
    }

    public void Lose()
    {
        //TODO
        //Hint: you can call public methods from other scripts
        //Example: Timer.cs may use GameManager.Lose() method if this method is public.
        //Hint2: Time.timeScale() is the "time while the game is running".
        Time.timeScale = 0f;
        winLoseText.text = "YOU LOSE!";
        musicBackground.Stop();
    }
}
