using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashMove : MonoBehaviour
{
    public Rigidbody2D rb;
    public float dashSpeed;
    private float dashTime;
    public float startDashTime;

    public bool allowDash;

    private float timeBtw;
    public float startTimeBtw;
    Vector3 refVelocity = Vector3.zero;

    public GameObject[] spActive;
    public GameObject[] spNotActive;

    Player player;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        allowDash = true;
        player = GetComponent<Player>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if (!allowDash)
        {
            if (timeBtw <= 0)
            {
                if (Input.GetKey(KeyCode.JoystickButton2) || Input.GetKey(KeyCode.E))
                {
                    allowDash = true;
                    timeBtw = startTimeBtw;
                }
            }
            else
            {
                timeBtw -= Time.deltaTime;
            }
        }

        else
        {
            if (dashTime <= 0)
            {
                allowDash = false;
                dashTime = startDashTime;
                rb.velocity = Vector3.SmoothDamp(rb.velocity, Vector2.zero, ref refVelocity, 0.02f);
                for (int i = 0; i < spActive.Length; i++)
                {
                    spActive[i].SetActive(false);
                }
                for (int i = 0; i < spNotActive.Length; i++)
                {
                    spNotActive[i].SetActive(true);
                }
                rb.gravityScale = 0.7f;
            }
            else
            {
                Vector3 targetVelocity;
                if (Input.GetAxis("Horizontal") == 0f && Input.GetAxis("Vertical") == 0f)
                {
                    if (player.flipSprite)
                    {
                        targetVelocity = new Vector2(-1f, Random.Range(-0.2f, 0.2f)).normalized * dashSpeed;
                        rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref refVelocity, 0.1f);
                    }
                    else
                    {
                        targetVelocity = new Vector2(1f, Random.Range(-0.2f, 0.2f)).normalized * dashSpeed;
                        rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref refVelocity, 0.1f);
                    }
                }
                else
                {
                    Vector2 difference = new Vector2(Input.GetAxis("Horizontal"), -1f * Input.GetAxis("Vertical")) - Vector2.zero;
                    targetVelocity = new Vector2(difference.x + Random.Range(-0.2f, 0.2f), difference.y + Random.Range(-0.2f, 0.2f)).normalized * dashSpeed;
                    rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref refVelocity, 0.1f);
                }

                dashTime -= Time.deltaTime;
                for (int i = 0; i < spActive.Length; i++)
                {
                    spActive[i].SetActive(true);
                }
                for (int i = 0; i < spNotActive.Length; i++)
                {
                    spNotActive[i].SetActive(false);
                }
                rb.gravityScale = 0f;
            }
        }
    }
}