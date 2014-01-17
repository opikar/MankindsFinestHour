using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

	public float speed = 10f;
	public float jumpSpeed = 100f;
	bool facingRight;
	float move;
	bool grounded = false;
	float groundRadius = 0.2f;
	public Transform groundCheck;
	public LayerMask whatIsGround;
	int def;

	// Use this for initialization
	void Start () {
		def = gameObject.layer;
	}
	
	// Update is called once per frame
	void FixedUpdate() {
		if(rigidbody2D.velocity.y > 0)
			gameObject.layer = 0;
		else
			gameObject.layer = def;
		grounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);
		if(Input.GetButtonDown("Fire1") && grounded	)
			rigidbody2D.AddForce(new Vector2(0, jumpSpeed));
		move = Input.GetAxis("Horizontal");
		rigidbody2D.velocity = new Vector2 (speed * move, rigidbody2D.velocity.y);
	}
}
