using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPointer : MonoBehaviour
{
    public GameObject player;
    public GameObject enemy;
    public EnemyScript enemyScript;
    public Rigidbody2D enemyRb;

    public Vector2 difference;
    public float rotZ;
    public float enemyDetectRad;

    
    public GameObject closestEnemy;
    private bool tempStatus;

    float distanceToClosestEnemy;
    GameObject[] allEnemies;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        enemyScript = enemy.GetComponent<EnemyScript>();
        enemyRb = enemy.GetComponent<Rigidbody2D>();
        closestEnemy = player;
        tempStatus = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        distanceToClosestEnemy = Mathf.Infinity;
        closestEnemy = player;

        if (enemyScript.charm)
        {
            allEnemies = GameObject.FindGameObjectsWithTag("Enemy");
            foreach (GameObject currentEnemy in allEnemies)
            {
                float distanceToEnemy = (currentEnemy.transform.position - this.transform.position).sqrMagnitude;
                if (distanceToEnemy < distanceToClosestEnemy)
                {
                    distanceToClosestEnemy = distanceToEnemy;
                    closestEnemy = currentEnemy;
                }
            }
        }

        if (enemyScript.charm && closestEnemy == player)
        {
            tempStatus = true;
        }

        if (tempStatus == false && Vector3.Distance(closestEnemy.transform.position, transform.position) < enemyDetectRad)
        {
            difference = closestEnemy.transform.position - transform.position;
            rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, rotZ);
        }
        else
        {
            if (!enemyScript.charm)
            {
                if (enemyRb.velocity.x >= 0)
                {
                    transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                }
                else if (enemyRb.velocity.x <= 0)
                {
                    transform.rotation = Quaternion.Euler(0f, 0f, 180f);
                }
            }
            else if (enemyScript.charm)
            {
                if (player.transform.position.x < enemy.transform.position.x)
                {
                    transform.rotation = Quaternion.Euler(0f, 0f, 180f);
                }
                else if (player.transform.position.x > enemy.transform.position.x)
                {
                    transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                }
            }
        }

        tempStatus = false;
    }
}
