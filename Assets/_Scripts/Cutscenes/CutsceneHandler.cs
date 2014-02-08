using UnityEngine;
using System.Collections;

public class CutsceneHandler : MonoBehaviour 
{
	// How would this be done so they would use the same variable for size? 
	// I tried (string[size]) where "size" would be int, 
	// but for some non-apparent reason it didn't work. Wrong syntax? - Panu
	
	public GameObject[] texture;
	public string[] story;
	int index = 0;
	
	// Use this for initialization
	void Start () 
	{
        if (texture.Length != story.Length)
        {
            Debug.Log("Not same amount of texture and story");
        }
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
