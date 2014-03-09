using UnityEngine;
using System.Collections;

public enum Directions
{
	North = 1,
	East = 2,
	South = 3,
	West = 4
};

public class LevelManager : MonoBehaviour {
	public int size = 10;
	public int startX = 4;
	public int startY = 9;
	public Transform player;
	public GameSquare genericLevel;

	private MazeCreator maze;
	private int curX;
	private int curY;

	private GameSquare[,] gameBoard;

	// Use this for initialization
	void Start () {
		gameBoard = new GameSquare[size,size];
		maze = new MazeCreator(size, 3, new int[]{startX, startY});
		curX = startX;
		curY = startY;
		ActivateCurrentSquare();
		MovePlayerToSpawn(Directions.South);
	}

	// Moves the player to a new square
	public void Move(Directions direction) {
		DeactivateCurrentSquare();

		int[] dir = DirToCoord(direction);
		curX = curX + dir[0];
		curY = curY + dir[1];
		if(curX > size - 1)
			curX = 0;
		if(curX < 0)
			curX = size - 1;
		if(curY > size - 1)
			curY = 0;
		if(curY < 0)
			curY = size - 1;

		ActivateCurrentSquare();
		MovePlayerToSpawn(direction);
		Debug.Log("Player At " + curX.ToString() + " " + curY.ToString());
	}
	
	private int[] DirToCoord(Directions direction) {
		if(direction == Directions.North)
			return new int[2] {0, -1};
		else if(direction == Directions.East)
			return new int[2] {1, 0};
		else if(direction == Directions.West)
			return new int[2] {-1, 0};
		else if(direction == Directions.South)
			return new int[2] {0, 1};
		else
			return new int[2] {0, 0};
	}

	private void DeactivateCurrentSquare() {
		gameBoard[curX, curY].Deactivate();
	}

	private void ActivateCurrentSquare() {
		if(gameBoard[curX, curY] == null) {
			gameBoard[curX, curY] = (GameSquare) Instantiate(genericLevel, Vector3.zero, Quaternion.identity);
			Debug.Log("Activating Square " + curX.ToString() + " " + curY.ToString());
		}
		gameBoard[curX, curY].Activate();
	}

	private void MovePlayerToSpawn(Directions dir) {
		player.transform.position = gameBoard[curX, curY].spawnForOppositeDirection(dir).position;
		player.transform.rotation = gameBoard[curX, curY].spawnForOppositeDirection(dir).rotation;
	}
}
