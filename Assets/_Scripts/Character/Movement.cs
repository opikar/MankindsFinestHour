using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour
{

    #region MEMBERS
	public float f_speed = 10f;
	
	public bool facingRight 
    {
		get;
		private set;
	}
	
	//jumping variables
	public float f_jumpForce = 13f;
	public bool b_grounded = true;
	private bool b_doubleJump = false;
	public bool flipped = false;

	private Animator animator = null;
	private Transform m_groundCheck;
    private Transform m_groundCheck2;
    private LayerMask m_groundMask;
    private float f_groundRadius = 0.30f;
	private MovingPlatform platform;
    // Here I am just creating a variable with the same name as inherited members
    // The compiler will complain that it is hiding existing member
    // Using new on the front I explicitely tell to hide
    // Then I can use the variable just like the normal ones but faster
    private new Rigidbody2D rigidbody2D;
    private new Transform transform;
    #endregion

    #region UNITY_METHODS
    void Start () 
    {
        // Here we call the base.variable which is the slow one
        // and store that value into our new one.
		animator = GetComponent<Animator> ();
        rigidbody2D = base.rigidbody2D;
        transform = base.transform;
		m_groundCheck = transform.Find ("groundCheck");
		m_groundCheck2 = transform.Find ("groundCheck2");
		m_groundMask = 1 << LayerMask.NameToLayer("Ground");
		facingRight = true;
	}
	

    //////////////////////////////////////////////
    // Think if you really need the update here.
    // You are setting those values for a future use in Move
    // What if you only call them at the beginning of Move?
    //////////////////////////////////////////////
	void Update () 
    {

	}

	public void SetMoveSpeed(float speed)
	{
		f_speed = speed;
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Ground" && rigidbody2D.velocity.y < 0)
        {
            if (transform.position.x + Mathf.Abs(transform.localScale.x / 2) >= other.transform.position.x - other.transform.localScale.x / 2 &&
               transform.position.x - Mathf.Abs(transform.localScale.x / 2) <= other.transform.position.x + other.transform.localScale.x / 2 &&
			    !flipped)
            {
				if(other.gameObject.name == "MovingPlatform")
				{
					platform = other.gameObject.GetComponent<MovingPlatform>();
					transform.parent = other.gameObject.transform;
				}
                rigidbody2D.gravityScale = 0;
                rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, 0);
                transform.position = new Vector3(transform.position.x, other.transform.position.y + (0.5f * (other.transform.localScale.y + transform.localScale.y)), transform.position.z);
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
		if(other.tag == "Ground")
		{
			if(other.gameObject.name == "MovingPlatform")
			{
				platform = null;
				transform.parent = null;
			}
        	rigidbody2D.gravityScale = 1;
		}
    }
    #endregion

    #region METHODS
    /// <summary>
	/// Set the horizontal speed of the gameobject to given direction -1 for left and 1 for right
	/// </summary>
	/// <param name="direction">Direction.</param>
	public void Move(float direction)
    {
		flipped = false;
		if(rigidbody2D.velocity.y > 0)
			rigidbody2D.gravityScale = 1;
		
		if(rigidbody2D.velocity.x > 0 && !facingRight)
		{
			Flip();
		}
		if(rigidbody2D.velocity.x < 0 && facingRight)
		{
			Flip ();
		}

		//grounded = rigidbody2D.gravityScale == 0 ? true : false;
		//character is standing on ground if groundchecks are overlapping with ground
		b_grounded = Physics2D.OverlapCircle(m_groundCheck.position, f_groundRadius, m_groundMask) ||
			Physics2D.OverlapCircle(m_groundCheck2.position, f_groundRadius, m_groundMask);
		
		//if the character is on ground he can use double jump
		if(b_grounded)
			b_doubleJump = false;


		if (platform != null)
		{
			float platformVelocity = 0;
			platformVelocity = platform.MoveDirection.x;
			Vector2 vel = new Vector2(f_speed, rigidbody2D.velocity.y);
			if(platformVelocity > 0 && direction < 0)
				vel.x += platformVelocity;
			else if(platformVelocity < 0 && direction > 0)
				vel.x -= platformVelocity;
			vel.x *= direction;
			rigidbody2D.velocity = vel;
			//print (platformVelocity);
		}
		else
			rigidbody2D.velocity = new Vector2(f_speed * direction, rigidbody2D.velocity.y);
		if(animator != null)
			animator.SetFloat ("Speed", Mathf.Abs(rigidbody2D.velocity.x));
	}

	/// <summary>
	/// Sets the rigid bodys Y-velocity to jump speed if able to jump
	/// </summary>
	public void Jump()
    {
		if(b_grounded || !b_doubleJump)
        {
			rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, Vector2.up.y * f_jumpForce);
			if(!b_grounded)
				b_doubleJump = true;
		}
	}
    
    /// <summary>
    /// Character goes through the ground
    /// </summary>
	public void Drop()
    {
		print (transform.parent);
		if(rigidbody2D.gravityScale == 0)
			rigidbody2D.gravityScale = 1f;
	}

	private void Flip()
    {
		facingRight = !facingRight;
		flipped = true;
		Vector3 scale = transform.localScale;
		scale.x *= -1f;
		transform.localScale = scale;
    }
    #endregion
}
