using UnityEngine;
using System.Collections;

public class GameSquare : MonoBehaviour {
	public int difficulty = 3;
	public Portal NorthDoor;
	public Portal SouthDoor;
	public Portal EastDoor;
	public Portal WestDoor;
	public Transform NorthSpawn;
	public Transform SouthSpawn;
	public Transform EastSpawn;
	public Transform WestSpawn;

	private LevelManager lm_reference;
	private Transform players_transform;

	// Use this for initialization
	void Awake () {
		lm_reference = GameObject.Find("Logic Controller").GetComponent<LevelManager>();
	}

	public Transform spawnForOppositeDirection(Directions direction) {
		if(direction == Directions.North)
			return SouthSpawn;
		else if(direction == Directions.East)
			return WestSpawn;
		else if(direction == Directions.West)
			return EastSpawn;
		else if(direction == Directions.South)
			return NorthSpawn;
		else
			return NorthSpawn;
	}

	public void setPlayerTransform(Transform p) {
		players_transform = p;
	}

	public void Deactivate() {
		gameObject.SetActive(false);
	}

	public void Activate() {
		gameObject.SetActive(true);
		if(NorthDoor != null) NorthDoor.Activate();
		if(SouthDoor != null) SouthDoor.Activate();
		if(WestDoor != null) WestDoor.Activate();
		if(EastDoor != null) EastDoor.Activate();
	}
}
