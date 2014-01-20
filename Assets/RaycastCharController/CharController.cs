using UnityEngine;
using System.Collections;

public class CharController : MonoBehaviour {
	public float fallSpeed = 20;
	public float jumpSpeed = 16;
	public float speed = 10;
	public float skin = 0.001f;
	private Vector3 movement;
	private bool grounded = false;
	private bool drop = false;
	private float groundLevel = 0;
	private float wallPosition = 0;
	
	void Start () {
	}

	void FixedUpdate () {
		if(CheckGround()) {
			grounded = true;

			if(CheckSides()) {
				if(movement.x < 0) {
					if(wallPosition < transform.position.x) {
						movement.x = 0;
					}
				} else if(movement.x > 0) {
					if(wallPosition > transform.position.x) {
						movement.x = 0;
					}
				}
			}

			Vector3 target = transform.position;
			target.y = groundLevel+transform.localScale.y/2+skin;

			if(movement.y != 0)
				movement -= new Vector3(0, fallSpeed*Time.deltaTime, 0);
			if(movement.y + transform.position.y <= target.y) {
				movement.y = 0;
				transform.position = target;
			}
		} else {
			grounded = false;
			movement -= new Vector3(0, fallSpeed*Time.deltaTime, 0);
		}

		transform.position += (Vector3)(movement*Time.deltaTime);
	}

	void Update() {
		if(Input.GetKey ("down") && Input.GetButton("Jump"))
			drop = true;
		else
			drop = false;

		if(grounded) {
			if(Input.GetButtonDown("Jump")) {
				movement.y = jumpSpeed;
			}
		} else {
			if(Input.GetKeyDown("down"))
				movement.y = -jumpSpeed;
		}



		movement.x = Input.GetAxis("Horizontal")*speed;
	}

	bool CheckSides() {
		Vector3 position = transform.position;

		float distance = transform.localScale.x/2 + skin;

		RaycastHit2D hit = Physics2D.Raycast(position, -Vector2.right, distance);
		if(hit.fraction != 0) {
			wallPosition = hit.point.x;
			return true;
		}
		hit = Physics2D.Raycast(position, Vector2.right, distance);
		if(hit.fraction != 0) {
			wallPosition = hit.point.x;
			return true;
		}

		return false;
	}

	bool CheckGround() {
		if(drop)
			return false;

		Vector3 position = transform.position;
		position -= transform.localScale/2;
		float step = transform.localScale.x/2;

		float distance = transform.localScale.y/2 + skin;

		for(int i = 0; i < 3; i++) {
			RaycastHit2D hit = Physics2D.Raycast(position, -Vector2.up, distance);
			Debug.Log(hit.fraction);
			if(hit.fraction != 0 && movement.y <= 0) {
				groundLevel = hit.point.y;
				return true;
			}
			position.x += step;
		}
		return false;
	}
}
