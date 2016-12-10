using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.Events;


[RequireComponent (typeof (Controller))]
public class Player : MonoBehaviour {

	public float jumpHeight = 4;
	public float timeToJumpApex = .4f;
	float accelerationTimeAirborne = .2f;
	float accelerationTimeGrounded = .1f;
	float moveSpeed = 8;

	float gravity;
	float jumpVelocity;
	public Vector3 velocity;
	float velocityXSmoothing;



	Controller controller;

	void Start() {
		controller = GetComponent<Controller> ();

//		gravity = -(2 * jumpHeight) / Mathf.Pow (timeToJumpApex, 2);
		gravity = -50f;
		jumpVelocity = Mathf.Abs(gravity) * timeToJumpApex;
	}


	void Update() {
		if (controller.collisions.above)
			velocity.y = 0;

		if(Input.GetKeyDown(KeyCode.DownArrow) && controller.collisions.below) {
			moveSpeed = 12;
		}
		if(Input.GetKeyUp(KeyCode.DownArrow)) {
			moveSpeed = 8;
		}
		Vector2 input = new Vector2 (Input.GetAxisRaw ("Horizontal"), Input.GetAxisRaw ("Vertical"));

		if (Input.GetKeyDown (KeyCode.Space) && controller.collisions.below || Input.GetKeyDown(KeyCode.UpArrow) && controller.collisions.below) {
			Jump ();
		}

		float targetVelocityX = input.x * moveSpeed;
		velocity.x = Mathf.SmoothDamp (velocity.x, targetVelocityX, ref velocityXSmoothing, (controller.collisions.below)?accelerationTimeGrounded:accelerationTimeAirborne);

		if (!controller.collisions.below) {
			velocity.y += gravity * Time.deltaTime;
		}



		controller.Move (velocity * Time.deltaTime);
	}

	public void Jump() {
		velocity.y = jumpVelocity;
	}
		
}

