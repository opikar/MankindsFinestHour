using UnityEngine;
using System.Collections;

public class PatrollingEnemy : MonoBehaviour {

	private Vector2 v_rightWaypoint;
	private Vector2 v_leftWaypoint;
	private float f_move;
	private Vector2 v_target;
	private float f_seePlayerDistance;
	private EnemyManager enemyManager;
	private Transform m_transform;
	private int mask;
	RaycastHit2D hit;

	// Use this for initialization
	void Start () {
		mask = LayerMask.NameToLayer("Player");
		print (mask);
		m_transform = transform;
		f_seePlayerDistance = 20f;
		enemyManager = GetComponent<EnemyManager>();
	}
	
	// Update is called once per frame
	void Update () {
		print (SeePlayer());
		Move ();
		enemyManager.Move(f_move);
	}

	//when the enemy steps on a platform set 2 points at each end of the platform and start patrolling that route
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
		v_leftWaypoint = new Vector2(other.position.x - other.localScale.x * .5f, m_transform.position.y);
		v_rightWaypoint = new Vector2(other.position.x + other.localScale.x * .5f, m_transform.position.y);
	}

	void ResetPatrolRoute(){
		v_leftWaypoint = Vector2.zero;
		v_rightWaypoint = Vector2.zero;
	}

	void Move(){
		if(Vector2.Distance(v_target, m_transform.position) < 2f && f_move == 1f){
			v_target = v_leftWaypoint;
			f_move = -1f;
		}
		else if(Vector2.Distance(v_target, m_transform.position) < 2f && f_move == -1f){
			v_target = v_rightWaypoint;
			f_move = 1f;
		}
		if(v_leftWaypoint == Vector2.zero && v_rightWaypoint == Vector2.zero)
			f_move = 0;
	}

	bool SeePlayer(){
		hit = Physics2D.Raycast(m_transform.position, m_transform.right * f_move, f_seePlayerDistance, mask);
		if(hit){
				return true;
		}
		else 
			return false;
	}
}
