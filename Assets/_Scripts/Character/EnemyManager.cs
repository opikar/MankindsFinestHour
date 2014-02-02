using UnityEngine;
using System.Collections;

public class EnemyManager : Character {

    #region MEMBERS
    #endregion

    #region UNITY_METHODS
    override public void Start()
    {
        base.Start();
    }

    public override void Die()
    {
        Destroy(gameObject);
    }
    #endregion
}
