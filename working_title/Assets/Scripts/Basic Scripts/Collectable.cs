using UnityEngine;
using System.Collections;

public enum Items
{
	GoalItem = 1,
	HeartLarge = 2
};

public class Collectable : MonoBehaviour {
	public Items itemId;

	void OnTriggerEnter(Collider other) {
		Player p = other.GetComponent<Player>();
		if(p != null) {
			p.collectItem(this);
		}
		Destroy (this.gameObject);
	}
}
