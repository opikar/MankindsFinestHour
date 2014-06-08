using UnityEngine;
using System.Collections;

public class InputManagerMobile : MonoBehaviour
{
    #region MEMBERS
    private Movement m_movement;
	private PlayerManager m_playerManager;
    private int i_up, i_down, i_left, i_right;
    private float axisVertical;
    private float axisHorizontal;
    private TouchButton shoot, jump, up, down ,left, right;
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
        up = GameObject.Find("ArrowUp").GetComponent<TouchButton>();
        down = GameObject.Find("ArrowDown").GetComponent<TouchButton>();
        left = GameObject.Find("ArrowLeft").GetComponent<TouchButton>();
        right = GameObject.Find("ArrowRight").GetComponent<TouchButton>();
	}

	void Update () 
    {
        //Pause handling
        if(Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Escape)){
            m_playerManager.Pause();
        }

		if(GameManager.instance.GetState() != State.Running || m_playerManager.GetPlayerState() != PlayerState.Normal) return;


        if (up.Pressing())
            i_up = 1;
        else
            i_up = 0;

        if (down.Pressing())
            i_down = -1;
        else
            i_down = 0;

        if (right.Pressing())
            i_right = 1;
        else
            i_right = 0;

        if (left.Pressing())
            i_left = -1;
        else
            i_left = 0;


        axisHorizontal = i_left + i_right;
        axisVertical = i_up + i_down;

		m_playerManager.AimVertical(axisVertical, axisHorizontal);
		
        m_movement.Move(axisHorizontal);

		if(jump.Pressed()) 
        {
			if(axisVertical < 0)
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
