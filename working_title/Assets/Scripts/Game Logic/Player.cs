using UnityEngine;
using System.Collections;

public class Player : Controllable {
	public bool goalAcheived = false;
	public PlayerWeapon weapon;

	public void collectItem(Collectable collectedItem) {
		if(collectedItem.itemId == Items.GoalItem) {
			goalAcheived = true;
		}
	}

	protected override void Update() {
		base.Update();
		if(Input.GetMouseButtonDown(0)) {
			weapon.animation.Play();
		}
	}
}
