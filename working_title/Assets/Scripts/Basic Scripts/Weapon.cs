using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour {
	public float damage;

	void OnTriggerEnter(Collider other) {
		if(other.tag == "Player") {
			Player player = other.GetComponent<Player>();
			player.TakeDamage(damage);
		}
	}
}
