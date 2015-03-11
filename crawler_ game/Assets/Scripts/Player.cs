﻿using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	private float speed;
	private Animator animator;
	private Controller controllerScript;
	private Vector3 position;
	private Vector3 newPosition;

	// called on start up
	void Start () {
		animator = gameObject.GetComponent<Animator> ();
		controllerScript = gameObject.GetComponent<Controller> ();

		position = transform.position;
		newPosition = transform.position;

		rotatePlayer (); // rotating him on start up to face the right way 
		speed = 2f;
	}


	// Called every frame
	void Update (){
	#if UNITY_STANDALONE || UNITY_EDITOR
		newPosition = controllerScript.keyboard(position);
		movePlayer();
	#else

		newPosition = controllerScript.touchScreen(position);
		movePlayer();
	
	#endif
	}


	// move the player towards destination
	private void movePlayer(){
		if (position != (newPosition))
			position = checkCollider (position, newPosition);

		transform.position = Vector3.MoveTowards(transform.position, position, Time.deltaTime * speed);
	}

	
	// check for player hitting colliders
	private Vector3 checkCollider(Vector3 start, Vector3 end) {
		rotatePlayer ();
		//Debug.DrawLine (start, end, Color.green); 										// shows linecast for debugging purposes
		
		bool wallCollision = Physics2D.Linecast (start, end, 1 << LayerMask.NameToLayer ("WallLayer"));	// cast a line and check if its a wall
		bool keyCollision = Physics2D.Linecast (start, end, 1 << LayerMask.NameToLayer ("KeyLayer"));	// cast a line and check if its a key

		if (keyCollision == true) {
			playerWalkAnim();
			return end;
		}

		if (wallCollision == true) {
			return start;
		}else {
			playerWalkAnim();
			return end;
		}
	}
                         
	                           


	// rotating player to face direction of movement
	private void rotatePlayer(){
		if (((position.x) == (newPosition.x)) && ((position.y) == (newPosition.y))) {
			transform.rotation = Quaternion.Euler (0, 0, 180f);
		}else if ((position.x) > (newPosition.x)) {
			transform.rotation = Quaternion.Euler (0, 0, 270f);
		} else if ((position.x) < (newPosition.x)) {
			transform.rotation = Quaternion.Euler (0, 0, 90f);
		} else if ((position.y) < (newPosition.y)) {
			transform.rotation = Quaternion.Euler (0, 0, 180f);
		} else if ((position.y) > (newPosition.y)) {
			transform.rotation = Quaternion.Euler (0, 0, 0f);
		}
	}

	private void playerWalkAnim(){
		animator.SetTrigger("Walk");
	}

}


