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

    private bool showArrows;
    private int touchID;
    private TouchButtonController controller;
    #endregion

    #region UNITY_METHODS
	void Awake () 
    {
		m_movement = GetComponent<Movement>();
        if (m_movement == null)
        {
            gameObject.AddComponent<Movement>();
        }
        controller = GetComponent<TouchButtonController>();
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

        for (int i = 0; i < Input.touchCount; i++)
        {
            if (shoot.Pressed(i) || jump.Pressed(i)) continue;
            if (Input.touches[i].phase == TouchPhase.Ended && showArrows)
            {
                showArrows = false;
            }
            if (Input.touches[i].phase == TouchPhase.Began && !showArrows)
            {
                showArrows = true;
                touchID = Input.touches[i].fingerId;
                controller.SetButtons(Input.touches[i].position);
            }
        }

        if (showArrows)
        {
            i_up = up.Pressing() ? 1 : 0;
            i_down = down.Pressing() ? -1 : 0;
            i_right = right.Pressing() ? 1 : 0;
            i_left = left.Pressing() ? -1 : 0;
        }
        else
            i_up = i_right = i_left = i_down = 0;

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

		if(shoot.Pressed())
			m_playerManager.ShootPrimaryWeapon();
    }
    #endregion
}
