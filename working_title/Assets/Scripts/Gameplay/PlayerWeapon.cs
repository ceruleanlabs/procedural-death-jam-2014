using UnityEngine;
using System.Collections;

public class PlayerWeapon : MonoBehaviour {
	public float damage = 1.0f;

	void OnTriggerEnter(Collider other) {
		if(other.tag == "Enemy") {
			Controllable otherCharacter = other.GetComponent<Controllable>();
			otherCharacter.TakeDamage(damage);
		}
	}
}
