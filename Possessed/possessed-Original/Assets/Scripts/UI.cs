using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    public GameObject howto;
    public GameObject mainMenu;
    public GameObject fade;

    public Text highScore;

    // Start is called before the first frame update
    void Start()
    {
        highScore.text = "high score: " + PlayerPrefs.GetInt("HighScore").ToString() + " kills";
    }

    // Update is called once per frame
    void Update()
    {
        highScore.text = "high score: " + PlayerPrefs.GetInt("HighScore").ToString() + " kills";
    }

    public void LoadGameScene ()
    {
        StartCoroutine(FadeNLoad());
    }

    public void OpenHowTo ()
    {
        mainMenu.SetActive(false);
        howto.SetActive(true);
    }

    public void CloseHowTo()
    {
        mainMenu.SetActive(true);
        howto.SetActive(false);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void resetHighScore()
    {
        PlayerPrefs.DeleteKey("HighScore");
    }

    IEnumerator FadeNLoad()
    {
        fade.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene("GameScene");
    }
}
