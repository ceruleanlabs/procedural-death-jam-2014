using UnityEngine;
using System.Collections;

public class Controllable : MonoBehaviour {
	public float health = 3f;

	public void TakeDamage(float amount) {
		health -= amount;
		if(health <= 0f) {
			Destroy(this.gameObject);
		}
	}
}
