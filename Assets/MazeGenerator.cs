using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeGenerator : MonoBehaviour {
	public int width = 50, height = 50;
	private bool[][] maze;
	private List<GameObject> walls = new List<GameObject>();
	public GameObject wall;

	// Use this for initialization
	void Start () {
		recursiveDivision(width, height, 0, 0);

		// Loop through and spawn each wall
		for (int i = 0; i < width; i++){
			for (int j = 0; j < height; j++){
				var instance = Instantiate(wall, wall.transform);
				instance.transform.localPosition = new Vector3(i,0,j);
				walls.Add(instance);
			}
		}
	}

	void recursiveDivision(int width, int height, int offsetX, int offsetY){
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
					maze[wall][path] = true;
				}else{
					maze[wall][path] = false;
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
					maze[path][wall] = true;
				}
				else{
					maze[path][wall] = false;
				}
			}

			// call recursiveDivision for the left and right half
			recursiveDivision(width, height - wall, offsetX, offsetY);
			recursiveDivision(width, height - wall, offsetX, offsetY + wall);
		}
	}
}
