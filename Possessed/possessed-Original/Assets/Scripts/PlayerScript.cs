using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerScript : MonoBehaviour
{

    public Rigidbody2D rb;

    public float speed;

    private float timeBtwShots;
    public float startTimeBtwShots;
    public Transform shotPoint;
    public Transform pointer;
    public GameObject projectile;
    public GameObject playerSprite;
    public GameObject muzzle;

    public float charge;

    public float maxHealth;
    public float currentHealth;
    public GameObject healthFill;

    public float dashBar;

    public Vector2 difference;
    public bool flip;

    public GameObject[] circling;
    public bool[] circlingStatus;
    public GameObject[] circlingObject;

    public GameManager gm;
    public GameObject almostDead;


    // Start is called before the first frame update
    void Start()
    {
        timeBtwShots = startTimeBtwShots;
        currentHealth = 100f;
        flip = false;
        circlingStatus = new bool[4];
        for (int i = 0; i < circlingStatus.Length; i++)
        {
            circlingStatus[i] = false;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //rotation lock
        /*if (pointer.transform.eulerAngles.z >= 60f && pointer.transform.eulerAngles.z <= 90f){
            pointer.transform.eulerAngles = new Vector3(0f, 0f, 60);}
        if (pointer.transform.eulerAngles.z <= 120f && pointer.transform.eulerAngles.z >= 90f){
            pointer.transform.eulerAngles = new Vector3(0f, 0f, 120);}
        if (pointer.transform.eulerAngles.z <= 300f && pointer.transform.eulerAngles.z >= 270f){
            pointer.transform.eulerAngles = new Vector3(0f, 0f, 300);}
        if (pointer.transform.eulerAngles.z >= 240f && pointer.transform.eulerAngles.z <= 270f){
            pointer.transform.eulerAngles = new Vector3(0f, 0f, 240);}*/

        if (timeBtwShots <= 0)
        {
            if (Input.GetMouseButton(0) && !(EventSystem.current.IsPointerOverGameObject()))
            {
                Instantiate(projectile, shotPoint.position, pointer.transform.rotation);
                timeBtwShots = startTimeBtwShots;
                Instantiate(muzzle, shotPoint.position, Quaternion.identity);
            }
        }
        else {
            timeBtwShots -= Time.deltaTime;
        }

        healthFill.transform.localScale = new Vector2(currentHealth / 100, healthFill.transform.localScale.y);

        if (currentHealth >= 100f)
        {
            currentHealth = 100f;
        }
        if (currentHealth <= 0f)
        {
            currentHealth = 0f;
        }

        if (currentHealth <= 25f)
        {
            almostDead.SetActive(true);
        }
        else
        {
            almostDead.SetActive(false);
        }

        //difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;

        /*if (difference.x >= 0)
        {
            flip = false;
        }
        else if (difference.x <= 0)
        {
            flip = true;
        }*/

        if (flip)
        {
            playerSprite.transform.localScale = Vector3.Lerp(playerSprite.transform.localScale, new Vector2(0.27f, playerSprite.transform.localScale.y), 0.5f);
        }
        else if (!flip)
        {
            playerSprite.transform.localScale = Vector3.Lerp(playerSprite.transform.localScale, new Vector2(-0.27f, playerSprite.transform.localScale.y), 0.5f);
        }

        float directionx = Input.GetAxis("Horizontal");
        float directiony = Input.GetAxis("Vertical");

        if (directionx > 0)
        {
            flip = false;
        }
        else if (directionx < 0)
        {
            flip = true;
        }
        float newX = transform.position.x + (speed * directionx * Time.deltaTime);
        float newY = transform.position.y + (speed * directiony * Time.deltaTime);
        transform.position = Vector3.Lerp(transform.position, new Vector3(newX, newY, transform.position.z), 1f);
    }
}
