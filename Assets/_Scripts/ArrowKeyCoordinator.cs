using UnityEngine;
using System.Collections;

public class ArrowKeyCoordinator : MonoBehaviour {

    public GUIText[] guiTexts;
    private GuiButtons[] guiButtons;

    public delegate void ChangeSelected();
    public event ChangeSelected changeSelected;

    private int selection = 0;
    private int textCount;


	// Use this for initialization
	void Start () {
        textCount = guiTexts.Length;
        guiButtons = new GuiButtons[textCount];
        for(int i = 0; i < textCount; i++)
        {
            guiButtons[i] = guiTexts[i].gameObject.GetComponent<GuiButtons>();
            if (guiButtons[i] == null)
                print("problem in coordinator");
        }
        if(changeSelected != null)
            changeSelected();
        guiButtons[selection].selected = true;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            ArrowUp();
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            ArrowDown();
        }
        if (Input.GetKeyDown(KeyCode.Return))
            guiButtons[selection].PressButton();

	}

    private void ArrowUp()
    {
        do
        {
            selection--;
            if (selection < 0)
                selection = textCount - 1;
        } while (!guiButtons[selection].guiText.enabled);
        if (changeSelected != null)
            changeSelected();
        guiButtons[selection].selected = true;
    }
    private void ArrowDown()
    {
        do{
            selection++;
            if (selection >= textCount)
                selection = 0;
        } while (!guiButtons[selection].guiText.enabled);
        if (changeSelected != null)
            changeSelected(); 
        guiButtons[selection].selected = true;
    }

    public void ChangeSelectedByMouse(GuiButtons gb)
    {
        for (int i = 0; i < textCount; i++)
        {
            if (gb == guiButtons[i])
                selection = i;        
        }
        if (changeSelected != null)
            changeSelected();
    }
}
