using UnityEngine;
using System.Collections;

public class MechListLoc : MonoBehaviour {

	public int widthPercent, pos;
	private float widthPos;

	// Use this for initialization
	void Start () {
		SaveScript.save.availableLevels["TrueLevel1"] = true;
		GUITexture tex = GetComponent<GUITexture>();
		tex.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		GUITexture tex = GetComponent<GUITexture>();
		if(SaveScript.save.availableLevels[gameObject.name]){
			tex.enabled = true;
			widthPos = (Screen.width/100) * widthPercent;
			tex.pixelInset = new Rect((-widthPos/2)-(widthPos/2.5f),-(widthPos*(1f/4f))*pos,widthPos,(widthPos*(1f/4f)));
		}
	}
}
