// http://wiki.unity3d.com/index.php?title=RigidbodyFPSWalker

using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Rigidbody))]
[RequireComponent (typeof (CapsuleCollider))]

public class BasicEnemy : Controllable {
	
	public float minSpeed = 10.0f;
	public float maxSpeed = 10.0f;
	public float gravity = 10.0f;
	public float maxVelocityChange = 10.0f;
	public float range = 2.0f;
	public float attack = 1.0f;
	public float attackTimer = 3.0f;
	private float attackCountdown = 0.0f;
	private bool grounded = false;
	private float speed;
	public Transform target;
	public Transform model;
	
	void Start () {
		GameObject player = GameObject.FindGameObjectWithTag("Player");
		speed = Random.Range(minSpeed, maxSpeed);
		if(player != null) target = player.transform;
	}
	
	void Awake () {
		rigidbody.freezeRotation = true;
		rigidbody.useGravity = false;
	}

	void Update () {
		attackCountdown -= Time.deltaTime;
		if(target != null && Vector3.Distance(transform.position, target.position) <= range && CanAttack()) {
			Attack();
		}
	}
	
	void FixedUpdate () {
		if(target == null) return;

		if(model != null) model.LookAt(new Vector3(target.position.x, transform.position.y, target.position.z));

		if (grounded && Vector3.Distance(transform.position, target.position) > range) {
			// Calculate how fast we should be moving
			Vector3 targetVelocity = target.transform.position - this.transform.position;
			targetVelocity = targetVelocity.normalized;
			targetVelocity = transform.TransformDirection(targetVelocity);
			targetVelocity *= speed;
			
			// Apply a force that attempts to reach our target velocity
			Vector3 velocity = rigidbody.velocity;
			Vector3 velocityChange = (targetVelocity - velocity);
			velocityChange.x = Mathf.Clamp(velocityChange.x, -maxVelocityChange, maxVelocityChange);
			velocityChange.z = Mathf.Clamp(velocityChange.z, -maxVelocityChange, maxVelocityChange);
			velocityChange.y = 0;
			rigidbody.AddForce(velocityChange, ForceMode.VelocityChange);
		}
		
		// We apply gravity manually for more tuning control
		rigidbody.AddForce(new Vector3 (0, -gravity * rigidbody.mass, 0));
		
		grounded = false;
	}
	
	void OnCollisionStay () {
		grounded = true;    
	}

	private bool CanAttack() {
		if(attackCountdown < 0.0) return true;
		else return false;
	}

	private void Attack() {
		target.gameObject.SendMessage("TakeDamage",attack);
		attackCountdown = attackTimer;
	}
}