using UnityEngine;
using System.Collections;

public class Portal : MonoBehaviour {
	public Directions direction;

	// Bunch of timer shit because apparently setting a parent to inactive doesn't stop the trigger from firing
	public float activationTime = 2.0f;
	private float activationTimer;

	private LevelManager lm_reference;

	void Start () {
		collider.isTrigger = false;
		activationTimer = activationTime;
	}

	void Awake () {
		collider.isTrigger = false;
		lm_reference = GameObject.Find("Logic Controller").GetComponent<LevelManager>();
	}
	
	void OnTriggerEnter(Collider other) {
		if(other.tag == "Player") {
			lm_reference.Move(direction);
			collider.isTrigger = false;
		}
	}

	void Update() {
		if(activationTimer > 0) {
			activationTimer = activationTimer - Time.deltaTime;
			if(activationTimer <= 0) {
				collider.isTrigger = true;
			}
		}
	}

	public void Deactivate() {
		collider.isTrigger = false;
		activationTimer = activationTime;
	}

	public void Activate() {
		collider.isTrigger = false;
		activationTimer = activationTime;
	}
}
