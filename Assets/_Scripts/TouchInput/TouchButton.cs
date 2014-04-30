using UnityEngine;
using System.Collections;

[RequireComponent(typeof(GUITexture))]
public class TouchButton : MonoBehaviour {

    Rect area;

	void Start () {
        area = guiTexture.pixelInset;
	}
	
	// Update is called once per frame
	void Update () {
        
	}

    public virtual bool Pressed() 
    {
        for (int i = 0; i < Input.touchCount; i++)
        {
            if (Input.touches[i].phase == TouchPhase.Began && area.Contains(Input.touches[i].position))
            {
                Debug.Log("Pressed: " + gameObject.name);
                return true;
            }
        }
        return false;
    }
}
