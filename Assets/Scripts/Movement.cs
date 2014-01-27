using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour {

	//moving variable
	float speed = 10f;
	
	public bool facingRight {
		get;
		private set;
	}
	
	//jumping variables
	float jumpForce = 13f;
	float groundRadius = 0.26f;
	bool grounded = true;
	bool doubleJump = false;
	Transform groundCheck;
	Transform groundCheck2;
	public LayerMask whatIsGround;
	
	//variables for jumping through platforms
	int playerLayer;
	int passGroundLayer;
	float layerRadius = 0.4f;
	bool isInGround;

	//variables for dropping through platforms
	bool dropping;
	float dropTimer;
	float dropDuration = 0.5f;

	// Use this for initialization
	void Start () {
		groundCheck = transform.Find ("groundCheck");
		groundCheck2 = transform.Find ("groundCheck");
		playerLayer = LayerMask.NameToLayer("Player");
		passGroundLayer = LayerMask.NameToLayer("PassPlatforms");
		facingRight = true;
	}
	
	// Update is called once per frame
	void Update () {
		if(rigidbody2D.velocity.y > 0)
			gameObject.layer = passGroundLayer;
		else{
			isInGround = Physics2D.OverlapCircle(transform.position, layerRadius, whatIsGround);
			if(!isInGround)
				gameObject.layer = playerLayer;
		}


		if(rigidbody2D.velocity.x > 0 && !facingRight){
			facingRight = true;
			Flip();
		}
		if(rigidbody2D.velocity.x < 0 && facingRight){
			facingRight = false;
			Flip ();
		}


		//character is standing on ground if groundchecks are overlapping with ground
		grounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround) 
			|| Physics2D.OverlapCircle(groundCheck2.position, groundRadius, whatIsGround);

		//check if character is jumping through a platform and block him to triple jump
		if(isInGround)
			grounded = false;
		//if the character is on ground he can use double jump
		if(grounded)
			doubleJump = false;
	}

	/// <summary>
	/// Set the horizontal speed of the gameobject to given direction -1 for left and 1 for right
	/// </summary>
	/// <param name="direction">Direction.</param>
	public void Move(float direction){
		rigidbody2D.velocity = new Vector2(speed * direction, rigidbody2D.velocity.y);
	}
	/// <summary>
	/// Sets the rigid bodys Y-velocity to jump speed if able to jump
	/// </summary>
	public void Jump(){
		if((grounded || !doubleJump) && !isInGround){
			rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, Vector2.up.y * jumpForce);
			if(!grounded)
				doubleJump = true;
		}
	}
	public void Drop(){

	}
	void Flip(){
		transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
	}
}
