using UnityEngine;
using System.Collections;

public class MazeCreator {
	public int[,] maze;
	public int[] start;
	public int[] end;
	private int defaultVal;

	private bool[,] wasHere;
	private bool[,] correctPath; // The solution to the maze

	public MazeCreator(int size, int defaultVal) {
		maze = new int[size, size];
		wasHere = new bool[size, size];
		correctPath = new bool[size, size];
		this.defaultVal = defaultVal;

		wasHere = new bool[size, size];
		correctPath = new bool[size, size];
		
		start = new int[2] { (int)(Random.value * size), (int)(Random.value * size) };
		end = new int[2] { (int)(Random.value * size), (int)(Random.value * size) };
		Debug.Log (start);
			Debug.Log(end);
		createRandomMazeRoute();
	}

	public MazeCreator(int size, int defaultVal, int[] start) {
		maze = new int[size, size];
		wasHere = new bool[size, size];
		correctPath = new bool[size, size];

		this.start = start;
		this.defaultVal = defaultVal;

		Random random = new Random();
		end = new int[2] { (int)(Random.value * size), (int)(Random.value * size) };
		createRandomMazeRoute();
	}

	public MazeCreator(int size, int defaultVal, int[] start, int[] end) {
		maze = new int[size, size];
		wasHere = new bool[size, size];
		correctPath = new bool[size, size];

		this.defaultVal = defaultVal;
		this.start = start;
		this.end = end;
		createRandomMazeRoute();
	}

	//Generates a random solvable maze route
	private void createRandomMazeRoute(){

		for ( int i = 0; i < maze.GetLength(0); i++ ) {
			for ( int j = 0; j < maze.GetLength(1); j++) {
				maze[i,j] = defaultVal;
			}
		}
		maze[start[0], start[1]] = 0;

		int curX = start[0];
		int curY = start[1];

		while (curX != end[0] && curY != end[1]) {

			int moveX = 0;
			int moveY = 0;

			bool tried_left = false;
			bool tried_right = false;
			bool tried_up = false;
			bool tried_down = false;
			Debug.Log("beforeloop");

			//Shouldn't ever happen, but sanity
			while(tried_all(tried_up, tried_down, tried_left, tried_right) == false){
				Debug.Log("inloop");
				int pm_one = plus_or_minus_one();

				float rand = Random.value;
				if (rand < 0.5){
					moveX = pm_one;
					if (pm_one > 0){
						tried_right = true;
					}else{
						tried_left = true;
						}
				}else{
					moveY = pm_one;
					if (pm_one > 0){
						tried_up = true;
					}else{
						tried_down = true;
					}
				}

				int potentialNextX = curX+moveX;
				int potentialNextY = curY+moveY;
				if (validNext(potentialNextX, potentialNextY)){
					int[,] newMaze = (int[,]) maze.Clone();
					newMaze[curX+moveX, curY+moveY] = 1;
					if (solveMaze(newMaze, curX+moveX, curY+moveY) != false){
						curX += moveX;
						curY += moveY;
						maze[curX,curY] = 1;
						break;
					}
				}

			}

			Debug.Log("FAILED X"+curX+"Y"+curY);


		}

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
		if (maze[nextX, nextX] == 1 || maze[nextX, nextY] == 0) {
			rval = true;
		}

		return rval;

	}

	private bool validNext(int nextX, int nextY) {

		bool valid = true;

		int maxX = maze.GetLength(0)-1;
		int maxY = maze.GetLength(1)-1;

		bool out_of_range = (nextX > maxX || nextY > maxY || nextX < 0 || nextY < 0);

		if (out_of_range) valid = false;

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
		wasHere[x,y] = true;
		if (x != 0) // Checks if not on left edge
		if (isSolvable(x-1, y)) { // Recalls method one to the left
			correctPath[x,y] = true; // Sets that path value to true;
			return true;
		}
		if (x != width - 1) // Checks if not on right edge
		if (isSolvable(x+1, y)) { // Recalls method one to the right
			correctPath[x,y] = true;
			return true;
		}
		if (y != 0)  // Checks if not on top edge
		if (isSolvable(x, y-1)) { // Recalls method one up
			correctPath[x,y] = true;
			return true;
		}
		if (y != height- 1) // Checks if not on bottom edge
		if (isSolvable(x, y+1)) { // Recalls method one down
			correctPath[x,y] = true;
			return true;
		}
		return false;


		}


	public bool solveMaze(int[,] maze, int startX, int startY) {

		for (int row = 0; row < maze.GetLength(0); row++)  
			// Sets bool Arrays to default values
		for (int col = 0; col < maze.GetLength(1); col++){
			wasHere[row,col] = false;
			correctPath[row,col] = false;
		}
		bool b = recursiveSolve(startX, startY);
		// Will leave you with a bool array (correctPath) 
		// with the path indicated by true values.
		// If b is false, there is no solution to the maze
		return b;
	}
	public bool recursiveSolve(int x, int y) {

		int endX = end[0];
		int endY = end[1];

		int width = maze.GetLength(0);
		int height = maze.GetLength(1);

		if (x == endX && y == endY) return true; // If you reached the end
		if (maze[x,y] == 1 || maze[x,y] == 0 || wasHere[x,y]) return false;  
		// If you are on a wall or already were here
		wasHere[x,y] = true;
		if (x != 0) // Checks if not on left edge
		if (recursiveSolve(x-1, y)) { // Recalls method one to the left
			correctPath[x,y] = true; // Sets that path value to true;
			return true;
		}
		if (x != width - 1) // Checks if not on right edge
		if (recursiveSolve(x+1, y)) { // Recalls method one to the right
			correctPath[x,y] = true;
			return true;
		}
		if (y != 0)  // Checks if not on top edge
		if (recursiveSolve(x, y-1)) { // Recalls method one up
			correctPath[x,y] = true;
			return true;
		}
		if (y != height- 1) // Checks if not on bottom edge
		if (recursiveSolve(x, y+1)) { // Recalls method one down
			correctPath[x,y] = true;
			return true;
		}
		return false;
	}

	//Generate + or - 1 for movement
	private int plus_or_minus_one() {
		int rval = 0;
		float random = Random.value;
		if (random < 0.5) {
			rval = -1;
		} else {
			rval = 1;
		}
		return rval;
	}

	// Checks to see if we've tried to move in all 4 directions
	private bool tried_all(bool up, bool down, bool left, bool right){
		return (up && down && left && right);
	}

}
