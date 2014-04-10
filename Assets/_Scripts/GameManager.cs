using UnityEngine;
using System.Collections;
using System;

public class GameManager : MonoBehaviour
{
	//Constant for scaling ui-elements
	public static readonly Vector2 screenScale = new Vector2(1920, 1080);
    public delegate void ResolutionChanged();
    public static event ResolutionChanged resolutionChanged = delegate { };
    private int screenWidth;
    private int screenHeight;

	protected State e_state;
	
	public static GameManager instance {
		get;
		private set;
	}
	
	void Awake()
	{
		if (instance != null) 
			Destroy (instance);

		instance = this;

        screenWidth = Screen.width;
        screenHeight = Screen.height;

        SetState(State.StartMenu);
	}

    void Update()
    {
        if (Screen.width != screenWidth || Screen.height != screenHeight)
        {
            screenWidth = Screen.width;
            screenHeight = Screen.height;
			resolutionChanged();
        }
    }
	
	/// <summary>
	/// Sets the current state of the GameManager.
	/// </summary>
	/// <param name="state">State.</param>
	public void SetState(State state)
	{	
		e_state = state;
	}

	/// <summary>
	/// Adds the state to the current state of the GameManager.
	/// </summary>
	/// <param name="state">State.</param>
	public void AddState(State state)
	{
		e_state |= state;
	}

	/// <summary>
	/// Gets the current state.
	/// </summary>
	/// <returns>The state.</returns>
	public State GetState()
	{
		return e_state;
	}

	/// <summary>
	/// Removes the given state from the current state.If the state is not contain, nothing happens.
	/// </summary>
	/// <param name="state">State.</param>
	public void RemoveState(State state)
	{
		e_state &= ~state;
	}

	/// <summary>
	/// Checks if the state is contained in the current state.
	/// </summary>
	/// <returns><c>true</c>, if the state is contained, <c>false</c> otherwise.</returns>
	/// <param name="state">State.</param>
	public bool CheckForState(State state)
	{
		if ((e_state&state)==state) {
			return true;		
		}
		return false;
	}	
}

[Flags]
public enum State{
	StartMenu=1,
	Pregame=2,
	Running=4,
	Postgame=8,
	Win=16,
	Loss=32,
	PauseMenu=64,
    Lobby = 128,
}