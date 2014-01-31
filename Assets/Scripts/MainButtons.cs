using UnityEngine;
using System.Collections;

public class MainButtons : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButtonUp(0) && guiTexture.HitTest(Input.mousePosition))
		{
			if(guiTexture.name == "New")
			{
				Application.LoadLevel("Level1");
				//Debug.Log("New");
			}
			else if(guiTexture.name == "Load")
			{
				Debug.Log("Load");
			}
			else if(guiTexture.name == "Options")
			{
				Debug.Log("Options");
			}
			else if(guiTexture.name == "Quit")
			{
				Debug.Log("Quit");
				Application.Quit();
			}
		}
	}
}
