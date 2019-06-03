using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashMove3 : MonoBehaviour
{
    public Rigidbody2D rb;
    public float dashSpeed;
    private float dashTime;
    public float startDashTime;

    public bool allowDash;

    private float timeBtw;
    public float startTimeBtw;
    Vector3 refVelocity = Vector3.zero;

    Player player;
    public GameObject explosion;

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
                if (Input.GetKey(KeyCode.JoystickButton1) || Input.GetKey(KeyCode.LeftControl))
                {
                    allowDash = true;
                    timeBtw = startTimeBtw;
                    GameObject explo = Instantiate(explosion, transform.position, Quaternion.identity);
                    explo.transform.SetParent(player.transform);    
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
            }
            else
            {
                Vector3 targetVelocity;
                if (player.isGrounded)
                {
                    targetVelocity = new Vector2(Input.GetAxis("Horizontal"), 0f) * dashSpeed;
                    rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref refVelocity, 0.8f);
                }

                dashTime -= Time.deltaTime;
            }
        }
    }
}
