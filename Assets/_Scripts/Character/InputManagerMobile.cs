using UnityEngine;
using System.Collections;

public class InputManagerMobile : MonoBehaviour
{
    #region MEMBERS
    private Movement m_movement;
	private PlayerManager m_playerManager;
    public Joystick joystick;
    public TouchButton shoot, jump;
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
        jump = GameObject.Find("JumpButton").GetComponent<TouchButton>();
        shoot = GameObject.Find("ShootButton").GetComponent<TouchButton>();
        joystick = GameObject.Find("Single Joystick").GetComponent<Joystick>();
	}

	void Update () 
    {
        //Pause handling
        if(Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Escape)){
            m_playerManager.Pause();
        }

		if(GameManager.instance.GetState() != State.Running || m_playerManager.GetPlayerState() != PlayerState.Normal) return;
        float axisVertical = joystick.position.y;
        float axisHorizontal = joystick.position.x;
		m_playerManager.AimVertical(axisVertical, axisHorizontal);
		
        m_movement.Move(joystick.position.x);

		if(jump.Pressed()) 
        {
			if(joystick.position.y < -0.5f)
				m_movement.Drop();
			else
				m_movement.Jump();
		}

		//if(Input.GetButton ("Fire2"))
			//m_playerManager.ShootSpecialWeapon();
		if(shoot.Pressed())
			m_playerManager.ShootPrimaryWeapon();
    }
    #endregion
}
