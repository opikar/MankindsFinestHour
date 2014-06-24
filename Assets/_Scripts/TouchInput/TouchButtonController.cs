using UnityEngine;
using System.Collections;

public class TouchButtonController : MonoBehaviour {

    public GUITexture up, down, left, right, shoot, jump;
    public float arrowSize = 9, abSize = 8;

	// Use this for initialization
	void Awake () {
        arrowSize = Screen.width / arrowSize;
        abSize = Screen.width / abSize;
        SetABButtons();
        SetArrowButtons();
        Debug.Log(Screen.width);
	}

    private void SetArrowButtons()
    {
        Rect arrow = new Rect(0, 0, arrowSize, arrowSize);

        arrow.x = 20;
        arrow.y = (Screen.height - arrowSize) * 0.5f;

        left.pixelInset = arrow;

        arrow.x += arrowSize;
        //arrow.y += arrowSize * 0.5f;

        right.pixelInset = arrow;

        arrow.width = arrowSize;
        arrow.height = arrowSize;

        arrow.x -= arrowSize * 0.5f;
        arrow.y += arrowSize * 0.5f;

        up.pixelInset = arrow;

        arrow.y -= arrowSize;

        down.pixelInset = arrow;

        
    }

    private void SetABButtons()
    {
        Rect button = new Rect(0, 0, abSize, abSize);
        button.x = Screen.width - abSize;
        button.y = Screen.height - abSize;

        shoot.pixelInset = button;

        button.y -= abSize;

        jump.pixelInset = button;
    }
}
