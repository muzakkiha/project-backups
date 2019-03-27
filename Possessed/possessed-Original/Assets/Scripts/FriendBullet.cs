using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriendBullet : MonoBehaviour
{
    public float speed;
    public float acc;
    public float lifetime;

    public GameObject bulletSprite;

    public float timeBtwAcc;
    private float startTimeBtwAcc = 0;


    public GameObject particle1;

    public GameObject collisionEffect;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, lifetime);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);

        if (timeBtwAcc <= 0)
        {
            timeBtwAcc = startTimeBtwAcc;
            speed += acc;
        }

        else
        {
            timeBtwAcc -= Time.deltaTime;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Instantiate(collisionEffect, transform.position, Quaternion.identity);

        Destroy(bulletSprite);
        Destroy(particle1);
        transform.DetachChildren();
        Destroy(gameObject);

        if (collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<EnemyScript>().currentHealth -= Random.Range(25f, 35f);
        }
    }
}
