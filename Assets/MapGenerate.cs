using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewBehaviourScript : MonoBehaviour {
	// Variables
	public bool inMenu = true;
	public float moveSpeed = 6.0F;
	private Vector3 moveDirection = Vector3.zero;

	// Use this for initialization
	void Start () {
		

	}
	
	// Update is called once per frame
	void Update () {
		CharacterController controller = GetComponent<CharacterController>();

		// If not in a menu then get movement direction input
		if (!inMenu){
			moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
			moveDirection *= moveSpeed;
		}

		controller.Move(moveDirection * Time.deltaTime);
	}

	public string firstLevel;

	public void StartGame(){
		SceneManager.LoadScene(firstLevel);
	}

	public void QuitGame(){
		Application.Quit();
	}
}
