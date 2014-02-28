using UnityEngine;
using System.Collections;

public class BasicAI : MonoBehaviour {
	
	private RaycastHit2D hit;

	protected int i_mask;
	protected int i_viewDirection;
	protected Transform m_transform;

	protected bool SeePlayer(float seePlayerDistance){
		//for loop for more precise vision.
		//send ray from the top, middle and bottom of enemy
		for(int i = 0; i < 3; i++){	
			Vector2 origin = new Vector2(m_transform.position.x, m_transform.position.y - m_transform.localScale.y * .5f + m_transform.localScale.y * .5f * i);
			hit = Physics2D.Raycast(origin, m_transform.right * i_viewDirection, seePlayerDistance, i_mask);
			if(hit){
				return true;
			}
		}
		return false;
	}
	
	protected bool SeePlayerHalfCircle(float seePlayerDistance, Vector3 playerPosition, bool facingRight)
	{
		if (seePlayerDistance * seePlayerDistance > (playerPosition - m_transform.position).sqrMagnitude) 
		{
			Vector3 distance = playerPosition - m_transform.position;
			if(distance.x + 5f > distance.y)
			{
			}
			else if(!facingRight && playerPosition.x < m_transform.position.x || 
			        facingRight && playerPosition.x > m_transform.position.x)
			{
				return true;
			}
		} 
		return false;
	}
}
