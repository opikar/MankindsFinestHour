using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class ActionState {
	public string stateName;
	//For path-finding if we have time
	public Transform position;
	List<Action> actions = new List<Action>();
	
	public void AddAction(Action action) {
		actions.Add(action);
	}

	public Action GetAction() {
		if(actions.Count == 0)
			return null;
		else
			return actions[Random.Range(0, actions.Count)];
	}
}
