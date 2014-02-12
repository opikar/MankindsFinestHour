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
    #endregion

    #region UNITY_METHODS
    public virtual void Start () 
    {
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

	public void ShootPrimaryWeapon()
    {
		weapon.ShootPrimaryWeapon();
	}

	public void SetTarget(Transform shootSpawn)
	{
		weapon.SetTarget(shootSpawn);
	}

	public bool GetFacingRight(){
		return movement.facingRight;
	}
    #endregion
}
