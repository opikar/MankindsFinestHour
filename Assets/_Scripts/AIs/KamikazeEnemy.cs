using UnityEngine;
using System.Collections;

public class KamikazeEnemy : PatrollingEnemy {

	public float reactionTime = 1f;

	private bool b_seePlayer = false;
	private bool b_charge = false;
	private float f_chargeSpeed = 20f;
	private float f_chargeStart;
	private PlayerManager m_playerManager;
	// Use this for initialization
	protected override void Start () {
		base.Start();
		m_playerManager = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManager>();
		f_chargeStart = Time.time;
	}
	
	// Update is called once per frame
	protected override void Update () {
		b_seePlayer = (SeePlayer(seePlayerDistance) || b_seePlayer); //&& renderer.isVisible;
		if(!b_seePlayer){
			Move();
			b_charge = true;
			f_chargeStart = Time.time;
		}
		else if(f_chargeStart + reactionTime < Time.time && b_charge)
		{
			Charge();
			f_move = i_viewDirection;
		}
		else
			f_move = 0;
		Move(f_move);
	}

	void Charge(){
		if(Vector2.SqrMagnitude(m_transform.position - m_player.position) < 15f)
		{
			b_charge = false;
			Explode();
		}
		SetMoveSpeed(f_chargeSpeed);
	}

	void Explode()
	{
		//still need and exploding animation
		m_playerManager.ApplyDamage(m_playerManager.GetMaxHealth() * .5f);
		Die();
	}


}
