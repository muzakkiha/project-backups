using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public float startTime;
    public float time;

    public Text timeText;
    public GameObject timeOverText;
    public GameObject youAreDeadText;
    public Text killText;

    public int kills;

    public GameObject player;
    public PlayerScript playerScript;

    public int enemyLimit;
    public int enemyCount;

    public GameObject howto;
    public GameObject mainMenu;
    public GameObject gameUI;
    public GameObject filterBlack;
    public GameObject endgameElse;
    public Text score;
    public Text highScore;
    public Text highScore2;

    public bool timeOver;
    public bool dead;

    // Start is called before the first frame update
    void Start()
    {
        time = startTime;
        playerScript = player.GetComponent<PlayerScript>();
        kills = 0;
        enemyCount = 0;
        timeOver = false;
        dead = false;
        highScore.text = "high score: " + PlayerPrefs.GetInt("HighScore").ToString() + " kills";
        highScore2.text = "high score: " + PlayerPrefs.GetInt("HighScore").ToString() + " kills";
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        time -= Time.deltaTime;
        timeText.text = ((int) time).ToString();
        killText.text = (kills).ToString();

        if (time <= 10f)
        {
            timeText.color = new Color(202/255f, 20/255f, 20/255f, 1f);
        }

        if(time < 1 && !dead)
        {
            time = 0;
            timeOverText.SetActive(true);
            timeOver = true;
            filterBlack.SetActive(true);
            gameUI.SetActive(false);
            endgameElse.SetActive(true);
            if (kills > PlayerPrefs.GetInt("HighScore"))
            {
                score.GetComponent<Text>().text = "Kills : " + (kills).ToString() + " -- new high score!";
                PlayerPrefs.SetInt("HighScore", kills);
            }
            else if (kills <= PlayerPrefs.GetInt("HighScore"))
            {
                score.GetComponent<Text>().text = "Kills : " + (kills).ToString();
            }
            Time.timeScale = 0f;
        }
        else if (playerScript.currentHealth <= 0f && !timeOver)
        {
            youAreDeadText.SetActive(true);
            dead = true;
            filterBlack.SetActive(true);
            gameUI.SetActive(false);
            endgameElse.SetActive(true);
            if (kills > PlayerPrefs.GetInt("HighScore"))
            {
                PlayerPrefs.SetInt("HighScore", kills);
                score.GetComponent<Text>().text = "Kills : " + (kills).ToString() + " -- new high score!";
            }
            else if (kills <= PlayerPrefs.GetInt("HighScore"))
            {
                score.GetComponent<Text>().text = "Kills : " + (kills).ToString();
            }
            Time.timeScale = 0f;
        }

        highScore.text = "high score: " + PlayerPrefs.GetInt("HighScore").ToString() + " kills";
        highScore2.text = "high score: " + PlayerPrefs.GetInt("HighScore").ToString() + " kills";
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene("GameScene");
        Time.timeScale = 1f;
    }

    public void Pause()
    {
        mainMenu.SetActive(true);
        gameUI.SetActive(false);
        Time.timeScale = 0f;
        filterBlack.SetActive(true);
    }

    public void Resume()
    {
        mainMenu.SetActive(false);
        gameUI.SetActive(true);
        Time.timeScale = 1f;
        filterBlack.SetActive(false);
    }

    public void OpenHowTo()
    {
        mainMenu.SetActive(false);
        howto.SetActive(true);
    }

    public void CloseHowTo()
    {
        mainMenu.SetActive(true);
        howto.SetActive(false);
    }

    public void MainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }

    public void resetHighScore()
    {
        Time.timeScale = 1f;
        PlayerPrefs.DeleteKey("HighScore");
        highScore.text = "high score: " + PlayerPrefs.GetInt("HighScore").ToString() + " kills";
        highScore2.text = "high score: " + PlayerPrefs.GetInt("HighScore").ToString() + " kills";
        Time.timeScale = 0f;
    }
}
