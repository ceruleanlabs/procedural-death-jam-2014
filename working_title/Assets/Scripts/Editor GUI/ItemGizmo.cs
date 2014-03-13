using UnityEngine;
using System.Collections;

public class ItemGizmo : MonoBehaviour {
	
	public void OnDrawGizmos() {
		Gizmos.color = Color.white;
		Gizmos.DrawCube(transform.position, new Vector3(1, 1, 1));
	}
}