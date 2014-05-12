using UnityEngine;
using System.Collections;

public abstract class LevelManager : MonoBehaviour {

    public State startState;
    public float waitingTime;

	public virtual IEnumerator Start() {
        GameManager.instance.SetState(startState);
        yield return new WaitForSeconds(waitingTime);
		GameManager.instance.SetState(State.Running);
	}


    public abstract void CompleteLevel();
}
