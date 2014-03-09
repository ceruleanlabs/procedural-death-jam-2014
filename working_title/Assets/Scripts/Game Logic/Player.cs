using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	public int health = 100;
	public bool goalAcheived = false;

	public void collectItem(Collectable collectedItem) {
		if(collectedItem.itemId == Items.GoalItem) {
			goalAcheived = true;
		}
	}
}
