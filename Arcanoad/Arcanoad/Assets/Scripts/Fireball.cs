using UnityEngine;
using System.Collections;

public class Fireball : MonoBehaviour {
	
	public Rigidbody ballRigidbody;
	public ParticleBehaviour particle;
	public float speed;
	public float initialSpeed;
	public GameObject fire;

	private float particleSpawn = 0f;
	private float particleTime = 0.02f;

	void Start () {
		particleSpawn = Time.time;
		ballRigidbody.isKinematic = false;
		Vector3 direction = new Vector3 ((transform.position.x - GameObject.Find("Paddle").transform.position.x) * GameObject.Find("Paddle").GetComponent<PaddleBehaviour> ().paddleUpSpeedMod, (Random.value * 0.3f) + 0.9f, 0);
		ballRigidbody.velocity = direction * initialSpeed;
	}

	void OnCollisionEnter (Collision other) {
		float ballPos = transform.position.x;
		if (other.gameObject.GetComponent<PaddleBehaviour> ()) {
			Vector3 direction = new Vector3 ((ballPos - other.transform.position.x) * GameObject.Find("Paddle").GetComponent<PaddleBehaviour> ().paddleUpSpeedMod, (Random.value * 0.3f) + 0.9f, 0);
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
			Destroy (gameObject, 1.5f);
		}
	}
}