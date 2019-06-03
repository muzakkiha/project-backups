using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ringOfSpeed : MonoBehaviour
{
    public Rigidbody2D rb;

    Vector3 refVelocity = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 9)
        {
            Rigidbody2D rbCollision = collision.gameObject.GetComponent<Rigidbody2D>();
            rbCollision.gravityScale = 0.15f;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 9)
        {
            Rigidbody2D rbCollision = collision.gameObject.GetComponent<Rigidbody2D>();
            rbCollision.gravityScale = 0.7f;
        }
    }
}