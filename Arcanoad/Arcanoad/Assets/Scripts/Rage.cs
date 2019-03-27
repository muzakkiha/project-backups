using UnityEngine;
using System.Collections;

public class Rage : MonoBehaviour {

	// slows down everything except the paddle

	public float value;
	public float duration;
	public Rigidbody rigid;
	public ParticleBehaviour upgrade; // particle animation used after power up is picked 
	public GameObject redLight; // cinematic red screen
	public bool collide = false; // used to prevent the object to collide with both dead zone and paddle

	private float start = 0;

	void Start () {
		start = Time.time;
	}

	void OnTriggerEnter(Collider other) {
		PaddleBehaviour paddle = other.GetComponent<PaddleBehaviour> (); 
		if (paddle != null && !GameObject.Find ("Ball").GetComponent<BallBehaviour> ().lose && !collide) {
			Instantiate (upgrade, transform.position, Quaternion.identity);
			Instantiate (redLight, transform.position, Quaternion.Euler (-64, 29, 107));
			paddle.score += value;

			gameObject.transform.position = new Vector3 (14.7f, 5.8f, 0);
			rigid.velocity = new Vector3 (0, 0, 0);
			rigid.useGravity = false;
			gameObject.tag = "Rage Active";

			Time.timeScale = 0.7f;
			paddle.paddleSpeed = paddle.paddleSpeed * 10 / 7;
			collide = true;
		}
		if (other.GetComponent<DeadZone> () && !collide) {
			collide = true;
			gameObject.tag = "Untagged";
			Destroy (gameObject, 1f);
		}
	}

	void Update () {
		if (Time.time > start + duration) {
			GameObject.Find("Paddle").GetComponent<PaddleBehaviour> ().paddleSpeed = 0.5f;
			Destroy (GameObject.FindGameObjectWithTag("Red Light"));
			Time.timeScale = 1f;
			Destroy (gameObject);
		}
	}
}