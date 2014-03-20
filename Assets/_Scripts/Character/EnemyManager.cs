using UnityEngine;
using System.Collections;

public class EnemyManager : Character {

    #region MEMBERS
    #endregion

    #region UNITY_METHODS
    public void Start()
    {
    }

    public override void Die()
    {
        gameObject.SetActive(false);
    }

	public void Move(float direction){
		movement.Move(direction);
	}
    #endregion
}
