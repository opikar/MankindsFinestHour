using UnityEngine;
using System.Collections;

public class PlayerManager : Character
{
    #region MEMBERS
    private GUIManager m_guiManager;
    #endregion

    #region UNITY_METHODS
    override public void Start () 
    {
		base.Start();
        m_guiManager = GetComponent<GUIManager>();
        if (m_guiManager == null)
        {
            m_guiManager = gameObject.AddComponent<GUIManager>();
        }
        if (m_guiManager == null) 
        {
            Debug.LogError("Component did not load properly in PlayerManager");
        }
	}
    #endregion

    #region METHODS
    public void AimVertical (float axis)
	{
		weapon.MoveShootingTarget(axis);
	}

	public override void Die()
    {
		gameManager.SetState(State.Loss);
    }
    #endregion
}
