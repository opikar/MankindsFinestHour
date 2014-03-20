using UnityEngine;
using System.Collections;

public class LiftPlatform : MonoBehaviour {

	public float speed = 2f;

	private bool lift = false;
	private Vector3 originalPosition;
	private Transform _transform;
	private RaycastHit2D hit;

	// Use this for initialization
	void Start () {
		_transform = transform;
		originalPosition = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		if (Physics2D.OverlapCircle (_transform.position, 3f, 1 << LayerMask.NameToLayer ("Player")))
						lift = true;
				else
						lift = false;
		print (lift);
		if (lift)
			rigidbody2D.velocity = new Vector2 (0, -speed);
		else if (!lift && _transform.position.y < originalPosition.y)
		{
			rigidbody2D.velocity = new Vector2 (0, speed);
			lift = false;
		}
		else if (_transform.position.y > originalPosition.y) 
		{
			rigidbody2D.velocity = Vector2.zero;
			_transform.position = originalPosition;
			lift = false;
		}
	}
}
