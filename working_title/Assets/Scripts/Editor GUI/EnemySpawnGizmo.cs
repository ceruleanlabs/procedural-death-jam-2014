using UnityEngine;
using System.Collections;

public class EnemySpawnGizmo : MonoBehaviour {
	
	public void OnDrawGizmos() {
		Gizmos.color = Color.red;
		Gizmos.DrawCube(transform.position, new Vector3(1, 1, 1));
	}
}
