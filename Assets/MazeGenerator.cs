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

			var instance = null;

			// Loop through and spawn each wall
			for (int i = 0; i < width; i++){
				for (int j = 0; j < height; j++){
					//Debug.Log("Maze [" + i + ", "+ j +"] = " + maze[i,j]);
					if (maze[i,j]) {
						instance = Instantiate(wall);
						instance.transform.position += new Vector3(i, 0, j);
					}
				}
			}

			// This code can be used to spawn a cube, and transform the copy on the x,z plane for the maze
			//var instance = Instantiate(wall);
			//instance.transform.position += new Vector3(1,0,0);
		
			//Debug.Log("Started: " + started);
			started = true;
			//doOnce();
		}
	}

	void doOnce(){
		var instance = Instantiate(wall);
		instance.transform.position += new Vector3(1, 0, 0);
	}

	void recursiveDivision(int width, int height, int offsetX, int offsetY){
		Debug.Log("Recursive Start");
		// Check for end conditions
		if (width < 2 || height < 2){
			return;
		}

		// Choose a random split either horizontal of vertical
		int horizontal = Random.Range(1,100) % 2;
		int wall, path;

		if(horizontal == 1){
			// Choose where to make the wall, and where the open path will be
			wall = Random.Range(offsetY, height);
			path = Random.Range(offsetX, width);

			// Fill them in our maze
			for(int i = offsetX; i < width; i++){
				if(i != path){
					maze[wall, path] = true;
				}else{
					maze[wall, path] = false;
				}
			}

			// call recursiveDivision for the top and bottom half
			recursiveDivision(width - wall, height, offsetX, offsetY);
			recursiveDivision(width - wall, height, offsetX + wall, offsetY);
		}
		else{
			// Choose where to make the wall, and where the open path will be
			wall = Random.Range(offsetY, height);
			path = Random.Range(offsetX, width);

			// Fill them in on our maze
			for (int i = offsetY; i < height; i++){
				if (i != path){
					maze[path, wall] = true;
				}
				else{
					maze[path, wall] = false;
				}
			}

			// call recursiveDivision for the left and right half
			recursiveDivision(width, height - wall, offsetX, offsetY);
			recursiveDivision(width, height - wall, offsetX, offsetY + wall);
		}

		Debug.Log("Recursive Div End");
	}
}
