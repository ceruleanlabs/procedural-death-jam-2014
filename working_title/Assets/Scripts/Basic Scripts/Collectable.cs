using UnityEngine;
using System.Collections;

public class Collectable : MonoBehaviour {
	void OnTriggerEnter(Collider other) {
		Destroy (this.gameObject);
	}
}
