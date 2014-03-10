using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ItemSpawner : MonoBehaviour {
	public Transform tree1;
	public int minTrees;
	public int maxTrees;
	
	// Use this for initialization
	void Start () {
		maxTrees = Mathf.Min(maxTrees + 1, transform.childCount);
		List<Transform> samplePositions = new List<Transform>();
		List<Transform> pickedPositions = new List<Transform>();

		int count = 0;
		int target = (int)((Random.value * (maxTrees - minTrees)) + minTrees);

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
			Transform newTree = (Transform) Instantiate(tree1, pickedPosition.position, Quaternion.identity);
			newTree.localEulerAngles = new Vector3(0, Random.Range(0, 360), 0);
			float newScale = (Random.value * 0.3f) + 0.7f;
			newTree.localScale = new Vector3(newScale, newScale, newScale);
			newTree.parent = pickedPosition;
		}
	}
}
