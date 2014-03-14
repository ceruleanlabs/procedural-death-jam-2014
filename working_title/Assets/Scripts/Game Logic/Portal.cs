using UnityEngine;
using System.Collections;

public class Portal : MonoBehaviour {
	public Directions direction;

	// Bunch of timer shit because apparently setting a parent to inactive doesn't stop the trigger from firing
	public float activationTime = 2.0f;
	private float activationTimer;
	private bool portalActive = true;

	private LevelManager lm_reference;

	void Start () {
		activationTimer = activationTime;
		collider.isTrigger = true;
	}

	void Awake () {
		lm_reference = GameObject.Find("Logic Controller").GetComponent<LevelManager>();
	}
	
	void OnTriggerEnter(Collider other) {
		if(other.tag == "Player" && activationTimer <= 0 && portalActive) {
			portalActive = false;
			lm_reference.Move(direction);
		}
	}

	void Update() {
		if(activationTimer > 0) {
			activationTimer = activationTimer - Time.deltaTime;
		}
	}

	public void Deactivate() {
		activationTimer = activationTime;
		portalActive = false;
	}

	public void Activate() {
		activationTimer = activationTime;
		portalActive = true;
	}
}
