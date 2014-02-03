using UnityEngine;
using System.Collections;

public class IntroCutscene : MonoBehaviour {

	public string[] texts = {
	"This'll be the IntroCutscene",
	"There will be a few things here for testing purposes",
	"This sentence is short",
	"Last sentece was shorter than any sentence in this array, while this one is the longest one and the only one containing a punctuation in the end.",
	"After this string the level should load up"
	};
	public int currentString = 0;
	private GUIText cutsceneText;


	// Use this for initialization
	void Start () 
	{
		cutsceneText = GetComponent<GUIText>();
		cutsceneText.text = texts[currentString];
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown("Space")) {
			moveToNextString();
			if (currentString > texts.Length) {
				Application.LoadLevel("Level1");
			}
			else {
				cutsceneText.text = texts[currentString];
			}
		}
	}

	public void moveToNextString() {
		currentString++;
	}

}
