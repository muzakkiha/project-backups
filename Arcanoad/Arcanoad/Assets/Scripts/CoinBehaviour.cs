using UnityEngine;
using System.Collections;

public class CoinBehaviour : MonoBehaviour {

	public float value;

	// Add coin to score and then destroy
	// when the coin touches the paddle 
	void OnTriggerEnter(Collider other) {
		PaddleBehaviour paddle = other.GetComponent<PaddleBehaviour> (); 
		if (paddle != null && !GameObject.Find ("Ball").GetComponent<BallBehaviour> ().lose) {
			Destroy (gameObject);
			paddle.score += value;
		}
		if (other.GetComponent<DeadZone> ()) {
			gameObject.tag = "Untagged";
			Destroy (gameObject, 0.4f);
		}
	}

	void Update () {
		RotateCoin ();
	}

	// Rotating coin
	void RotateCoin () {
		transform.RotateAround (transform.position, Vector3.left, 10);
	}
}