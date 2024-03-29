﻿using UnityEngine;
using System.Collections;

public class PatrollingEnemy : BasicAI {

	public float seePlayerDistance = 20f;
	
	protected Vector3 v_rightWaypoint;
	protected Vector3 v_leftWaypoint;
	protected Vector3 v_target;
	protected Transform m_player;
	protected float f_move;

	// Use this for initialization
	protected override void Start () {
        base.Start();
        m_player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
		m_transform = transform;
		
	}
	
	// Update is called once per frame
	protected virtual void Update () {
		Move ();
		if(i_viewDirection == Mathf.Sign(m_player.position.x - m_transform.position.x)){
			if(SeePlayerCone(seePlayerDistance, m_player.position, GetFacingRight())){
				AimVertical();
				ShootPrimaryWeapon();
				f_move = 0;
			}
			else if(f_move == 0)
				f_move = i_viewDirection;
		}
		Move(f_move);
	}

	//when the enemy steps on a platform set 2 points at each end of the platform and start patrolling that route
	void OnCollisionEnter2D(Collision2D other){
		if(other.gameObject.tag == "Ground" && !GetFlipped())
			SetPatrolRoute(other.gameObject.transform);
	}

	void OnTriggerEnter2D(Collider2D other){
        if (gameObject.activeSelf)
        {
            if (other.gameObject.tag == "Ground" && !GetFlipped())
                SetPatrolRoute(other.gameObject.transform);
        }
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

		if(v_leftWaypoint == Vector3.zero && v_rightWaypoint == Vector3.zero)
			f_move = 0;
		if(GetFacingRight())
			i_viewDirection = 1;
		else
			i_viewDirection = -1;
	}

	public override void Reset ()
	{
		base.Reset ();
		ResetPatrolRoute();
	}

	void ChangeDirection()
	{
		if(v_target == v_rightWaypoint)
			v_target = v_leftWaypoint;
		else if(v_target == v_leftWaypoint)
			v_target = v_rightWaypoint;
		f_move *= -1;

	}
	
	void AimVertical ()
	{
		float direction = m_player.position.y - m_transform.position.y;
		float scale = (m_player.localScale.y + m_transform.localScale.y) * .5f;
		if (direction > scale)
			AimVertical (1f, 1f);
		else if (direction < -scale)
			AimVertical (-1f, -1f);
		else
			AimVertical (0, 0);
	}
}
