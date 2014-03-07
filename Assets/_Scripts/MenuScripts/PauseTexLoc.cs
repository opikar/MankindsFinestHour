using UnityEngine;
using System.Collections;

public class PauseTexLoc : MonoBehaviour {

	public int widthPercent;
	private float widthPos;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		widthPos = (Screen.width/100) * widthPercent;
		GUITexture tex = GetComponent<GUITexture>();
		tex.pixelInset = new Rect((-widthPos/2),0,widthPos,(widthPos*(89f/453f)));
	}
}
