using UnityEngine;
using System.Collections;

public abstract class Character : MonoBehaviour {

	public GameManager gameManager;
	public Health health;
	public Weapon weapon;
	public Movement movement;

	// Use this for initialization
	public virtual void Start () {
		movement = GetComponent<Movement>();
		health = GetComponent<Health>();
		weapon = GetComponent<Weapon>();
		gameManager = GameObject.Find("Manager").GetComponent<GameManager>();
		gameManager.SetState(State.Running);
	}

	public virtual void ApplyDamage (float damage)
	{
		health.ApplyDamage(damage);
	}

	public void ShootNormal(){
		weapon.ShootNormalGun();
	}

	public abstract void Die();
}
