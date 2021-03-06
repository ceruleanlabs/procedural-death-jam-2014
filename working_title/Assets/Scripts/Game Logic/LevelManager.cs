﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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
	public int startY = 10;
	public Player playerCharacter;
	public Transform goalObject;
	public GameSquare genericLevel;
	public GameSquare startLevelPrefab;
	public List<Transform> runes;

	public Transform mainRune;
	private GameSquare startLevel;
	private MazeCreator maze;
	private GUIController gui;
	private int curX;
	private int curY;
	private Player player;

	private GameSquare[,] gameBoard;

	// Use this for initialization
	void Start () {
		gui = GameObject.Find("Logic Controller").GetComponent<GUIController>();
		InitGame();
	}

	public void InitGame () {
		// First reset anything that already exists
		if(player) player.goalAcheived = false;
		if(startLevel != null) {
			Destroy(startLevel.gameObject);
			startLevel = null;
		}
		if(mainRune != null) runes.Add(mainRune);
		if(gameBoard != null) {
			for(int i = 0; i < gameBoard.GetLength(0); i++) {
				for(int j = 0; j < gameBoard.GetLength(1); j++){
					if(gameBoard[i, j] != null) {
						Destroy(gameBoard[i, j].gameObject);
						gameBoard[i, j] = null;
					}
				}
			}
		}

		if(player == null) player = (Player) Instantiate(playerCharacter, Vector3.zero, Quaternion.Euler(0, -90, 0));
		gameBoard = new GameSquare[size,size];
		maze = new MazeCreator(size, 3, new int[]{startX, startY - 1});
		Debug.Log("Start at " + maze.start[0].ToString() + " " + maze.start[1].ToString());
		Debug.Log("End at " + maze.end[0].ToString() + " " + maze.end[1].ToString());
		for(int i = 0; i < 10; i++) {
			string test = "";
			for(int j = 0; j < 10; j++) {
				test += maze.maze[j,i].ToString();
			}
			Debug.Log(test);
		}
		curX = startX;
		curY = startY;
		int runeIndex = Random.Range(0, runes.Count - 1);
		mainRune = runes[runeIndex];
		runes.RemoveAt(runeIndex);
		ActivateCurrentSquare();
		MovePlayerToSpawn(Directions.North);
	}

	// Moves the player to a new square
	public void Move(Directions direction) {
		DeactivateCurrentSquare();

		int[] dir = DirToCoord(direction);
		curX = curX + dir[0];
		curY = curY + dir[1];

		if(!(curX == startX && curY == startY)) {
			if(curX > size - 1)
				curX = 0;
			if(curX < 0)
				curX = size - 1;
			if(curY > size - 1)
				curY = 0;
			if(curY < 0)
				curY = size - 1;
		} else {
			if(gui != null && player.goalAcheived == true) gui.LevelDone();
		}

		ActivateCurrentSquare();
		MovePlayerToSpawn(direction);
		Debug.Log("Player At " + curX.ToString() + " " + curY.ToString());
	}

	public bool playerAlive(){
		return player != null && player.health > 0;
	}

	public Player getPlayer() {
		return player;
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
		currentSquare().Deactivate();
	}

	private void ActivateCurrentSquare() {
		if(curX == startX && curY == startY) {
			if(startLevel == null) {
				startLevel = (GameSquare) Instantiate(startLevelPrefab, Vector3.zero, Quaternion.identity);
				startLevel.SetRunes(mainRune, runes[0], runes[0], runes[0]);
				startLevel.Activate();
			} else {
				startLevel.Activate();
			}
		} else {
			if(currentSquare() == null) {
				gameBoard[curX, curY] = (GameSquare) Instantiate(genericLevel, Vector3.zero, Quaternion.identity);
				if(maze.end[0] == curX && maze.end[1] == curY) InstantiateGoalObject(currentSquare());
				gameBoard[curX, curY].difficulty = maze.maze[curX, curY];
				gameBoard[curX, curY].StartSpawners();
				gameBoard[curX, curY].SetRunes(RuneForDirection(Directions.North), RuneForDirection(Directions.South), RuneForDirection(Directions.East), RuneForDirection(Directions.West));
			}
			currentSquare().Activate();
		}
	}

	private void MovePlayerToSpawn(Directions dir) {
		player.transform.position = currentSquare().spawnForOppositeDirection(dir).position;
		player.transform.rotation = currentSquare().spawnForOppositeDirection(dir).rotation;
	}

	private void InstantiateGoalObject (GameSquare endSqaure) {
		Transform clonedObject = (Transform) Instantiate(goalObject);
		goalObject.transform.position = new Vector3(0, 1, 0);
		clonedObject.transform.parent = endSqaure.transform;
	}

	private GameSquare currentSquare() {
		if(curX == startX && curY == startY) {
			return startLevel;
		} else {
			return gameBoard[curX, curY];
		}
	}

	private Transform RuneForDirection(Directions dir) {
		switch(dir) {
		case Directions.North:
			if(curY - 1 >= 0 && maze.maze[curX, curY - 1] == 1) return mainRune;
			break;
		case Directions.South:
			if((curY + 1 < size && maze.maze[curX, curY + 1] == 1) || (curY + 1 == startY && curX == startX)) return mainRune;
			break;
		case Directions.East:
			if(curX + 1 < size && maze.maze[curX + 1, curY] == 1) return mainRune;
			break;
		case Directions.West:
			if(curX - 1 >= 0 && maze.maze[curX - 1, curY] == 1) return mainRune;
			break;
		}
		return runes[Random.Range(0, runes.Count)];
	}
}
