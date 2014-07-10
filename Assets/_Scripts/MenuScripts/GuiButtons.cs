using UnityEngine;
using System.Collections;
using System;



public class GuiButtons : MonoBehaviour {

    public ArrowKeyCoordinator arrows;

    public delegate void PressedButton();
    public event PressedButton pressedButton;

    public bool restart;
    public bool loadLevel = true;
    public Levels levelToLoad;

    public bool selected;
    private bool ignoreMouse;

    private Vector3 previousMousePosition;

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
        if (selected)
            guiText.color = on;
        //Return color to normal
        else
        {
            guiText.color = off;
        }

        if (levelToLoad.ToString().Substring(0, 4) == "True")
            if (!SaveScript.save.availableLevels[levelToLoad.ToString()])
            {
                //guiText.color = disabled;
                guiText.enabled = false;                
            }
            else
            {
                guiText.enabled = true;
            }
        if (ignoreMouse)
        {
            if (Input.GetAxisRaw("Mouse X") != 0 || Input.GetAxisRaw("Mouse Y") != 0 || Input.GetMouseButton(0))
                ignoreMouse = false;

            if (selected)
                guiText.color = on;
            else
                guiText.color = off;

            return;
        }
        if (guiText.HitTest(Input.mousePosition))
        {
            arrows.ChangeSelectedByMouse(this);
            selected = true;
            guiText.color = on;
           

            if (Input.GetMouseButtonUp(0))
            {
                PressButton();
            }
        }       
    }

    private void OnEnable()
    {
        arrows.changeSelected += ChangeSelected;
    }
    private void OnDisable()
    {
        arrows.changeSelected -= ChangeSelected;
    }
    private void ChangeSelected()
    {
        ignoreMouse = true;
        if (selected)
            selected = false;
    }

    public void PressButton()
    {
        if (pressedButton != null) pressedButton();
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
