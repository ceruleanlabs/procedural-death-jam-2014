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
		createMazeRoute();
	}

	public MazeCreator(int size, int defaultVal, int[] start) {
		maze = new int[size, size];
		this.start = start;
		this.defaultVal = defaultVal;

		Random random = new Random();
		end = new int[2] { (int)(Random.value * size), (int)(Random.value * (size / 2)) };
		createMazeRoute();
	}

	public MazeCreator(int size, int defaultVal, int[] start, int[] end) {
		maze = new int[size, size];

		this.defaultVal = defaultVal;
		this.start = start;
		this.end = end;
		createMazeRoute();
	}

	private void createMazeRoute() {
		int[] midpoint = pickEndPoint(end[0], end[1]);
		Debug.Log("Midpoint: " + midpoint[0].ToString() + " " + midpoint[1].ToString());

		for ( int i = 0; i < maze.GetLength(0); i++ ) {
			for ( int j = 0; j < maze.GetLength(1); j++) {
				maze[i,j] = defaultVal;
			}
		}

		maze[start[0], start[1]] = 0;

		int curX = start[0];
		int curY = start[1];

		while(curX != midpoint[0] || curY != midpoint[1]) {
			int[] next = nextStep(curX, curY, midpoint[0], midpoint[1]);

			curX += next[0];
			curY += next[1];
			
			maze[curX,curY] = 1;
		}

		while(curX != end[0] || curY != end[1]) {
			int[] next = nextStep(curX, curY, end[0], end[1]);
			
			curX += next[0];
			curY += next[1];

			maze[curX,curY] = 1;
		}
		fillInTwos();
	}

	private int[] nextStep(int startX, int startY, int endX, int endY) {
		int[] result = new int[2] {0, 0};
		if(startX < endX) result[0] = 1;
		if(startX > endX) result[0] = -1;
		if(startY < endY) result[1] = 1;
		if(startY > endY) result[1] = -1;

		if(result[0] != 0 && result[1] != 0) {
			if(Random.value < 0.5) {
				result[0] = 0;
			} else result[1] = 0;
		}
		return result;
	}

	private int[] pickEndPoint(int endX, int endY) {
		int xQuadrant = 0;
		int yQuadrant = 0;

		if(endX >= maze.GetLength(0) / 2) xQuadrant = 1;
		if(endY >= maze.GetLength(1) / 2) yQuadrant = 1;

		if(xQuadrant == 1) xQuadrant = 0;
		else xQuadrant = 1;

		if(Random.value < 0.5) {
			if(yQuadrant == 1) yQuadrant = 0;
			else yQuadrant = 1;
		}

		int midX = Random.Range(0, maze.GetLength(0) / 2) + (xQuadrant * (maze.GetLength(0) / 2));
		int midY = Random.Range(0, maze.GetLength(0) / 2) + (yQuadrant * (maze.GetLength(0) / 2));

		return new int[2] {midX, midY};
	}

	private void fillInTwos() {
		for ( int i = 0; i < maze.GetLength(0); i++ ) {
			for ( int j = 0; j < maze.GetLength(1); j++) {
				if(maze[i,j] <= 2) continue;
				if(i > 0 && maze[i - 1, j] <= 1) maze[i,j] = 2;
				if(j > 0 && maze[i, j - 1] <= 1) maze[i,j] = 2;
				if(i < maze.GetLength(0) - 1 && maze[i + 1, j] <= 1) maze[i,j] = 2;
				if(j < maze.GetLength(1) - 1 && maze[i, j + 1] <= 1) maze[i,j] = 2;
			}
		}
	}
}
