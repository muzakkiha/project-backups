using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartBehaviour : MonoBehaviour {

    public float speed;
    public float duration;

    float time;

	// Use this for initialization
	void Start () {
        time = Time.time;
		Destroy (gameObject, duration);
	}
	
	// Update is called once per frame
	void Update () {
        if (Time.time < time + 0.2f)
        {
            Vector3 position = this.transform.position;
            position.y += speed * Time.deltaTime;
            this.transform.position = position;
        }
    }
}
