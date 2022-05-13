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

            int borderwidth = width + 1;
            int borderheight = height + 1;
            for (int i = 0; i < width + 1; i++)
            {
                var instance = Instantiate(wall);
                instance.transform.position += new Vector3(i, 0, borderheight);
            }
            for (int i = 0; i < borderheight; i++)
            {
                var instance = Instantiate(wall);
                instance.transform.position += new Vector3(borderwidth, 0, i);
            }
            for (int i = 2; i < borderheight; i++)
            {
                var instance = Instantiate(wall);
                instance.transform.position += new Vector3(0, 0, i);
            }
            for (int i = 0; i < borderheight; i++)
            {
                var instance = Instantiate(wall);
                instance.transform.position += new Vector3(i, 0, 0);
            }

            recursiveDivision(width, height, 0, 0);

            maze[width / 2, height / 2] = false;
            maze[0, 2] = false;
            maze[0, 0] = false;
            maze[0, 1] = false;

            // Loop through and spawn each wall
            for (int i = 0; i < width; i++){
				for (int j = 0; j < height; j++){
					//Debug.Log("Maze [" + i + ", "+ j +"] = " + maze[i,j]);
					if (maze[i,j] == true) {
						var instance = Instantiate(wall);
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
        //wall = Random.Range(offsetY, height);
        //path = Random.Range(offsetX, width);

        wall = Random.Range(offsetY, height);
        path = Random.Range(offsetX, width);

        if (horizontal == 1){
			// Choose where to make the wall, and where the open path will be
			//wall = Random.Range(offsetY, height);
			//path = Random.Range(offsetX, width);

			// Fill them in our maze
			for(int i = offsetX; i < width; i++){
                //if(i != path){
                //	maze[wall, path] = true;
                //}else{
                //	maze[wall, path] = false;
                Debug.Log("Maze iteration: [" + wall + ", " + i + "]");
                maze[wall, i] = true;
			}
            maze[wall, path] = false;

			// call recursiveDivision for the top and bottom half
			recursiveDivision(width, (wall - offsetY), offsetX, offsetY);
			recursiveDivision(width, offsetY + height - wall, offsetX + wall, wall);
		}
		else{
			// Choose where to make the wall, and where the open path will be
			//wall = Random.Range(offsetX, width);
			//path = Random.Range(offsetY, height);

			// Fill them in on our maze
			for (int i = offsetY; i < height; i++){
                //if (i != path){
                //	maze[path, wall] = true;
                //}
                //else{
                //	maze[path, wall] = false;
                //}
                Debug.Log("Maze iteration: [" + wall + ", " + i + "]");
                maze[i, wall] = true;
			}
            maze[path, wall] = false;

			// call recursiveDivision for the left and right half
			recursiveDivision(wall - offsetX, height, offsetX, offsetY);
			recursiveDivision(width + offsetX - wall, height - wall, wall, offsetY);
		}
		Debug.Log("Recursive Div End");
	}
}
