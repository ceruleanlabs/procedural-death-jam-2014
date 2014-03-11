using UnityEngine;
using System.Collections;

public class Player : Controllable {
	public bool goalAcheived = false;

	public void collectItem(Collectable collectedItem) {
		if(collectedItem.itemId == Items.GoalItem) {
			goalAcheived = true;
		}
	}
}
