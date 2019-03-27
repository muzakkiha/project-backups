using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PaddleBehaviour : MonoBehaviour {

	// public variable will shown in inspector
	// so the value can be changed and different 
	// for all object that using same scripts.
	// It's good practice to give it default value
	public float paddleSpeed;
	public Text scoreText;
	public float xPos = 0;
	public bool paddleUp = false;
	public float score = 0;
	public float paddleUpSpeedMod = 1f;
	public float winScore;
	public bool win = false;
	public bool wait = false;

	// private variable won't shown in inspector
	// the usage is limited to this script
	private Vector3 paddlePos = new Vector3 (1.25f, -7.07f, 0);
	private float start = 0;

	void Update () {
		/*------------
		 * Movement
		 * -----------*/
		// Player can control the paddle in x Axis
		// Input.GetAxis("Horizontal") is mapped to left
		// and right arrows key on the player keyboard
		float xPos = transform.position.x + (Input.GetAxis("Horizontal") * paddleSpeed);
		// Using clamp to make sure the new position
		// is bounded to the desired boundaries
		if (paddleUp) {  //if Paddle Power Up is active
			paddlePos = new Vector3 (Mathf.Clamp (xPos, -6, 8), paddlePos.y, paddlePos.z);
		}
		else {
			paddlePos = new Vector3 (Mathf.Clamp (xPos, -6.75f, 8.75f), paddlePos.y, paddlePos.z);
		}

		// TODO : Apply the new position to the paddle transform  
		// ---[Your code here]---
		if (!GameObject.Find("Ball").GetComponent<BallBehaviour> ().lose && !GameObject.Find("Ball").GetComponent<BallBehaviour> ().win) {
			transform.position = paddlePos;
		}
		if (Time.time > 1 + start && !win && !GameObject.Find("Ball").GetComponent<BallBehaviour> ().lose) {
			start = Time.time;
		}
		scoreText.text = "Score: " + score;
		if (GameObject.FindGameObjectsWithTag ("Brick").Length < 1 && !win && !GameObject.Find("Ball").GetComponent<BallBehaviour> ().lose) {
			wait = true;
		}
		if (GameObject.FindGameObjectsWithTag ("Brick").Length < 1 &&
			GameObject.FindGameObjectsWithTag ("Fireball").Length < 1 &&
			GameObject.FindGameObjectsWithTag ("Paddle Up").Length < 1 &&
			GameObject.FindGameObjectsWithTag ("Coin").Length < 1 &&
			GameObject.FindGameObjectsWithTag ("Rage").Length < 1 &&
			!win && !GameObject.Find("Ball").GetComponent<BallBehaviour> ().lose) {
			win = true;
			GameObject.Find ("Paddle").GetComponent<PaddleBehaviour> ().score += winScore;
			GameObject.Find ("Ball").GetComponent<BallBehaviour> ().win = true;
			GameObject.Find ("Ball").GetComponent<BallBehaviour> ().gameOverText.text = "YOU WIN";
			GameObject.Find ("Ball").GetComponent<BallBehaviour> ().gameOverText2.text = "Your score: " + GameObject.Find ("Paddle").GetComponent<PaddleBehaviour> ().score + "\nPress \"Space\" to Restart";
			GameObject.Find ("Info Text").SetActive (false);
			GameObject.Find ("Score Text").SetActive (false);
		}
		if (score < 0) {
			score = 0;
		}
		if (Input.GetKey("escape")) {
			Application.Quit();
		}
	}

	// A Score properties. Acts like a setter 
	// and getter for a variable 
	public float Score {
		get {
			return this.score;
		}
		set {
			score = value;
		}
	}
}