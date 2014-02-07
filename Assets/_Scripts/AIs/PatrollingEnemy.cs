using UnityEngine;
using System.Collections;

public class PatrollingEnemy : MonoBehaviour {

	private Vector2 v_rightWaypoint;
	private Vector2 v_leftWaypoint;
	private float f_move;
	private Vector2 v_target;
	private Movement movement;

	// Use this for initialization
	void Start () {
		movement = GetComponent<Movement>();
	}
	
	// Update is called once per frame
	void Update () {
		if(Vector2.Distance(v_target, transform.position) < 2f && f_move == 1f){
			v_target = v_leftWaypoint;
			f_move = -1f;
		}
		else if(Vector2.Distance(v_target, transform.position) < 2f && f_move == -1f){
			v_target = v_rightWaypoint;
			f_move = 1f;
		}
		if(v_leftWaypoint == Vector2.zero && v_rightWaypoint == Vector2.zero)
			f_move = 0;
		movement.Move(f_move);
	}

	void OnCollisionEnter2D(Collision2D other){
		if(f_move == 0){
			f_move = -1f;
			if(other.gameObject.tag == "Ground")
				SetPatrolRoute(other.gameObject.transform);
			v_target = v_leftWaypoint;
		}
	}

	void OnTriggerEnter2D(Collider2D other){
		if(f_move == 0){	
			f_move = -1f;
			if(other.gameObject.tag == "Ground")
				SetPatrolRoute(other.gameObject.transform);
			v_target = v_leftWaypoint;
		}
	}

	void OnTriggerExit2D(Collider2D other){
		ResetPatrolRoute();
	}
	void OnCollisionExit2D(Collision2D other){
		ResetPatrolRoute();
	}

	void SetPatrolRoute(Transform other){
		v_leftWaypoint = new Vector2(other.position.x - other.localScale.x * .5f, transform.position.y);
		v_rightWaypoint = new Vector2(other.position.x + other.localScale.x * .5f, transform.position.y);
	}

	void ResetPatrolRoute(){
		v_leftWaypoint = Vector2.zero;
		v_rightWaypoint = Vector2.zero;
	}
}
