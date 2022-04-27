using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewBehaviourScript : MonoBehaviour {
	public int mapSize = 50;

	private void Start(){
		//mapSize = mapSize-1;
	}
	
	// Possible Maze Generation Algorithm Code:
	// https://weblog.jamisbuck.org/2011/1/12/maze-generation-recursive-division-algorithm

	bool[,] GenerateMap(){
		bool[,] map = new bool[mapSize, mapSize]; ;

		for(int i = 0; i < mapSize; i++){
			for(int j = 0; j < mapSize; j++){
				if(i == 0 || i == mapSize-1){
					map[i, j] = true;
				}
				else{
					if(j == 0 || j == mapSize-1){
						map[i, j] = true;
					}
					else{

					}
				}
			}
		}

		return map;
	}
}
