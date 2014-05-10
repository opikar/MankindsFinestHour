﻿using UnityEngine;
using System.Collections;

public class InputManager : MonoBehaviour
{
    #region MEMBERS
    private Movement m_movement;
	private PlayerManager m_playerManager;
    #endregion

    #region UNITY_METHODS
	void Awake () 
    {
		m_movement = GetComponent<Movement>();
        if (m_movement == null)
        {
            gameObject.AddComponent<Movement>();
        }
		m_playerManager = GetComponent<PlayerManager>();
	}

	void Update () 
    {
		if(GameManager.instance.GetState() != State.Running || m_playerManager.GetPlayerState() != PlayerState.Normal) return;
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

		if(Input.GetButton ("Fire2"))
			m_playerManager.ShootSpecialWeapon();
		else if(Input.GetButtonDown("Fire1"))
			m_playerManager.ShootPrimaryWeapon();

		//Pause handling
		if(Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Escape)){
			m_playerManager.Pause();
		}
    }
    #endregion
}
