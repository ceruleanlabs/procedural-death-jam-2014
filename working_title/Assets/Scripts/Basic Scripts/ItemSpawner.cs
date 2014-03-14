using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ItemSpawner : MonoBehaviour {
	public Transform itemType;
	
	public void Spawn (int min, int max) {
		max = Mathf.Min(max, transform.childCount);
		List<Transform> samplePositions = new List<Transform>();
		List<Transform> pickedPositions = new List<Transform>();
		
		int count = 0;
		int target = (int)(Random.value * (max + 1 - min) + min);
		Debug.Log("ITEM: " + min.ToString() + " " + max.ToString() + " " + target.ToString());
		
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
			Transform item = (Transform) Instantiate(itemType, pickedPosition.position, Quaternion.identity);
			item.parent = pickedPosition;
		}
	}
}