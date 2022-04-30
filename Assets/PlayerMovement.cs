using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour {
	// Variables
	public float moveSpeed = 2.0F;
	public float mouseSpeed = 4.0F;
	private Vector3 moveDirection = Vector3.zero;
	private Vector3 cameraRotation = Vector3.zero;

	private float yRot = 0.0F;
	private float xRot = 0.0F;
	
	// Update is called once per frame
	void Update () {
		CharacterController controller = GetComponent<CharacterController>();

		//Feed moveDirection with input
		moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
		moveDirection = transform.TransformDirection(moveDirection);
		// Multiply it by speed
		moveDirection *= moveSpeed;

		//Get and camera rotation through mouse movement
		yRot += Input.GetAxis("Mouse X");

		if (!(xRot + -Input.GetAxis("Mouse Y") > 90 || xRot + -Input.GetAxis("Mouse Y") < -90)){
			xRot += -Input.GetAxis("Mouse Y");
		}

		// Move the Character
		controller.Move(moveDirection * Time.deltaTime);
		// Do special rotational transform calculations so we cannot rotate our Z
		controller.transform.localEulerAngles = new Vector3(xRot, yRot, transform.localEulerAngles.z);
	}
}
