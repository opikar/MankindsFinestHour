using UnityEngine;
using System.Collections;

public class ShakingPlatform : MonoBehaviour {

	public float speed = 1f;
	public float dropDelay = 2f;

	private bool b_shake = false;
	private bool b_drop = false;
	private float f_dropTimer;
	private Rigidbody2D r_rigidbody;
	// Use this for initialization
	void Start () {
		r_rigidbody = transform.Find("Sprite").GetComponent<Rigidbody2D>();
		print (r_rigidbody.gameObject.name);
	}
	
	// Update is called once per frame
	void Update () {
		if(b_shake)
			Shake();
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if(!b_drop && other.transform.position.y > transform.position.y)
		{
			if(other.tag == "Player"){
				if(!b_shake)
					f_dropTimer = Time.time;
				b_shake = true;
			}
		}
	}

	void Shake()
	{
		speed *= -1f;
		r_rigidbody.velocity = new Vector2(speed, 0);
		if(f_dropTimer + dropDelay < Time.time)
			Drop();
	}
	void Drop()
	{
		rigidbody2D.velocity = r_rigidbody.velocity = Vector2.zero;
		b_drop = true;
		b_shake = false;
		rigidbody2D.isKinematic = r_rigidbody.isKinematic = false;

	}
}
