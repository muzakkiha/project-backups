using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public float speed;
    public float acc;
    public float lifetime;

    public GameObject bulletSprite;
    public GameObject particleSpawner;
    public GameObject enemyCollisionEffect;
    public GameObject collisionEffect;

    public float timeBtwAcc;
    private float startTimeBtwAcc = 0;

    //public GameObject collisionEffect;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, lifetime);
    }
    
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
        Destroy(bulletSprite);
        Destroy(particleSpawner);
        transform.DetachChildren();
        Destroy(gameObject);
        Instantiate(collisionEffect, transform.position, Quaternion.identity);

        if (collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<EnemyScript>().currentLoveHealth += Random.Range(15f, 25f);
            Instantiate(enemyCollisionEffect, collision.transform.position, Quaternion.identity);
        }
        else if (collision.gameObject.tag == "Friend")
        {
            collision.gameObject.GetComponent<EnemyScript>().currentHealth += Random.Range(8f, 10f);
            Instantiate(enemyCollisionEffect, collision.transform.position, Quaternion.identity);
        }
    }
}
