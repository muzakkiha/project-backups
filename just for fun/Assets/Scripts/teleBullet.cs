using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class teleBullet : MonoBehaviour
{
    public float speed;
    public float lifetime;

    public GameObject explosion;

    public Rigidbody2D rb;

    public GameObject player;

    public TrailRenderer trailPlayer;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Destroy());
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }



    void OnCollisionEnter2D(Collision2D collision)
    {
        Instantiate(explosion, player.transform.position, Quaternion.identity);
        player.transform.position = transform.position;
        Destroy(gameObject);
        Instantiate(explosion, transform.position, Quaternion.identity);
        player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
    }

    IEnumerator Destroy()
    {
        yield return new WaitForSeconds(lifetime);
        Instantiate(explosion, player.transform.position, Quaternion.identity);
        player.transform.position = transform.position;
        Destroy(gameObject);
        Instantiate(explosion, transform.position, Quaternion.identity);
        player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
    }
}
