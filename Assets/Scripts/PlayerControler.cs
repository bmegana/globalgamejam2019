using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//By JC

public class PlayerControler : MonoBehaviour {
	public float speed;
	private Rigidbody2D rb; // attached
	private Vector2 moveVelocity;

	void Start () {

		rb = GetComponent<Rigidbody2D> ();

	}

	void Update () {
		Vector2 moveInput = new Vector2(Input.GetAxisRaw("Horizontal"),Input.GetAxisRaw("Vertical")); // Input.GetAxis for smooth 
		moveVelocity = moveInput.normalized * speed; //just input
	}

	void FixedUpdate(){
		rb.MovePosition (rb.position + moveVelocity * Time.fixedDeltaTime);
	}
}


