﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndIndicator : MonoBehaviour {
	public Canvas P2;
	public CharacterController characterController;
	public Text angleText;

	//Edit this for the endpoint generated by the maze. Make it the center of the end space.
	private Vector3 EndPoint = new Vector3(-10, 0, -10);

	// Update is called once per frame
	void Update () {
		// Calculate the EndIndicator's angle of rotation for pointing towards the end
		// Get the 2d vector of the player, canvas in front of the player, and end point using their x and z
		Vector2 Point1 = new Vector2(characterController.transform.position.x, characterController.transform.position.z);
		Vector2 Point2 = new Vector2(P2.transform.position.x, P2.transform.position.z);
		Vector2 Point3 = new Vector2(EndPoint.x, EndPoint.z);
		
		// Takes a vector towards where the player is looking, and towards the end, and calculates the signed angle between them
		Vector2 direction = Point2 - Point1;
		Vector2 toEnd = Point3 - Point1;
		float angleDegrees = Vector2.SignedAngle(direction, toEnd);
		
		var eulerAngles = this.transform.eulerAngles;
		this.transform.rotation = Quaternion.Euler(eulerAngles.x, eulerAngles.y, angleDegrees);
	}
}
