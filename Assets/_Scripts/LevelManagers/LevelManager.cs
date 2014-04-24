using UnityEngine;
using System.Collections;

public abstract class LevelManager : MonoBehaviour {
	public void Start() {
		GameManager.instance.SetState(State.Running);
	}

    public abstract void CompleteLevel();
}
