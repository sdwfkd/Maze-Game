using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeGenerator : MonoBehaviour {
	public int width, height;
	public static bool[,] maze;
	public GameObject wall;
	public static bool started = false;

	// Use this for initialization
	void Start () {
		// Because started is static we can change it and it will always be applied
		if (started == false){
			//wall = GameObject.FindGameObjectWithTag("Wall");

			maze = new bool[width, height];

			recursiveDivision(width, height, 0, 0);

			var instance = wall;

			// Loop through and spawn each wall
			for (int i = 0; i < width; i++){
				for (int j = 0; j < height; j++){
					if (maze[i,j]) {
						//Debug.Log("Maze [" + i + ", " + j + "] = " + maze[i, j]);
						instance = Instantiate(wall);
						instance.transform.position += new Vector3(-i, 0, -j);
					}
				}
			}

			for(int i = -2; i < width + 2; i++){
				for(int j = -2; j < height + 2; j++){
					if(i == -2 || j == -2 || i == width + 1 || j == height + 1){
						instance = Instantiate(wall);
						instance.transform.position += new Vector3(-i, 0, -j);
					}
				}
			}

			// This code can be used to spawn a cube, and transform the copy on the x,z plane for the maze
			//var instance = Instantiate(wall);
			//instance.transform.position += new Vector3(1,0,0);
		
			//Debug.Log("Started: " + started);
			started = true;
		}
	}

	void recursiveDivision(int width, int height, int offsetX, int offsetY){
		//Debug.Log("Recursive Start");
		// Check for end conditions
		if (width < 2 || height < 2){
			return;
		}

		// Choose a random split either horizontal of vertical
		int horizontal = Random.Range(1,100) % 2;

		// Choose where to make the wall, and where the open path will be
		int wall;
		int path;

		// Horizontal Wall
		if(horizontal == 1){
			// Choose where to make the wall, and where the open path will be
			wall = Random.Range(offsetY, height);
			path = Random.Range(offsetX, width);
			//Debug.Log("Wall, Path: " + wall + ", " + path);

			// Fill them in our maze
			for (int i = offsetX; i < width; i++){
				//Debug.Log("i, offsetX, width, wall" + i + ", " + offsetX + ", " + width + ", " + wall);
				if (i != path){
					maze[wall, path] = true;
				}else{
					maze[wall, path] = false;
				}
			}

			// call recursiveDivision for the top and bottom half
			//Debug.Log("Division top");
			recursiveDivision(width, wall - offsetY, offsetX, offsetY, path, true);
			//Debug.Log("Division bottom");
			recursiveDivision(width, height - wall, offsetX, wall, path, true);
		}
		// Vertical Wall
		else{
			// Choose where to make the wall, and where the open path will be
			wall = Random.Range(offsetY, height);
			path = Random.Range(offsetX, width);
			//Debug.Log("Wall, Path: " + wall + ", " + path);

			// Fill them in on our maze
			for (int i = offsetY; i < height; i++){
				//Debug.Log("i, offsetY, height, wall" + i + ", " + offsetY + ", " + height+ ", " + wall);
				if (i != path){
					maze[path, wall] = true;
				}
				else{
					maze[path, wall] = false;
				}
			}

			// call recursiveDivision for the left and right half
			//Debug.Log("Division left");
			recursiveDivision(wall - offsetX, height, offsetX, offsetY, path, false);
			//Debug.Log("Division right");
			recursiveDivision(offsetX + wall, height, wall, offsetY, path, false);
		}

		//Debug.Log("Recursive Div End");
	}

	// true for orientation is a horizontal division, false a vertical division
	void recursiveDivision(int width, int height, int offsetX, int offsetY, int noWall, bool orientation){
		//Debug.Log("Recursive Start");
		// Check for end conditions
		if (width < 2 || height < 2){
			return;
		}

		// Choose a random split either horizontal of vertical
		int horizontal = Random.Range(1, 100) % 2;

		// Choose where to make the wall, and where the open path will be
		int wall;
		int path;

		// Horizontal Wall
		if (horizontal == 1){
			// Choose where to make the wall, and where the open path will be
			// If the new wall will block the previous path, then piack a new one
			if (orientation == false){
				wall = Random.Range(offsetY, height);
				if (wall == noWall){
					wall = Random.Range(offsetY, height);
				}
			}
			else{
				wall = Random.Range(offsetY, height);
			}
			path = Random.Range(offsetX, width);
			//Debug.Log("Wall, Path: " + wall + ", " + path);

			// Fill them in our maze
			for (int i = offsetX; i < width; i++){
				//Debug.Log("i, offsetX, width, wall" + i + ", " + offsetX + ", " + width + ", " + wall);
				if (i != path){
					maze[wall, path] = true;
				}
				else{
					maze[wall, path] = false;
				}
			}

			// call recursiveDivision for the top and bottom half
			//Debug.Log("Division top");
			recursiveDivision(width, wall - offsetY, offsetX, offsetY, path, true);
			//Debug.Log("Division bottom");
			recursiveDivision(width, height - wall, offsetX, wall, path, true);
		}
		// Vertical Wall
		else{
			// Choose where to make the wall, and where the open path will be
			// If the new wall will block the previous path, then piack a new one
			if (orientation == true){
				wall = Random.Range(offsetY, height);
				if(wall == noWall){
					wall = Random.Range(offsetY, height);
				}
			}
			else{
				wall = Random.Range(offsetY, height);
			}
			
			path = Random.Range(offsetX, width);
			//Debug.Log("Wall, Path: " + wall + ", " + path);

			// Fill them in on our maze
			for (int i = offsetY; i < height; i++){
				//Debug.Log("i, offsetY, height, wall" + i + ", " + offsetY + ", " + height + ", " + wall);
				if (i != path){
					maze[path, wall] = true;
				}
				else{
					maze[path, wall] = false;
				}
			}

			// call recursiveDivision for the left and right half
			//Debug.Log("Division left");
			recursiveDivision(wall - offsetX, height, offsetX, offsetY, path, false);
			//Debug.Log("Division right");
			recursiveDivision(offsetX + wall, height, wall, offsetY, path, false);
		}

		//Debug.Log("Recursive Div End");
	}
}
