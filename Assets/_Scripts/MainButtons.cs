using UnityEngine;
using System.Collections;

public class MainButtons : MonoBehaviour 
{
	//Setting up the shading of MainMenuButtons
	//on = when mouse over item
	//off = idle shading
	//press = when item is clicked

	public Color on = new Color(0.7f,0.7f,0.7f,1f);
	public Color off = new Color(0.5f,0.5f,0.5f,1f);
	public Color press = new Color(0.3f,0.3f,0.3f,1f);

	void Update () 
    {

    	//activate if mouse over a button
		if(guiTexture.HitTest(Input.mousePosition))
		{
			guiTexture.color = on;
			
			//if clicked
			if(Input.GetMouseButton(0))
            {
				guiTexture.color = press;
			}

			//if released on a button
			if(Input.GetMouseButtonUp(0))
			{
				if(guiTexture.name == "New")
				{

					//Load the first cutscene or "New Game"
					Application.LoadLevel("IntroCutscene");
				}
				else if(guiTexture.name == "Load")
				{
					//Go into "Load Game" -Scene
					Debug.Log("Load");
				}
				else if(guiTexture.name == "Options")
				{
					//Open options menu
					Debug.Log("Options");
				}
				else if(guiTexture.name == "Quit")
				{
					//Shut down the game
					Debug.Log("Quit");
					Application.Quit();
				}
			}
		}
		//Return color to normal
		else
		{
			guiTexture.color = off;
		}
	}
}
