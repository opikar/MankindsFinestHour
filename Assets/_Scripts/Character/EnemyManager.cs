using UnityEngine;
using System.Collections;

public class EnemyManager : Character {

    #region MEMBERS
    #endregion

    #region UNITY_METHODS
    protected virtual void Start()
    {
    }

    public override void Die()
    {
        gameObject.SetActive(false);
    }

    public virtual void Stomped()
    {
		health.ApplyDamage(100);
    }

	public void Move(float direction){
		movement.Move(direction);
	}

	void OnBecameInvisible() {
		Die();
	}

	public override void Reset()
	{
		base.Reset();
		health.RestoreHP();
	}
    #endregion
}
