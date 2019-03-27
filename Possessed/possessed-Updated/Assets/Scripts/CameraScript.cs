using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public GameObject player;

    public float shakeDuration = 0f;
    public float shakeAmount = 0.05f;
    public float decreaseFactor = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }
    
    void FixedUpdate()
    {
        Vector3 playerPosition = player.transform.position;
        transform.position = Vector3.Lerp(transform.position, new Vector3(playerPosition.x, playerPosition.y, transform.position.z), 0.04f);

        if (shakeDuration > 0f)
        {
            transform.localPosition = transform.position + Random.insideUnitSphere * shakeAmount;
            shakeDuration -= Time.deltaTime * decreaseFactor;
        }
    }

    public void Shake()
    {
        this.shakeDuration = 0.1f;
    }

    public void Shake(float duration)
    {
        this.shakeDuration = duration;
    }
}
