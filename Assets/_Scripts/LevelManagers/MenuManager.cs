using UnityEngine;
using System.Collections;

public class MenuManager : MonoBehaviour {

    public State startState;

	// Use this for initialization
	void Start () {
        GameManager.instance.SetState(startState);
	}
	
}
