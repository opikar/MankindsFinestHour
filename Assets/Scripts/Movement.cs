using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour {

	float speed = 10f;
	float jumpForce = 700f;

	bool facingRight;

	Transform groundCheck;
	LayerMask groundMask;
	LayerMask playerMask;
	public LayerMask whatIsGround;
	float groundRadius = 0.2f;
	bool grounded = false;
	bool doubleJump = false;
	RaycastHit2D hit;
	// Use this for initialization
	void Start () {
		groundCheck = transform.Find ("groundCheck");
		groundMask = LayerMask.NameToLayer("Ground");
		playerMask = LayerMask.NameToLayer("Player");
	}
	
	// Update is called once per frame
	void Update () {
		hit = Physics2D.Linecast(transform.position, groundCheck.position, 1 << groundMask);
		//Check if we are inside a collider
		if(hit.fraction == 0)
			grounded = false;
		else
			grounded = true;

		if(grounded) {
			Physics2D.IgnoreLayerCollision(groundMask, playerMask, false);

		}
		else {
			//Disable collisions while we are in air
			Physics2D.IgnoreLayerCollision(groundMask, playerMask, true);
		}
	}

	public void Move(float direction){
		rigidbody2D.velocity = new Vector2(speed * direction, rigidbody2D.velocity.y);
	}
	public void Jump(){
		//grounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);
		if(grounded || !doubleJump){
			rigidbody2D.AddForce(Vector2.up * jumpForce);
			if(!grounded)
				doubleJump = true;
		}
		if(grounded)
			doubleJump = false;

	}
	public void Drop(){

	}
	void Flip(){

	}
}
