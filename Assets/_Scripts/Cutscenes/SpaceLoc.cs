using UnityEngine;
using System.Collections;

public class SpaceLoc : MonoBehaviour {

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		GUIText tex = GetComponent<GUIText>();
		tex.pixelOffset = new Vector2(0,(-Screen.height/2)+(Screen.height/3)+(Screen.height/5));
	}
}
