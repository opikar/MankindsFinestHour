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

	}

    private void ArrowUp()
    {
        selection--;
        if (selection < 0)
            selection = textCount - 1;
        changeSelected();
        guiButtons[selection].selected = true;
    }
    private void ArrowDown()
    {
        selection++;
        if (selection >= textCount)
            selection = 0;        
        changeSelected();
        guiButtons[selection].selected = true;
    }
}
