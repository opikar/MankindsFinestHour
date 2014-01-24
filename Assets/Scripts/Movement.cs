using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour {

	float speed = 10f;
	float jumpForce = 500f;

	bool facingRight;

	public Transform groundCheck;
	public LayerMask whatIsGround;
	float groundRadius = 0.2f;
	bool grounded = false;
	bool doubleJump = false;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public void Move(float direction){
		rigidbody2D.velocity = new Vector2(speed * direction, rigidbody2D.velocity.y);
	}
	public void Jump(){
		grounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);
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
