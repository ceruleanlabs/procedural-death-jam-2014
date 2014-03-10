using UnityEngine;
using System.Collections;

public class Gizmo : MonoBehaviour {

	public void OnDrawGizmos() {
		Gizmos.color = Color.green;
		Gizmos.DrawCube(transform.position, new Vector3(1, 1, 1));
	}
}
