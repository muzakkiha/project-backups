using UnityEngine;
using System.Collections;

public class ParticleBehaviour : MonoBehaviour {

	// Destroy particle after 0.6 s
	void Start () {
		Destroy (gameObject, 0.6f);
	}
}