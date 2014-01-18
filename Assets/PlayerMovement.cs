using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

	public float speed = 10f;
	public float jumpSpeed = 600f;
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
		print (whatIsGround.value);
	}
	
	void Update()
	{
		//ignore player collision with ground when going up
		if(rigidbody2D.velocity.y > 0)
			Physics2D.IgnoreLayerCollision(9, 8, true);
		//collide with ground if not going up
		else
			Physics2D.IgnoreLayerCollision(def, 8, false);


		grounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);
		if(rigidbody2D.velocity.y > 0 && grounded)
			grounded = false;
		print (grounded);
		if(Input.GetButtonDown("Fire1") && grounded	)
			rigidbody2D.AddForce(new Vector2(0, jumpSpeed));
	}
	void FixedUpdate() {
		move = Input.GetAxis("Horizontal");
		rigidbody2D.velocity = new Vector2 (speed * move, rigidbody2D.velocity.y);
	}
}
