using UnityEngine;
using System.Collections;

public class MainButtons : MonoBehaviour 
{
	public Color on = new Color(0.7f,0.7f,0.7f,1f);
	public Color off = new Color(0.5f,0.5f,0.5f,1f);
	public Color press = new Color(0.3f,0.3f,0.3f,1f);

	void Update () 
    {
		if(guiTexture.HitTest(Input.mousePosition))
		{
			guiTexture.color = on;
			if(Input.GetMouseButton(0))
            {
				guiTexture.color = press;
			}
			if(Input.GetMouseButtonUp(0))
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
		else
		{
			guiTexture.color = off;
		}
	}
}
