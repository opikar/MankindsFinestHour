using UnityEngine;
using System.Collections;

public class MechListLoc : MonoBehaviour {

	public int widthPercent, pos;
	private float widthPos;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		widthPos = (Screen.width/100) * widthPercent;
		GUITexture tex = GetComponent<GUITexture>();
		tex.pixelInset = new Rect((-widthPos/2)-(widthPos/2.5f),(-Screen.height/2)+(widthPos*(1f/4f))*pos,widthPos,(widthPos*(1f/4f)));
	}
}
