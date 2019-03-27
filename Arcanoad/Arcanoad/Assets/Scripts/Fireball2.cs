using UnityEngine;
using System.Collections;

public class Fireball2 : MonoBehaviour {

	public Rigidbody ballRigidbody;
	public ParticleBehaviour particle;
	public float speed;
	public float initialVelocity;
	public GameObject fire;

	private float particleSpawn = 0f;
	private float particleTime = 0.03f;

	void Start () {
		particleSpawn = Time.time;
		ballRigidbody.isKinematic = false;
		ballRigidbody.AddForce (new Vector3 ((Random.value * 2.4f - 1.2f) * initialVelocity, ((Random.value * 0.3f) + 0.9f) * initialVelocity, 0));
	}

	void OnCollisionEnter (Collision other) {
		float ballPos = transform.position.x;
		if (other.gameObject.GetComponent<PaddleBehaviour> ()) {
			Vector3 direction = new Vector3 ((ballPos - other.transform.position.x) / 1.2f, (Random.value * 0.3f) + 0.9f, 0);
			ballRigidbody.velocity = direction * speed;
		}
	}
	void OnTriggerEnter(Collider other) {
		if (other.GetComponent<DeadZone> ()) {
			Destroy (gameObject);
			Instantiate (fire, transform.position, Quaternion.identity);
		}
	}

	void Update () {
		if (Time.time > particleSpawn + particleTime) {
			Instantiate (particle, transform.position, Quaternion.identity);
			particleSpawn = Time.time;
		}
	}
}