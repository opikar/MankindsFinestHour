using UnityEngine;
using System.Collections;

public class TouchButtonController : MonoBehaviour {

    public GUITexture up, down, left, right, shoot, jump;
    public float arrowSize = 9, abSize = 8;

    private Vector2 arrowCenter;

	// Use this for initialization
	void Awake () {
        arrowSize = Screen.width / arrowSize;
        abSize = Screen.width / abSize;
        SetABButtons();
        DisableArrows();
        Debug.Log(Screen.width);
	}

    public void SetButtons(Vector2 position)
    {
        arrowCenter = position;
        SetArrowButtons();
    }

    public void DisableArrows()
    {
        up.enabled = down.enabled = left.enabled = right.enabled = false;
    }

    private void SetArrowButtons()
    {
        up.enabled = down.enabled = left.enabled = right.enabled = true;
        Rect arrow = new Rect(arrowCenter.x, arrowCenter.y, arrowSize, arrowSize);

        arrow.x -= arrowSize;
        arrow.y -= arrowSize * 0.5f;

        left.pixelInset = arrow;

        arrow.x += arrowSize;
        //arrow.y += arrowSize * 0.5f;

        right.pixelInset = arrow;

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
