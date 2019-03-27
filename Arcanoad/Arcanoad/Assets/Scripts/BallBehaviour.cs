using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class BallBehaviour : MonoBehaviour {

	public float initialVelocity;
	public ParticleBehaviour particle;
	public float speed;
	public Text gameOverText;
	public Text gameOverText2;
	public Text infoText;
	public bool lose = false;
	public bool win = false;
	public Rigidbody ballRigidbody;

	private bool startParticle = false;
	private bool ballInPlay;
	private float particleSpawn = 0f;
	private float particleTime = 0.02f;

	float paddlePos;
	float ballPos;

	// for full documentation about unity behavior lifecycle
	// see http://docs.unity3d.com/Manual/ExecutionOrder.html
	void Awake () {
		// Rigidbody component give the object physics
		// properties like mass, drag, etc. We can interact
		// with the object using rigidbody by using physics
		// properties, like throwing a ball is essentially
		// add force to ball to specific direction
		ballRigidbody = GetComponent<Rigidbody>();
	}

	void Update () {
		if (Input.GetKeyDown (KeyCode.Space) && ballInPlay) {
			Time.timeScale = 1f;
			SceneManager.LoadScene ("Main");
		}
		/*------------
		 * Input
		 * -----------*/
		// The game start when player press the left mouse button
		// or press left ctrl key on the keyboard
		// and the ball is NOT in playmode
		if (Input.GetKeyDown (KeyCode.Space) && !ballInPlay) {
			transform.parent = null;
			Destroy(GameObject.Find ("Fiery"));
			startParticle = true;

			// TODO : Change the ballInPlay bool to true
			// ---[Your code here]---
			ballInPlay = true;
			ballRigidbody.isKinematic = false;

			// TODO : Add some upward force using initialVelocity to the ball's rigidbody
			// ---[Your code here]---
			if (Random.value > 0.5f) {
				ballRigidbody.AddForce (new Vector3 (Random.value * 1.2f * initialVelocity, ((Random.value * 0.3f) + 0.9f) * initialVelocity, 0));
			}
			else {
				ballRigidbody.AddForce (new Vector3 (Random.value * -1.2f * initialVelocity, ((Random.value * 0.3f) + 0.9f) * initialVelocity, 0));
			}
			infoText.text = "Press \"Space\" to Restart";
		}
		if (Time.time > particleSpawn + particleTime && startParticle) {
			Instantiate (particle, transform.position, Quaternion.identity);
			particleSpawn = Time.time;
		}
	}
		
	void OnCollisionEnter (Collision other) {
		float ballPos = transform.position.x;
		if (other.gameObject.GetComponent<PaddleBehaviour> ()) {
			Vector3 direction = new Vector3 ((ballPos - other.transform.position.x) * GameObject.Find("Paddle").GetComponent<PaddleBehaviour> ().paddleUpSpeedMod, (Random.value * 0.3f) + 0.9f, 0);
			ballRigidbody.velocity = direction * speed;
		}
	}

	void OnTriggerEnter(Collider other) { // lose condition
		if (other.GetComponent<DeadZone> () && !win && !GameObject.Find("Paddle").GetComponent<PaddleBehaviour> ().wait) {
			ballRigidbody.velocity = new Vector3 (0, 0, 0);
			ballRigidbody.useGravity = false;
			lose = true;
			gameOverText.text = "YOU LOSE";
			gameOverText2.text = "Your score: " + GameObject.Find ("Paddle").GetComponent<PaddleBehaviour> ().score + "\nPress \"Space\" to Restart";
			GameObject.Find ("Info Text").SetActive(false);
			GameObject.Find ("Score Text").SetActive(false);
		}
		if (other.GetComponent<DeadZone> () && !win && GameObject.Find ("Paddle").GetComponent<PaddleBehaviour> ().wait) {
			ballRigidbody.velocity = new Vector3 (0, 0, 0);
			ballRigidbody.useGravity = false;
		}
		if (other.GetComponent<DeadZone> () && win) {
			ballRigidbody.velocity = new Vector3 (0, 0, 0);
			ballRigidbody.useGravity = false;
		}
	}
}