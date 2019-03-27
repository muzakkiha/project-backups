using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TargetBehaviour : MonoBehaviour {

    // Heart & 'Full' text
    public GameObject heart;
    public GameObject full;
    Timer timer;
    Score score;

    public float maxMood = 100f;
    public float minMood = 0f;

    public float minDecreaseRate = 5f;
    public float maxDecreaseRate = 10f;

    public float minStartMood = 30f;
    public float maxStartMood = 70f;

    public float mood;

    public GameObject moodBar;
    public Spawner spawner;

    public bool alive;

    public int increaseScore;

    private float decreaseRate;
    private float increaseRate;

    bool wait;
    private bool increasing;

    // Use this for initialization
    void Start () {

        #region energy bar display decrease logic | try to not modify this section
        decreaseRate = Random.Range(minDecreaseRate, maxDecreaseRate);
		mood = Random.Range(minStartMood, maxStartMood);
        Vector3 moodBarScale = moodBar.transform.localScale;
        moodBar.transform.localScale = new Vector3(Mathf.Clamp(mood / maxMood, 0f, 1f), moodBarScale.y, moodBarScale.z);
        increaseRate = GameObject.Find("Player").GetComponent<PlayerBehaviour>().charm;

        spawner = GameObject.Find("Spawner").GetComponent<Spawner>();
        #endregion
        timer = GameObject.Find("Timer").GetComponent<Timer>();
        score = GameObject.Find("Score").GetComponent<Score>();
    }

    // Update is called once per frame
    void Update () {
        if (!alive)
            return;
        #region energy bar value modifier logic
        float prevMood = mood;

        //if bool increasing then energy bar increases according to rate and time
        //else it decreases instead
        if (increasing) {
            mood = Mathf.Clamp(mood + increaseRate * Time.deltaTime, 0f, maxMood);
        } else {
            mood = Mathf.Clamp(mood - decreaseRate * Time.deltaTime, 0f, maxMood);
        }

        //apply numerical values to graphical display by transforming the moodbar scale
        Vector3 moodBarScale = moodBar.transform.localScale;
        moodBar.transform.localScale = new Vector3(Mathf.Clamp(mood / maxMood, 0f, 1f), moodBarScale.y, moodBarScale.z);
        #endregion

        if (mood != prevMood) {
            if (mood == minMood) {
                //despawns target if energy bar is empty
                spawner.despawn(gameObject);
                alive = false;
            } else if (mood == maxMood) {
                //TO DO buat target memunculkan sebuah tanda ketika meteran mereka penuh
                //your code here
                GameObject heartSpawn = Instantiate(heart, transform.position, Quaternion.identity);
                heartSpawn.transform.parent = gameObject.transform;
                // ada kemungkinan target untuk keluar
                if (Random.Range(0f, 5f) > 4f)
                {
                    spawner.despawn(gameObject);
                    alive = false;
                }
                full.SetActive(true);
                StartCoroutine(Hold());
                timer.increaseTheTime();
                score.score += increaseScore;
            }
        }
    }

    //TODO
    //implementasi supaya meteran target bertambah ketika didekati player
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "Player" && !wait)
        {
            increasing = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.name == "Player" && !wait)
        {
            increasing = false;
        }
    }

    IEnumerator Hold()
    {
        wait = true;
        yield return new WaitForSeconds(5);
        full.SetActive(false);
        wait = false;
        increasing = false;
    }
}
