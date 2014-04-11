using UnityEngine;
using System.Collections;

public class ButtonLoc : MonoBehaviour {

	public int widthPercent;
	public float pos;
	private float widthPos;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		widthPos = (Screen.width/100) * widthPercent;
		GUITexture tex = GetComponent<GUITexture>();
		tex.pixelInset = new Rect((-widthPos/2),(-Screen.height/2)+(widthPos*(3f/10f))*pos,widthPos,(widthPos*(3f/10f)));
	}
}
