using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public float maxSpeed = 5f;
    private float delay;
    private Vector2 movement;

    public GameObject player;
    public PlayerScript playerScript;
    public int circlingPos;

    public Rigidbody2D rb;

    public float loveHealth;
    public float currentLoveHealth;

    private float timeBtwDecay;
    public float startTimeBtwDecay;
    public float decayRate;

    public GameObject loveFill;
    public bool charm;

    public float maxHealth;
    public float currentHealth;
    public GameObject healthFill;

    private float timeBtwShots;
    public float startTimeBtwShots;
    public Transform shotPoint;
    public Transform pointer;
    public GameObject projectileAsEnemy;
    public GameObject projectileAsFriend;

    public GameManager gm;

    public GameObject enemySprite;
    public GameObject friendSprite;

    public GameObject destroyedFxEnemy;
    public GameObject destroyedFxFriend;

    public GameObject healFx;
    public GameObject changeFx;
    public GameObject changeFxInv;

    // Start is called before the first frame update
    void Start()
    {
        currentLoveHealth = 0f;
        currentHealth = 100f;
        charm = false;
        player = GameObject.Find("Player");
        timeBtwShots = startTimeBtwShots + Random.Range(-1f, 1f);
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (charm)
        {
            Vector2 difference = playerScript.circling[circlingPos].transform.position - transform.position;
            if (Mathf.Sqrt(difference.sqrMagnitude) > 0.8f)
            {
                transform.position = Vector3.Lerp(transform.position, playerScript.circling[circlingPos].transform.position, 0.015f);
            }
        }

        delay -= Time.deltaTime;
        if (delay <= 0 && !charm)
        {
            movement = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
            rb.velocity = movement * maxSpeed;
            delay = Random.Range(0.5f, 4f);
        }

        if (!charm) { 
            if (timeBtwDecay <= 0)
            {
                currentLoveHealth -= decayRate;
                timeBtwDecay = startTimeBtwDecay;
            }
            else
            {
                timeBtwDecay -= Time.deltaTime;
            }
        }

        loveFill.transform.localScale = new Vector2(currentLoveHealth / 100, loveFill.transform.localScale.y);
        if (currentLoveHealth <= 0 && !charm){
            currentLoveHealth = 0;
        }

        if (currentLoveHealth >= 100 && !charm){
            currentLoveHealth = 100;
            charm = true;
            loveFill.GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 0);
            gameObject.tag = "Friend";
            gameObject.layer = 14;
            gm.enemyCount -= 1;
            playerScript = player.GetComponent<PlayerScript>();
            enemySprite.SetActive(false);
            friendSprite.SetActive(true);
            bool check = false;
            for (int i = 0; i < playerScript.circlingStatus.Length; i++)
            {
                if (!playerScript.circlingStatus[i])
                {
                    circlingPos = i;
                    playerScript.circlingStatus[i] = true;
                    playerScript.circlingObject[i] = gameObject;
                    check = true;
                    break;
                }
            }
            if (!check)
            {
                EnemyScript circlingObj = playerScript.circlingObject[0].GetComponent<EnemyScript>();
                circlingObj.charm = false;
                circlingObj.loveFill.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 1f);
                playerScript.circlingObject[0].tag = "Enemy";
                playerScript.circlingObject[0].layer = 10;
                circlingObj.enemySprite.SetActive(true);
                circlingObj.friendSprite.SetActive(false);
                gm.enemyCount += 1;
                currentLoveHealth = 0f;
                circlingPos = 0;
                GameObject cfxinv = Instantiate(changeFxInv, playerScript.circlingObject[0].transform.position, Quaternion.identity);
                cfxinv.transform.SetParent(playerScript.circlingObject[0].transform);
                playerScript.circlingObject[0] = gameObject;
                playerScript.circlingStatus[0] = true;
            }
            GameObject cfx = Instantiate(changeFx, transform.position, Quaternion.identity);
            cfx.transform.SetParent(gameObject.transform);
        }

        /*if (currentLoveHealth <= 0 && charm){
            currentLoveHealth = 0;
            charm = false;
            loveFill.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 255);
            gameObject.tag = "Enemy";
            gameObject.layer = 10;
        }*/

        /*if (currentLoveHealth >= 100 && charm){
            currentLoveHealth = 100;
        }*/

        healthFill.transform.localScale = new Vector2(currentHealth / 100, healthFill.transform.localScale.y);

        if (timeBtwShots <= 0 && !(pointer.GetComponent<EnemyPointer>().closestEnemy == null) && Vector3.Distance(pointer.GetComponent<EnemyPointer>().closestEnemy.transform.position, pointer.transform.position) < pointer.GetComponent<EnemyPointer>().enemyDetectRad)
        {
            if (!charm)
            {
                Instantiate(projectileAsEnemy, shotPoint.position, pointer.transform.rotation);
            }
            else if (charm)
            {
                Instantiate(projectileAsFriend, shotPoint.position, pointer.transform.rotation);
            }
            if (!charm)
            {
                timeBtwShots = startTimeBtwShots + Random.Range(-0.5f, 0.5f);
            }
            else if (charm)
            {
                timeBtwShots = (startTimeBtwShots + Random.Range(-0.5f, 0.5f)) * 0.6f;
            }
        }
        else
        {
            timeBtwShots -= Time.deltaTime;
        }

        if (currentHealth <= 0)
        {
            GameObject.Find("Main Camera").GetComponent<CameraScript>().Shake(0.05f);
            Destroy(gameObject);
            gm.enemyCount -= 1;
            player.GetComponent<PlayerScript>().currentHealth += Random.Range(5f, 8f);
            GameObject healObj = Instantiate(healFx, player.transform.position, Quaternion.identity);
            healObj.transform.SetParent(player.transform);
            gm.kills += 1;
            if (charm)
            {
                playerScript.circlingStatus[circlingPos] = false;
                playerScript.circlingObject[circlingPos] = null;
                Instantiate(destroyedFxFriend, transform.position, Quaternion.identity);
            }
            else if (!charm)
            {
                Instantiate(destroyedFxEnemy, transform.position, Quaternion.identity);
            }
        }

        if (currentHealth >= 100f)
        {
            currentHealth = 100f;
        }
    }
}
