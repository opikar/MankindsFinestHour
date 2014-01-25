using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour {
	
	float speed = 10f;
	float jumpForce = 13f;
	
	bool facingRight;
	
	//jumping variables
	float groundRadius = 0.26f;
	bool grounded = true;
	bool doubleJump = false;
	public Transform groundCheck;
	public Transform groundCheck2;
	public LayerMask whatIsGround;
	
	//variables for going through platforms
	int playerLayer;
	int passGroundLayer;
	float layerRadius = 0.4f;
	bool isInGround;
	
	LayerMask groundMask;
	LayerMask playerMask;
	RaycastHit2D hit;
	// Use this for initialization
	void Start () {
		//groundCheck = transform.Find ("groundCheck");
		//groundMask = LayerMask.NameToLayer("Ground");
		//playerMask = LayerMask.NameToLayer("Player");
		playerLayer = LayerMask.NameToLayer("Player");
		passGroundLayer = LayerMask.NameToLayer("PassPlatforms");
	}
	
	// Update is called once per frame
	void Update () {
		
		/*hit = Physics2D.Linecast(transform.position, groundCheck.position, 1 << groundMask);
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
		}*/
		if(rigidbody2D.velocity.y > 0)
			gameObject.layer = passGroundLayer;
		else{
			isInGround = Physics2D.OverlapCircle(transform.position, layerRadius, whatIsGround);
			if(!isInGround)
				gameObject.layer = playerLayer;
		}
		grounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround) 
			|| Physics2D.OverlapCircle(groundCheck2.position, groundRadius, whatIsGround);
		if(grounded)
			doubleJump = false;
		
		
	}
	
	public void Move(float direction){
		rigidbody2D.velocity = new Vector2(speed * direction, rigidbody2D.velocity.y);
	}
	public void Jump(){
		if(grounded || !doubleJump){
			rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, Vector2.up.y * jumpForce);
			if(!grounded)
				doubleJump = true;
		}
	}
	public void Drop(){
		
	}
	void Flip(){
		
	}
}
