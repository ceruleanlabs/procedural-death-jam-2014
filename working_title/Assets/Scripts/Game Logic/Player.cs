using UnityEngine;
using System.Collections;

public class Player : Controllable {
	public bool goalAcheived = false;
	public PlayerWeapon weapon;

	public void collectItem(Collectable collectedItem) {
		switch(collectedItem.itemId) {
			case Items.GoalItem:
				goalAcheived = true;
				break;
			case Items.HeartLarge:
				health += 1f;
				break;
			default:
				break;
		}
	}

	protected override void Update() {
		base.Update();
		if(Input.GetMouseButtonDown(0)) {
			weapon.animation.Play();
		}
	}
}
