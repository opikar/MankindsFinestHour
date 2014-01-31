using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour {

	//moving variable
	[SerializeField]
	float speed = 10f;
	
	public bool facingRight {
		get;
		private set;
	}
	
	//jumping variables
	[SerializeField]
	float jumpForce = 13f;
	bool grounded = true;
	bool doubleJump = false;

	// Use this for initialization
	void Start () {
		facingRight = true;
	}
	
	void Update () {
		if(rigidbody2D.velocity.y > 0)
			rigidbody2D.gravityScale = 1;

		if(rigidbody2D.velocity.x > 0 && !facingRight){
			facingRight = true;
			Flip();
		}
		if(rigidbody2D.velocity.x < 0 && facingRight){
			facingRight = false;
			Flip ();
		}

		grounded = rigidbody2D.gravityScale == 0 ? true : false;

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
		if(grounded || !doubleJump){
			rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, Vector2.up.y * jumpForce);
			if(!grounded)
				doubleJump = true;
		}
	}
	public void Drop(){
		if(rigidbody2D.gravityScale == 0)
			rigidbody2D.gravityScale = 1;
	}
	void Flip(){
		transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
	}

	void OnTriggerEnter2D(Collider2D other){

		if(other.tag == "Ground" && rigidbody2D.velocity.y < 0){
			if(transform.position.x + Mathf.Abs (transform.localScale.x/2) >= other.transform.position.x - other.transform.localScale.x/2 &&
			   transform.position.x - Mathf.Abs (transform.localScale.x/2) <= other.transform.position.x + other.transform.localScale.x/2)
			{
				rigidbody2D.gravityScale = 0;
				rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, 0);
				transform.position = new Vector2(transform.position.x, other.transform.position.y + 0.5f *(other.transform.localScale.y + transform.localScale.y));
			}
		}
	}
	
	void OnTriggerExit2D(Collider2D other){
		rigidbody2D.gravityScale = 1;
	}

}
