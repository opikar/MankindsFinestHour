using UnityEngine;
using System.Collections;

public class TextBGscript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		GUITexture tex = GetComponent<GUITexture>();
		tex.pixelInset = new Rect((-Screen.width/2)-1,(-Screen.height/2)+(Screen.height/3),Screen.width,Screen.height/4);
	}
	
	// Update is called once per frame
	void Update () {
		GUITexture tex = GetComponent<GUITexture>();
		tex.pixelInset = new Rect((-Screen.width/2)-1,(-Screen.height/2)+(Screen.height/3),Screen.width,Screen.height/4);
	}
}
