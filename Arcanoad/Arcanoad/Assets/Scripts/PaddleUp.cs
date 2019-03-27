using UnityEngine;
using System.Collections;

public class PaddleUp : MonoBehaviour {

	// makes the paddle wider

	public float value;
	public float duration;
	public Rigidbody rigid;
	public ParticleBehaviour upgrade; // particle animation used after power up is picked 
	public ParticleBehaviour downgrade; // particle animation after power up duration over 
	public bool collide = false; // used to prevent the object to collide with both dead zone and paddle

	private float start = 0;

	void Start () {
		start = Time.time;
	}

	void OnTriggerEnter(Collider other) {
		PaddleBehaviour paddle = other.GetComponent<PaddleBehaviour> (); 
		if (paddle != null && !GameObject.Find ("Ball").GetComponent<BallBehaviour> ().lose && !collide) {
			Instantiate (upgrade, other.transform.position, Quaternion.identity);
			paddle.score += value;
			// to make it look like currently active effects
			gameObject.transform.position = new Vector3 (-12.9f, 5.8f, 0);
			rigid.velocity = new Vector3 (0, 0, 0);
			rigid.useGravity = false;
			gameObject.tag = "Paddle Up Active";

			paddle.paddleUp = true;
			paddle.paddleUpSpeedMod = 4f / 7f;
			paddle.transform.localScale = new Vector3 (3.5f, 0.5f, 0.5f);
			collide = true;
		}
		else if (other.GetComponent<DeadZone> () && !collide) {
			collide = true;
			gameObject.tag = "Untagged";
			Destroy (gameObject, 1f);
		}
	}

	void Update () {
		if (Time.time > start + duration) {
			GameObject.Find("Paddle").transform.localScale = new Vector3 (2f, 0.5f, 0.5f);
			GameObject.Find("Paddle").GetComponent<PaddleBehaviour> ().paddleUp = false;
			Instantiate (downgrade, GameObject.Find("Paddle").transform.position, Quaternion.identity);
			GameObject.Find("Paddle").GetComponent<PaddleBehaviour> ().paddleUpSpeedMod = 1;
			Destroy (gameObject);
		}
	}
}