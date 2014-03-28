using UnityEngine;
using System.Collections;

public class FlappyBossMovement : Movement {

	public override void Move(float direction)
	{
		flipped = false;
		
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
}
