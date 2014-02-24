using UnityEngine;
using System.Collections;

public class PatrollingEnemy : BasicAI {

	public float seePlayerDistance = 20f;

	protected EnemyManager enemyManager;
	protected Vector3 v_rightWaypoint;
	protected Vector3 v_leftWaypoint;
	protected Vector3 v_target;
	protected float f_move;

	// Use this for initialization
	protected virtual void Start () {
		m_transform = transform;
		enemyManager = GetComponent<EnemyManager>();
		i_mask = 1 << LayerMask.NameToLayer("Player");
	}
	
	// Update is called once per frame
	protected virtual void Update () {
		Move ();
		if(SeePlayer(seePlayerDistance)){
			enemyManager.ShootPrimaryWeapon();
			f_move = 0;
		}
		else if(f_move == 0)
			f_move = i_viewDirection;
		enemyManager.Move(f_move);
	}

	//when the enemy steps on a platform set 2 points at each end of the platform and start patrolling that route
	void OnCollisionEnter2D(Collision2D other){
		if(other.gameObject.tag == "Ground" && !enemyManager.GetFlipped())
			SetPatrolRoute(other.gameObject.transform);
	}

	void OnTriggerEnter2D(Collider2D other){
		if(other.gameObject.tag == "Ground" && !enemyManager.GetFlipped())
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
		print ("setting patrol");
		v_leftWaypoint = new Vector3(other.position.x - other.localScale.x * .5f, m_transform.position.y, 0);
		v_rightWaypoint = new Vector3(other.position.x + other.localScale.x * .5f, m_transform.position.y, 0);
		v_target = v_leftWaypoint;
	}

	void ResetPatrolRoute(){
		v_leftWaypoint = Vector3.zero;
		v_rightWaypoint = Vector3.zero;
	}

	protected void Move(){
		if(Vector3.SqrMagnitude(v_target - m_transform.position) < 4f)
			ChangeDirection();

		/*if(Vector3.SqrMagnitude(v_rightWaypoint - m_transform.position) < 4f && v_target == v_rightWaypoint){
			ChangeDirection();
		}
		else if(Vector3.SqrMagnitude(v_leftWaypoint - m_transform.position) < 4f && v_target == v_leftWaypoint ){
			//this gets called 3-10 times when enemy reaches the left corner.
			ChangeDirection();
		}*/
		if(v_leftWaypoint == Vector3.zero && v_rightWaypoint == Vector3.zero)
			f_move = 0;
		if(enemyManager.GetFacingRight())
			i_viewDirection = 1;
		else
			i_viewDirection = -1;
	}

	void ChangeDirection()
	{
		if(v_target == v_rightWaypoint)
			v_target = v_leftWaypoint;
		else if(v_target == v_leftWaypoint)
			v_target = v_rightWaypoint;
		f_move *= -1;

	}
	
}
