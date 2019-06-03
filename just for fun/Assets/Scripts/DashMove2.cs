using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashMove2 : MonoBehaviour
{
    public Rigidbody2D rb;
    public float dashSpeed;
    private float dashTime;
    public float startDashTime;

    public bool allowDash;

    public float timeBtw;
    public float startTimeBtw;
    Vector3 refVelocity = Vector3.zero;

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
                if (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.JoystickButton9))
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
                rb.velocity = Vector3.SmoothDamp(rb.velocity, Vector2.zero, ref refVelocity, 0.04f);
            }
            else
            {
                Vector3 targetVelocity;
                if (player.flipSprite)
                {
                    targetVelocity = new Vector2(-1f, 0f) * dashSpeed;
                }
                else
                {
                    targetVelocity = new Vector2(1f, 0f) * dashSpeed;
                }
                rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref refVelocity, 0.01f);
                dashTime -= Time.deltaTime;
            }
        }
    }
}