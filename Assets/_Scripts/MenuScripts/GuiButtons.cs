using UnityEngine;
using System.Collections;
using System;



public class GuiButtons : MonoBehaviour {

    public delegate void PressedButton();
    public event PressedButton pressedButton;

    public bool restart;
    public bool loadLevel = true;
    public Levels levelToLoad;
    

    public Color on = new Color(0.7f,0.7f,0.7f,1f);
	public Color off = new Color(0.5f,0.5f,0.5f,1f);
	public Color press = new Color(0.3f,0.3f,0.3f,1f);
	public Color disabled = new Color(0.1f, 0.1f, 0.1f);

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        if (guiTexture != null && guiTexture.enabled)
            UpdateGUITexture();
        else if (guiText != null && guiText.enabled)
            UpdateGUIText();
    }

    private void UpdateGUITexture()
    {
        //activate if mouse over a button
        if (levelToLoad.ToString().Substring(0, 4) == "True")
            if (!SaveScript.save.availableLevels[levelToLoad.ToString()])
            {
                guiTexture.color = disabled;
                return;
            }
        if (guiTexture.HitTest(Input.mousePosition))
        {

            guiTexture.color = on;

            //if clicked
            if (Input.GetMouseButton(0))
            {
                guiTexture.color = press;
            }

            if (Input.GetMouseButtonUp(0))
            {
                if (pressedButton != null) pressedButton();
                Time.timeScale = 1f;
                if (loadLevel)
                    Application.LoadLevel(levelToLoad.ToString());
                else
                    Application.Quit();
            }
        }
        //Return color to normal
        else
        {
            guiTexture.color = off;
        }
    }
    private void UpdateGUIText()
    {
        if (guiText.HitTest(Input.mousePosition))
        {

            guiText.color = on;

            //if clicked
            if (Input.GetMouseButton(0))
            {
                guiText.color = press;
            }

            if (Input.GetMouseButtonUp(0))
            {             
                Time.timeScale = 1f;
                if (restart)
                {
                    Application.LoadLevel(Application.loadedLevelName);
                }
                else if (loadLevel)
                {
                    Application.LoadLevel(levelToLoad.ToString());
                }
                else
                    Application.Quit();
            }
        }
        //Return color to normal
        else
        {
            guiText.color = off;
        }
    }
}

[Flags]
public enum Levels { 
    MainMenu,
    TrueLevel1,
    TrueLevel2,
    TrueLevel3,
    TrueLevel4,
    TrueLevel5,
    BossLevel1,
    BossLevel2,
    BossLevel3,
    BossLevel4,
    BossLevel5,
    MechList,
    IntroCutscene

}
