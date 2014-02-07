﻿using UnityEngine;
using System.Collections;

public class CutsceneHandler : MonoBehaviour {
	public GameObject[] texture;
	public string[] story;
	int index = 0;
	
	// Use this for initialization
	void Start () 
	{
		texture[0].SetActive(true);
		guiText.text = story[0];
		for (int i = 1; i < texture.Length; i++)
		{
			texture[i].SetActive(false);
		}

	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Space))
		{
			if(index + 1 == texture.Length) 
			{
				Application.LoadLevel("Level1");
			}
			else if(texture[index].activeSelf && !texture[index+1].activeSelf)
			{
				texture[index].SetActive(false);
				index++;
				Debug.Log(index + " " + texture.Length);
				texture[index].SetActive(true);
				guiText.text = story[index];
			}
		}
	}
}
