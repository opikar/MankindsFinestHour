using UnityEngine;
using System.Collections;

public class BulletLogic : MonoBehaviour {

	public float speed = 1000f;
	Vector2 velocity;
	// Use this for initialization
	void Start () {
		//set speed for the bullet when it's instantiated
		velocity = new Vector2(transform.right.x, transform.right.y) * speed;
		rigidbody2D.AddForce(velocity);
	}
	
	// Update is called once per frame
	void Update () {
		//if the bullet goes out of screen, destroy it
		if(!gameObject.renderer.isVisible)
		{
			Destroy(gameObject);
		}
	}
}
