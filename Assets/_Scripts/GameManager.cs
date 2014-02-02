using UnityEngine;
using System.Collections;
using System;

public class GameManager : MonoBehaviour
{

	protected State e_state;
	
	private static GameManager instance=null;
	
	void Awake()
	{
		if (instance == null) 
		{
			instance = this;
		} 
		else
			Destroy (this);

        SetState(State.StartMenu);
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
    Lobby = 128
}