using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

	public float speed = 10f;
	public float jumpSpeed = 600f;
	public float maxFallSpeed = 20;
	bool facingRight;
	float move;
	RaycastHit2D hit;
	bool grounded;
	Transform groundCheck;
	LayerMask groundMask;
	LayerMask playerMask;

	// Use this for initialization
	void Start () {
		groundCheck = transform.Find ("groundCheck");
		groundMask = LayerMask.NameToLayer("Ground");
		playerMask = LayerMask.NameToLayer("Player");
	}
	
	void Update()
	{
		hit = Physics2D.Linecast(transform.position, groundCheck.position, 1 << groundMask);
		//Check if we are inside a collider
		if(hit.fraction == 0)
			grounded = false;
		else
			grounded = true;

		if(Input.GetButton ("Jump") && Input.GetKey ("down")) {
			grounded = false;
			if(rigidbody2D.velocity == Vector2.zero)
				transform.Translate(-Vector2.up * 0.8f);
		}

		if(grounded) {
			Physics2D.IgnoreLayerCollision(groundMask, playerMask, false);
			if(Input.GetButtonDown("Jump"))
				rigidbody2D.AddForce(new Vector2(0, jumpSpeed));
			}
		else {
			//Disable collisions while we are in air
			Physics2D.IgnoreLayerCollision(groundMask, playerMask, true);
		}


	}
	void FixedUpdate() {
		move = Input.GetAxis("Horizontal");
		rigidbody2D.velocity = new Vector2 (speed * move, rigidbody2D.velocity.y);

		//Limit maximum velocity so we don't fall through floor
		if(rigidbody2D.velocity.y < -maxFallSpeed)
			rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, -maxFallSpeed);
	}
}
