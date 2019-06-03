using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    public float speed;
    public float lifetime;

    public GameObject explosion;

    public Rigidbody2D rb;

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
        Destroy(gameObject);
        Instantiate(explosion, transform.position, Quaternion.identity);
    }

    IEnumerator Destroy()
    {
        yield return new WaitForSeconds(lifetime);
        Destroy(gameObject);
        Instantiate(explosion, transform.position, Quaternion.identity);
    }
}
