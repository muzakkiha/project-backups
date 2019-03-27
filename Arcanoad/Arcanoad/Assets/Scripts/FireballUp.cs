using UnityEngine;
using System.Collections;

public class FireballUp : MonoBehaviour {

	// launch a powerful fireball

	public float value;
	public GameObject fireball;
	public ParticleBehaviour upgrade; // particle animation used after power up is picked 

	void OnTriggerEnter(Collider other) {
		PaddleBehaviour paddle = other.GetComponent<PaddleBehaviour> (); 
		if (paddle != null && !GameObject.Find ("Ball").GetComponent<BallBehaviour> ().lose) {
			paddle.score += value;
			Destroy (gameObject);
			Instantiate (fireball, transform.position, Quaternion.identity);
			Instantiate (upgrade, transform.position, Quaternion.identity);
		}
		if (other.GetComponent<DeadZone> ()) {
			gameObject.tag = "Untagged";
			Destroy (gameObject, 1f);
		}
	}
}