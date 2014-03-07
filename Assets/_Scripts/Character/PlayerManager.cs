using UnityEngine;
using System.Collections;

public class PlayerManager : Character
{
    #region MEMBERS
    private GUIManager m_guiManager;
	private int lives = 3;
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

	public override void Die()
    {
		Debug.Log(lives);
		if (--lives >= 0) { 
			Application.LoadLevel(Application.loadedLevel);
		} else {
			gameObject.SetActive(false);
		}
    }
    #endregion
}
