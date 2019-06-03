using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvEffector : MonoBehaviour
{
    public Rigidbody2D rb;
    public float forceSpeed;
    public float maxForceSpeed;

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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 9)
        {
            Rigidbody2D rbCollision = collision.gameObject.GetComponent<Rigidbody2D>();
            Vector2 direction = (transform.position - collision.transform.position).normalized;
            Vector3 targetVelocity = new Vector2(direction.x * forceSpeed, direction.y * forceSpeed);
            rbCollision.velocity = Vector3.SmoothDamp(rbCollision.velocity, targetVelocity, ref refVelocity, 0.25f);
            rbCollision.velocity = Vector2.ClampMagnitude(rbCollision.velocity, maxForceSpeed);
        }
    }
}
