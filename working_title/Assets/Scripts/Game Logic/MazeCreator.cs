using UnityEngine;
using System.Collections;

public class MazeCreator {
	public int[,] maze;
	public int[] start;
	public int[] end;
	private int defaultVal;

	private bool[,] alreadyVisted;
	private bool[,] solutionPath; // The solution to the maze

	public MazeCreator(int size, int defaultVal) {
		maze = new int[size, size];
		this.defaultVal = defaultVal;

		alreadyVisted = new bool[size, size];
		solutionPath = new bool[size, size];
		
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

			Debug.Log("X"+curX+"Y"+curY);
			
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

	private bool validNext(int nextX, int nextY) {

		bool valid = true;

		int maxX = maze.GetLength(0);
		int maxY = maze.GetLength(1);

		if (nextX > maxX || nextY > maxY || have_visited(nextX, nextY)) valid = false;

		return valid;

		}
	//Checks if maze is still solvable if we go a direction
	private bool isSolvable(int x, int y) {

		int endX = end[0];
		int endY = end[1];
		int width = maze.GetLength(0);
		int height = maze.GetLength(1);

		if (x == endX && y == endY) return true; // If you reached the end
		if (!validNext(x, y)) return false;  
		// If you are on a wall or already were here
		alreadyVisted[x,y] = true;
		if (x != 0) // Checks if not on left edge
		if (isSolvable(x-1, y)) { // Recalls method one to the left
			solutionPath[x,y] = true; // Sets that path value to true;
			return true;
		}
		if (x != width - 1) // Checks if not on right edge
		if (isSolvable(x+1, y)) { // Recalls method one to the right
			solutionPath[x,y] = true;
			return true;
		}
		if (y != 0)  // Checks if not on top edge
		if (isSolvable(x, y-1)) { // Recalls method one up
			solutionPath[x,y] = true;
			return true;
		}
		if (y != height- 1) // Checks if not on bottom edge
		if (isSolvable(x, y+1)) { // Recalls method one down
			solutionPath[x,y] = true;
			return true;
		}
		return false;


		}

	// Generate 2 fake mazes
	private void generateFakes(bool[,] fake1, bool[,] fake2, int size){

			for (int i=0; i<size; i++) {
				for (int j=0; j<size; j++) {
					fake1[i,j] = false;
					fake2[i,j] = false;
					}
				}

		}

}
