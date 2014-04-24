using UnityEngine;
using System.Collections;

public class PauseMenuScript : MonoBehaviour {

	#region MEMBERS
    private GameManager m_gameManager;
    #endregion

    GUITexture blackBox, paused, toMain;

	// Use this for initialization
	void Start () {
		m_gameManager = GameObject.Find("Manager").GetComponent<GameManager>();

		blackBox = transform.Find("BlackBox").GetComponent<GUITexture>();
		paused = transform.Find("Paused").GetComponent<GUITexture>();
		toMain = transform.Find("ToLevelSelect").GetComponent<GUITexture>();
	}
	
	// Update is called once per frame
	void Update () {
		if(m_gameManager.GetState() == State.PauseMenu){
			blackBox.enabled = true;
			paused.enabled = true;
			toMain.enabled = true;
		}
		else{
			blackBox.enabled = false;
			paused.enabled = false;
			toMain.enabled = false;
		}
	}
}
