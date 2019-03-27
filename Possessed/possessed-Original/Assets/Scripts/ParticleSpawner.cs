using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSpawner : MonoBehaviour
{
    private float timeBtwShots;
    public float startTimeBtwShots;

    public GameObject particle;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (timeBtwShots <= 0)
        {
            Instantiate(particle, transform.position, Quaternion.identity);
            timeBtwShots = startTimeBtwShots;
        }
        timeBtwShots -= Time.deltaTime;
    }
}
