using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour {
	public Transform enemyType;

	public void Spawn (int min, int max) {
		max = Mathf.Min(max, transform.childCount);
		List<Transform> samplePositions = new List<Transform>();
		List<Transform> pickedPositions = new List<Transform>();
		
		int count = 0;
		int target = (int)((Random.value * (max - min)) + min);
		
		foreach (Transform child in transform)
		{
			samplePositions.Add(child);
		}
		
		while(count < target) {
			count += 1;
			int i = (int)(Random.value * samplePositions.Count);
			pickedPositions.Add(samplePositions[i]);
			samplePositions.RemoveAt(i);
		}
		
		foreach (Transform pickedPosition in pickedPositions) {
			Transform newEnemy = (Transform) Instantiate(enemyType, pickedPosition.position, Quaternion.identity);
			newEnemy.parent = pickedPosition;
		}
	}
}
