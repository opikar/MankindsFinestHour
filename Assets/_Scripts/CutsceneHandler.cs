using UnityEngine;
using System.Collections;

public class CutsceneHandler : MonoBehaviour {
	public int cutsceneIndex = 0;
	public GameObject[] texture;
	int index = 0;
	
	// Use this for initialization
	void Start () 
	{
		texture[0].SetActive(false);
		texture[1].SetActive(false);
		switch(cutsceneIndex)
		{
			case 0:
				GetComponent<IntroCutscene>();
				break;
			case 1:
				//something
				break;
			default:
				Debug.Log("Something went wrong while choosing a cutscene");
				break;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Space))
		{
			if(!texture[index].activeSelf)
				texture[index].SetActive(true);
			else
			{
				texture[index].SetActive(false);
				index++;
			}
		}
	}
}
