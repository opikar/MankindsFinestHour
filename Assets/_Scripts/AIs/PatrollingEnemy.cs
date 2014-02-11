using UnityEngine;
using System.Collections;

public class PatrollingEnemy : BasicAI {

	public float seePlayerDistance = 20f;

	private int i_mask;
	private int i_viewDirection; 
	private EnemyManager enemyManager;
	private Transform m_transform;
	private Vector2 v_rightWaypoint;
	private Vector2 v_leftWaypoint;
	private Vector2 v_target;
	private float f_move;

	// Use this for initialization
	void Start () {
		m_transform = transform;
		enemyManager = GetComponent<EnemyManager>();
		i_mask = 1 << LayerMask.NameToLayer("Player");
	}
	
	// Update is called once per frame
	void Update () {
		if(SeePlayer(m_transform, i_viewDirection, seePlayerDistance, i_mask)){
			enemyManager.ShootPrimaryWeapon();
			f_move = 0;
		}
		else if(f_move == 0)
			f_move = i_viewDirection;
		Move ();
		enemyManager.Move(f_move);
	}

	//when the enemy steps on a platform set 2 points at each end of the platform and start patrolling that route
	void OnCollisionEnter2D(Collision2D other){
		if(other.gameObject.tag == "Ground")
			SetPatrolRoute(other.gameObject.transform);
	}

	void OnTriggerEnter2D(Collider2D other){
		if(other.gameObject.tag == "Ground")
			SetPatrolRoute(other.gameObject.transform);
	}

	void OnTriggerExit2D(Collider2D other){
		if(other.gameObject.tag == "Ground")
			ResetPatrolRoute();
	}
	void OnCollisionExit2D(Collision2D other){
		if(other.gameObject.tag == "Ground")
			ResetPatrolRoute();
	}

	void SetPatrolRoute(Transform other){
		f_move = -1f;
		v_leftWaypoint = new Vector2(other.position.x - other.localScale.x * .5f, m_transform.position.y);
		v_rightWaypoint = new Vector2(other.position.x + other.localScale.x * .5f, m_transform.position.y);
		v_target = v_leftWaypoint;
	}

	void ResetPatrolRoute(){
		v_leftWaypoint = Vector2.zero;
		v_rightWaypoint = Vector2.zero;
	}

	void Move(){
		if(Vector2.Distance(v_target, m_transform.position) < 2f && f_move == 1f){
			v_target = v_leftWaypoint;
			print (v_target + "target2");
			f_move = -1f;
		}
		else if(Vector2.Distance(v_target, m_transform.position) < 2f && f_move == -1f ){
			v_target = v_rightWaypoint;
			print (v_target + "target");
			f_move = 1f;
		}
		if(v_leftWaypoint == Vector2.zero && v_rightWaypoint == Vector2.zero)
			f_move = 0;
		if(enemyManager.GetFacingRight())
			i_viewDirection = 1;
		else
			i_viewDirection = -1;
	}
	
}
