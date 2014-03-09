using UnityEngine;
using System.Collections;

public class Rotates : MonoBehaviour {
	public float RotateSpeed = 1.0f;

	// Use this for initialization
	void Start () {
		
	}

	void Update () {
		transform.RotateAround(collider.bounds.center, Vector3.up, RotateSpeed * Time.deltaTime);
	}
}
