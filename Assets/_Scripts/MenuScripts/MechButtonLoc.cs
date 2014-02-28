using UnityEngine;
using System.Collections;

public class MechButtonLoc : MonoBehaviour {

	public int widthPercent;
	private float widthPos;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		widthPos = (Screen.width/100) * widthPercent;
		GUITexture tex = GetComponent<GUITexture>();
		tex.pixelInset = new Rect(((Screen.width/7)*3)+(-widthPos),(-Screen.height/2)+((widthPos*(3f/10f))/2),widthPos,(widthPos*(3f/10f)));
	}
}
