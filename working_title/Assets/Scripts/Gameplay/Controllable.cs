using UnityEngine;
using System.Collections;

public class Controllable : MonoBehaviour {
	public float health = 3f;
	public float invulnerabilityTime = 1f;
	private float invulnCountDown = 0;

	void Update () {
		invulnCountDown -= Time.deltaTime;
	}

	public void TakeDamage(float amount) {
		if(invulnCountDown <= 0) {
			health -= amount;
			invulnCountDown = invulnerabilityTime;
		}

		if(health <= 0f) {
			Destroy(this.gameObject);
		}
	}
}
