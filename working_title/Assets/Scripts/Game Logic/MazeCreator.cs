using UnityEngine;
using System.Collections;

public class MazeCreator {
	public int[,] maze;
	public int[] start;
	public int[] end;
	private int defaultVal;

	public MazeCreator(int size, int defaultVal) {
		maze = new int[size, size];
		this.defaultVal = defaultVal;

		start = new int[2] { (int)(Random.value * size), (int)(Random.value * size) };
		end = new int[2] { (int)(Random.value * size), (int)(Random.value * size) };
		Debug.Log (start);
			Debug.Log(end);
		createMazeRoute();
	}

	public MazeCreator(int size, int defaultVal, int[] start) {
		maze = new int[size, size];
		this.start = start;
		this.defaultVal = defaultVal;

		Random random = new Random();
		end = new int[2] { (int)(Random.value * size), (int)(Random.value * size) };
		createMazeRoute();
	}

	public MazeCreator(int size, int defaultVal, int[] start, int[] end) {
		maze = new int[size, size];
		this.defaultVal = defaultVal;
		this.start = start;
		this.end = end;
		createMazeRoute();
	}

	// Quick way to make a beeline to the exit
	private void createMazeRoute() {
		for ( int i = 0; i < maze.GetLength(0); i++ ) {
			for ( int j = 0; j < maze.GetLength(1); j++) {
				maze[i,j] = defaultVal;
			}
		}

		maze[start[0], start[1]] = 0;

		int curX = start[0];
		int curY = start[1];

		while(curX != end[0] || curY != end[1]) {
			int moveX = 0;
			int moveY = 0;

			if(Random.value < 0.5) {
				if(curX < end[0]) moveX = 1;
				else if(curX > end[0]) moveX = -1;

				if(moveX == 0) {
					if(curY < end[1]) moveY = 1;
					else if(curY > end[1]) moveY = -1;
				}
			} else if(moveX == 0) {
				if(curY < end[1]) moveY = 1;
				else if(curY > end[1]) moveY = -1;
				
				if(moveY == 0) {
					if(curX < end[0]) moveX = 1;
					else if(curX > end[0]) moveX = -1;
				}
			}

			curX += moveX;
			curY += moveY;
			
			maze[curX,curY] = 1;
		}
	}

	private bool have_visited(int nextX, int nextY) {

		bool rval = false;
		if (maze[nextX, nextX] == 1) {
			rval = true;
		}

		return rval;

	}

	private bool valid_next(int nextX, int nextY) {

		bool valid = true;

		int maxX = maze.GetLength(0);
		int maxY = maze.GetLength(1);

		if (nextX > maxX || nextY > maxY || maze [nextX, nextX] == 1) valid = false;

		return valid;

		}

}
