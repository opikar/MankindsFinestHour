using UnityEngine;
using System.Collections;

/// <summary>
/// Character.cs
/// Base clas for all character objects in the game.
/// </summary>
public abstract class Character : MonoBehaviour
{
    #region MEMBERS
    protected GameManager gameManager;
	protected Health health;
	protected Weapon weapon;
	protected Movement movement;
	protected Transform m_transform;
	protected Animator animator;
    #endregion

    #region UNITY_METHODS
    protected virtual void Awake () 
    {
		animator = GetComponent<Animator> ();
		m_transform = transform;
		movement = GetComponent<Movement>();
        if (movement == null) 
        {
            movement = gameObject.AddComponent<Movement>();
        }
		health = GetComponent<Health>();
        if (health == null)
        {
            gameObject.AddComponent<Health>();
        }
		weapon = GetComponent<Weapon>();
        if (weapon == null)
        {
            gameObject.AddComponent<Weapon>();
        }
      
		gameManager = GameObject.Find("Manager").GetComponent<GameManager>();
        if (gameManager != null)
        {
            gameManager.SetState(State.Running);
        }

        if (gameManager == null || movement == null || health == null || weapon == null)
        {
            Debug.LogError("All components not loaded properly in Character.cs");
        }
	}
    #endregion

    #region METHODS
    public abstract void Die();
    public virtual void ApplyDamage (float damage)
	{
		health.ApplyDamage(damage);
	}

	public void AimVertical (float axisVertical, float axisHorizontal)
	{
		SetAnimatorAim (axisVertical);
		weapon.MoveShootingTarget(axisVertical, axisHorizontal);
	}

	public void ShootPrimaryWeapon()
    {
		weapon.ShootPrimaryWeapon();
	}

	public void ShootSpecialWeapon()
	{
		weapon.ShootSpecialGun();
	}

	public void SetTarget(Transform shootSpawn)
	{
		weapon.SetTarget(shootSpawn);
	}

	public void Jump()
	{
		movement.Jump ();
	}
	public void Flip()
	{
		movement.Flip ();
	}
	public void Drop()
	{
		movement.Drop ();
	}


	public bool GetFacingRight()
	{
		return movement.facingRight;
	}

	public void SetMoveSpeed(float speed)
	{
		movement.SetMoveSpeed(speed);
	}

	public float GetMaxHealth()
	{
		return health.fullHP;
	}

	public bool GetFlipped()
	{
		return movement.flipped;
	}

	public virtual void Reset()
	{
		health.RestoreHP();
		rigidbody2D.velocity = new Vector2(0, 0);
        gameManager = GameManager.instance;
		rigidbody2D.gravityScale = 1;
	}

	public void SetAnimatorSpeed(float speed)
	{
		if (animator != null) 
		{
			animator.SetFloat ("Speed", speed);
		}
	}
	public void SetAnimatorYVelocity(float velocity)
	{
		if (animator != null) 
		{
			animator.SetFloat ("YVelocity", velocity);
		}
	}
	public void SetAnimatorAim(float aim)
	{
		if (animator != null) 
		{
			animator.SetFloat ("Aim", aim);
		}
	}
	public void SetAnimatorGrounded(bool grounded)
	{
		if (animator != null) 
		{
			animator.SetBool("Grounded", grounded);
		}
	}
    #endregion
}
