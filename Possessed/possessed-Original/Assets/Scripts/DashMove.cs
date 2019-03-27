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
    public Transform pointer;

    public GameObject[] dashFill;
    public int count;
    private float timeBtw;
    public float startTimeBtw;

    public SpriteRenderer[] sp;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        allowDash = true;
        count = 3;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        for (int i = 0; i < 3; i++)
        {
            dashFill[i].SetActive(false);
        }

        for (int i = 0; i < count; i++)
        {
            dashFill[i].SetActive(true);
        }

        if (timeBtw <= 0 && count < 3)
        {
            timeBtw = startTimeBtw;
            count++;
        }
        else
        {
            timeBtw -= Time.deltaTime;
        }

        if (count >= 3)
        {
            timeBtw = startTimeBtw;
        }

        if (!allowDash)
        {
            if (Input.GetMouseButtonDown(1  ) && count > 0)
            {
                allowDash = true;
                count--;
            }
        }

        else
        {
            if (dashTime <= 0)
            {
                allowDash = false;
                dashTime = startDashTime;
                rb.velocity = Vector2.zero;
                for (int i = 0; i < sp.Length; i++)
                {
                    sp[i].color = new Color(255, 255, 255, 235);
                }
            }
            else
            {
                dashTime -= Time.deltaTime;

                Vector2 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
                rb.velocity = new Vector2(Mathf.Clamp(difference.x, -2f, 2f) * dashSpeed, Mathf.Clamp(difference.y, -2f, 2f) * dashSpeed);
                if (difference.x > 0)
                {
                    gameObject.GetComponent<PlayerScript>().flip = false;
                }
                else if (difference.x < 0)
                {
                    gameObject.GetComponent<PlayerScript>().flip = true;
                }
                    for (int i = 0; i < sp.Length; i++)
                {
                    sp[i].color = new Color(255, 255, 255, 0.1f);
                }
            }
        }
    }
}
