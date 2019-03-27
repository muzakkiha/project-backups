using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    
    public GameObject enemy;
    private float timer;

    public float spawnTimeMin;
    public float spawnTimeMax;
    public float spawnTime;

    public Rigidbody2D rb;

    public float maxSpeed = 5f;
    private float delay;
    private Vector2 movement;

    public GameObject spawnEffect;

    public GameManager gm;

    void Start()
    {
        spawnTime = Random.Range(spawnTimeMin, spawnTimeMax);
        timer = spawnTime;
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        this.timer -= Time.deltaTime;

        if (timer < 0 && gm.enemyCount < gm.enemyLimit)
        {
            Instantiate(enemy, transform.position, Quaternion.identity);
            timer = Random.Range(8f, 12f);
            gm.enemyCount += 1;
            Instantiate(spawnEffect, transform.position, Quaternion.identity);
        }

        delay -= Time.deltaTime;
        if (delay <= 0)
        {
            movement = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
            rb.velocity = movement * maxSpeed;
            delay = Random.Range(0.5f, 2f);
        }
    }
}
