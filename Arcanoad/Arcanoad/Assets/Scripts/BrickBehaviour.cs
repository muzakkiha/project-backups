using UnityEngine;
using System.Collections;

public class BrickBehaviour : MonoBehaviour {

	public CoinBehaviour goldenPrefab;
	public CoinBehaviour silverPrefab;
	public CoinBehaviour badCoinPrefab;
	public FireballUp fireball;
	public Fireball1 fireball1;
	public Fireball1 fireball2;
	public Fireball1 fireball3;
	public Fireball1 fireball4;
	public PaddleUp paddleUp;
	public Rage rage;
	public ParticleBehaviour particle;
	public ParticleBehaviour fireballParticle;
	public float value;
	public float fireballValue;

	private float luck = 0;

	// Destroy when collide with the ball
	void OnCollisionEnter (Collision other) {
		//Check if other's gameobject is Ball object
		if (other.gameObject.GetComponent<BallBehaviour> ()) {
			Instantiate (particle, transform.position, Quaternion.identity);
			Destroy (gameObject);
			luck = Random.value;
			// If the random value bigger than 0.5
			// then instantiate Coin prefab
			if (luck > 0.5f && luck < 0.6f) {
				// TODO : Do instantiate coin prefab
				// ---[Your code here]---
				Instantiate (goldenPrefab, transform.position, Quaternion.identity);
			}
			if (luck > 0.6f) {
				Instantiate (silverPrefab, transform.position, Quaternion.identity);
			}
			if (luck > 0.425f && luck < 0.5f) {
				Instantiate (fireball, transform.position, Quaternion.identity);
			}
			if (luck > 0.35f && luck < 0.425f) {
				Instantiate (badCoinPrefab, transform.position, Quaternion.identity);
			}
			if (luck > 0.25f && luck < 0.3f && GameObject.FindGameObjectsWithTag ("Paddle Up Active").Length < 1 && GameObject.FindGameObjectsWithTag ("Paddle Up").Length < 1) {
				Instantiate (paddleUp, transform.position, Quaternion.identity);
			}
			if (luck > 0.3f && luck < 0.35f && GameObject.FindGameObjectsWithTag ("Rage Active").Length < 1 && GameObject.FindGameObjectsWithTag ("Rage").Length < 1) {
				Instantiate (rage, transform.position, Quaternion.identity);
			}
			GameObject.Find("Paddle").GetComponent<PaddleBehaviour> ().score += value;
		}
		if (other.gameObject.GetComponent<Fireball> ()) {  // Fireball Power Up ball
			Instantiate (fireballParticle, transform.position, Quaternion.identity);
			// initiate fire splash attack
			Instantiate (fireball1, transform.position, Quaternion.identity);
			Instantiate (fireball2, transform.position, Quaternion.identity);
			Instantiate (fireball3, transform.position, Quaternion.identity);
			Instantiate (fireball4, transform.position, Quaternion.identity);
			Destroy (other.gameObject);
			if (luck > 0.5f && luck < 0.6f) {
				Instantiate (goldenPrefab, transform.position, Quaternion.identity);
			}
			if (luck > 0.6f) {
				Instantiate (silverPrefab, transform.position, Quaternion.identity);
			}
			if (luck > 0.425f && luck < 0.5f) {
				Instantiate (fireball, transform.position, Quaternion.identity);
			}
			if (luck > 0.35f && luck < 0.425f) {
				Instantiate (badCoinPrefab, transform.position, Quaternion.identity);
			}
			if (luck > 0.25f && luck < 0.3f && GameObject.FindGameObjectsWithTag ("Paddle Up Active").Length < 1 && GameObject.FindGameObjectsWithTag ("Paddle Up").Length < 1) {
				Instantiate (paddleUp, transform.position, Quaternion.identity);
			}
			if (luck > 0.3f && luck < 0.35f && GameObject.FindGameObjectsWithTag ("Rage Active").Length < 1 && GameObject.FindGameObjectsWithTag ("Rage").Length < 1) {
				Instantiate (rage, transform.position, Quaternion.identity);
			}
			GameObject.Find("Paddle").GetComponent<PaddleBehaviour> ().score += fireballValue;
		}
		// TODO : Destroy brick gameObject
		// ---[Your code here]---
		Destroy (gameObject);
	}

	void OnTriggerEnter (Collider other) {
		if (other.gameObject.GetComponent<Fireball1> ()) {  // Fireball Power Up ball
			Instantiate (fireballParticle, transform.position, Quaternion.identity);
			Destroy (other.gameObject);
			Destroy (gameObject);
			if (luck > 0.5f && luck < 0.6f) {
				Instantiate (goldenPrefab, transform.position, Quaternion.identity);
			}
			if (luck > 0.6f) {
				Instantiate (silverPrefab, transform.position, Quaternion.identity);
			}
			if (luck > 0.425f && luck < 0.5f) {
				Instantiate (fireball, transform.position, Quaternion.identity);
			}
			if (luck > 0.35f && luck < 0.425f) {
				Instantiate (badCoinPrefab, transform.position, Quaternion.identity);
			}
			if (luck > 0.25f && luck < 0.3f && GameObject.FindGameObjectsWithTag ("Paddle Up Active").Length < 1 && GameObject.FindGameObjectsWithTag ("Paddle Up").Length < 1) {
				Instantiate (paddleUp, transform.position, Quaternion.identity);
			}
			if (luck > 0.3f && luck < 0.35f && GameObject.FindGameObjectsWithTag ("Rage Active").Length < 1 && GameObject.FindGameObjectsWithTag ("Rage").Length < 1) {
				Instantiate (rage, transform.position, Quaternion.identity);
			}
			GameObject.Find ("Paddle").GetComponent<PaddleBehaviour> ().score += fireballValue;
		}
	}
}