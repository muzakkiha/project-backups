using UnityEngine;
using System.Collections;

public class Fireball1 : MonoBehaviour { // fire splash attack after the fireball hits a brick 

	public Rigidbody ballRigidbody;
	public ParticleBehaviour particle;
	public float initialVelocity;
	public float x;
	public float y;

	private float particleSpawn = 0f;
	private float particleTime = 0.04f;

	void Start () {
		particleSpawn = Time.time;
		ballRigidbody.isKinematic = false;
		ballRigidbody.AddForce (new Vector3 (x * initialVelocity, y * initialVelocity, 0));
	}

	void Update () {
		if (Time.time > particleSpawn + particleTime) {
			Instantiate (particle, transform.position, Quaternion.identity);
			particleSpawn = Time.time;
			Destroy (gameObject, 0.12f);
		}
	}
}