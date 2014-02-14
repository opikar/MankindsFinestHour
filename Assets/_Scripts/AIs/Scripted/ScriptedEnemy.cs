using UnityEngine;
using System.Collections;

public delegate IEnumerator Action();

public abstract class ScriptedEnemy : MonoBehaviour {
	public ActionState[] states;
	bool actionRunning = false;
	protected ActionState currentState;

	virtual protected void Awake() {
		currentState = states[0];
	}

	// Update is called once per frame
	void Update () {
		if(!actionRunning) {
			StartCoroutine(RunAction(currentState.GetAction()));
		}
	}

	IEnumerator RunAction(Action action) {
		actionRunning = true;
		yield return StartCoroutine (action());
		actionRunning = false;
	}

	protected void ChangeState(string state) {
		for(int i = 0; i < states.Length; i++) {
			if(states[i].stateName == state) {
				currentState = states[i];
				break;
			}
		}
	}
}