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
	protected bool b_doubleJump = false;
	public bool flipped = false;

	private Character character;

	protected Animator animator = null;
	protected Transform m_groundCheck;
	protected Transform m_groundCheck2;
	public LayerMask m_groundMask;
	protected float f_groundRadius = 0.30f;
	protected MovingPlatform platform;
    // Here I am just creating a variable with the same name as inherited members
    // The compiler will complain that it is hiding existing member
    // Using new on the front I explicitely tell to hide
    // Then I can use the variable just like the normal ones but faster
	protected new Rigidbody2D rigidbody2D;
	protected new Transform transform;
	protected Vector2 colliderSize;
	private float previousX = 0;
    #endregion

    #region UNITY_METHODS
    protected void Awake () 
    {
        // Here we call the base.variable which is the slow one
        // and store that value into our new one.
		character = GetComponent<Character> ();
        rigidbody2D = base.rigidbody2D;
        transform = base.transform;
		m_groundCheck = transform.Find ("groundCheck");
		m_groundCheck2 = transform.Find ("groundCheck2");
		m_groundMask = 1 << LayerMask.NameToLayer("Ground");
		facingRight = true;
		colliderSize = GetComponent<BoxCollider2D>().size;
	}
	

    //////////////////////////////////////////////
    // Think if you really need the update here.
    // You are setting those values for a future use in Move
    // What if you only call them at the beginning of Move?
    //////////////////////////////////////////////


    //Animations and ground checking are in update instead of Move() because they need to be updated when gamestate is not running
	void Update () 
    {
        b_grounded = Physics2D.OverlapCircle(m_groundCheck.position, f_groundRadius, m_groundMask) ||
            Physics2D.OverlapCircle(m_groundCheck2.position, f_groundRadius, m_groundMask);
        character.SetAnimatorSpeed(Mathf.Abs(previousX - transform.position.x) / Time.deltaTime);
		previousX = transform.position.x;
        character.SetAnimatorGrounded(b_grounded);
        character.SetAnimatorYVelocity(rigidbody2D.velocity.y);
	}

	public void SetMoveSpeed(float speed)
	{
		f_speed = speed;
	}

    protected void OnTriggerEnter2D(Collider2D other)
    {
        if(gameObject.activeSelf)
        {
            if (other.tag == "Ground" && rigidbody2D.velocity.y < 0)
            {
                if (transform.position.x + Mathf.Abs(transform.localScale.x / 2) >= other.transform.position.x - other.transform.localScale.x / 2 &&
                   transform.position.x - Mathf.Abs(transform.localScale.x / 2) <= other.transform.position.x + other.transform.localScale.x / 2 )
                {
                    if (flipped)
                    {
                        if (rigidbody2D.velocity.y > 0 || other.transform.position.y > transform.position.y)
                            return;
                    }
                    if (other.gameObject.name == "MovingPlatform" || other.gameObject.name == "TransportPlatform")
                    {
                        if (transform.parent == null)
                        {
                            transform.parent = other.transform;
                            platform = other.gameObject.GetComponent<MovingPlatform>();
                        }
                    }
                    rigidbody2D.gravityScale = 0;
                    rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, 0);
                    transform.position = new Vector3(transform.position.x, other.transform.position.y + (0.5f * (colliderSize.y + other.transform.localScale.y)) /*other.transform.position.y + (1.5f * (other.transform.localScale.y + transform.localScale.y))*/, transform.position.z);
                }
            }
        }
    }

    protected void OnTriggerExit2D(Collider2D other)
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
	public virtual void Move(float direction)
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
		}
		else
			rigidbody2D.velocity = new Vector2(f_speed * direction, rigidbody2D.velocity.y);
		
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
		if(rigidbody2D.gravityScale == 0)
			rigidbody2D.gravityScale = 1f;
		else if(!b_grounded)
			Jump ();
	}

	public void Flip()
    {
		facingRight = !facingRight;
		flipped = true;
		Vector3 scale = transform.localScale;
		scale.x *= -1f;
		transform.localScale = scale;
    }
    #endregion
}
