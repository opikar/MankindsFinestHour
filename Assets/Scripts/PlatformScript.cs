using UnityEngine;
using System.Collections;

public class PlatformScript : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D other){
		if((other.tag == "Player" || other.tag == "Enemy") && other.rigidbody2D != null && other.rigidbody2D.velocity.y < 0){
			if(other.transform.position.x - other.transform.localScale.x * .49f < transform.position.x + transform.localScale.x * .5f
			   && other.transform.position.x + other.transform.localScale.x * .49f > transform.position.x - transform.localScale.x * .5f){
				other.rigidbody2D.gravityScale = 0;
				other.rigidbody2D.velocity = new Vector2(other.rigidbody2D.velocity.x, 0);
				other.transform.position = new Vector2(other.transform.position.x, transform.position.y + 0.5f *(transform.localScale.y + other.transform.localScale.y));
			}
		}
	}

	void OnTriggerExit2D(Collider2D other){
		if((other.tag == "Player" || other.tag == "Enemy") && other.rigidbody2D != null){
			other.rigidbody2D.gravityScale = 1;
		}
	}
}
