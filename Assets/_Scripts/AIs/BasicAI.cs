using UnityEngine;
using System.Collections;

public class BasicAI : MonoBehaviour {
	
	private RaycastHit2D hit;

	protected bool SeePlayer(Transform t, int viewDirection, float seePlayerDistance, int mask){
		//for loop for more precise vision.
		//send ray from the top, middle and bottom of enemy
		for(int i = 0; i < 3; i++){	
			Vector2 origin = new Vector2(t.position.x, t.position.y - t.localScale.y * .5f + t.localScale.y * .5f * i);
			hit = Physics2D.Raycast(origin, t.right * viewDirection, seePlayerDistance, mask);
			if(hit){
				return true;
			}
		}
		return false;
	}
}
