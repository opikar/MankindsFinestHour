﻿using UnityEngine;
using System.Collections;

public class InputManager : MonoBehaviour
{
    #region MEMBERS
    private Movement m_movement;
	private PlayerManager m_playerManager;
    private GameManager m_gameManager;
    #endregion

    #region UNITY_METHODS
	void Awake () 
    {
        m_gameManager = GameObject.Find("Manager").GetComponent<GameManager>();
        if (m_gameManager != null)
        {
            m_gameManager.SetState(State.Running);
        }
		m_movement = GetComponent<Movement>();
        if (m_movement == null)
        {
            gameObject.AddComponent<Movement>();
        }
		m_playerManager = GetComponent<PlayerManager>();
	}

	void Update () 
    {
		if(m_gameManager.GetState() != State.Running) return;
		float axisVertical = Input.GetAxisRaw("Vertical");
		float axisHorizontal = Input.GetAxisRaw("Horizontal");
		m_playerManager.AimVertical(axisVertical, axisHorizontal);
		m_movement.Move(Input.GetAxis("Horizontal"));

		if(Input.GetButtonDown("Jump")) 
        {
			if(Input.GetAxisRaw("Vertical") < 0)
				m_movement.Drop();
			else
				m_movement.Jump();
		}
		if(Input.GetKey(KeyCode.Z))
			m_playerManager.ShootPrimaryWeapon();
    }
    #endregion
}
