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

	public void StartSpawners() {
		foreach (Transform child in transform)
		{
			ItemSpawner itemSpawner = child.GetComponent<ItemSpawner>();
			if(itemSpawner != null && (difficulty == 1 || difficulty == 2)) itemSpawner.Spawn(0, 1);
			EnemySpawner spawner = child.GetComponent<EnemySpawner>();
			if(spawner != null) {
				switch(difficulty) {
					case 1:
						spawner.Spawn(0, 1);
						break;
					case 2:
						spawner.Spawn(1, 2);
						break;
					case 3:
						spawner.Spawn(2, 3);
						break;
					default:
						break;
				}
			}
		}
	}
}
