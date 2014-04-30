using UnityEngine;
using System.Collections;

public class TouchButton : MonoBehaviour {

    Rect area;

	// Use this for initialization
	void Start () {
        area = guiTexture.pixelInset;
	}
	
	// Update is called once per frame
	void Update () {
        for (int i = 0; i < Input.touchCount; i++)
        { 
            if(Input.touches[i].phase == TouchPhase.Began && area.Contains(Input.touches[i].position))
                Debug.Log("touched button");
        }
	}
}
