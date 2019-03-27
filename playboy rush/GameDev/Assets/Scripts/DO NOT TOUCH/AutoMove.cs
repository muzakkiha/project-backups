using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoMove : MonoBehaviour {

    public float ERROR_TRESHOLD = 0.1f;

    public float maxDelayBetweenMove = 20f;
    public float minDelayBetweenMove = 10f;
    public float currentDelay;

    public float maxX = 7f;
    public float maxY = 3f;

    public float speed = 5f;

    public int idleState = 0;
    public int walkRightState = 1;
    public int walkLeftState = 2;

    private Vector3 target;

	// Use this for initialization
	void Start () {
        currentDelay = Random.Range(minDelayBetweenMove, maxDelayBetweenMove);
        target = new Vector3(Random.Range(-maxX, maxX), Random.Range(-maxY, maxY), 0f);
    }

    // Update is called once per frame
    void Update () {
        currentDelay -= Time.deltaTime;

        if (currentDelay < 0f) {
            target = new Vector3(Random.Range(-maxX, maxX), Random.Range(-maxY, maxY), 0f);
            currentDelay = Random.Range(minDelayBetweenMove, maxDelayBetweenMove);
        }

        gameObject.transform.position =  Vector3.MoveTowards(gameObject.transform.position, target, speed * Time.deltaTime);

        float direction = target.x - gameObject.transform.position.x;
        // Debug.Log(direction);
        if (direction > ERROR_TRESHOLD) {
            gameObject.GetComponent<Animator>().SetInteger("state", walkRightState);
        } else if (direction < -ERROR_TRESHOLD) {
            gameObject.GetComponent<Animator>().SetInteger("state", walkLeftState);
        } else {
            gameObject.GetComponent<Animator>().SetInteger("state", idleState);
        }
	}
}
