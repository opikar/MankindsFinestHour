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

    public virtual bool Pressed(int id)
    {
        if (Input.GetTouch(id).phase == TouchPhase.Began && area.Contains(Input.GetTouch(id).position))
        {
            Debug.Log("Pressed: " + gameObject.name);
            return true;
        }
        return false;
    }

    public virtual bool Pressing()
    {
        for (int i = 0; i < Input.touchCount; i++)
        {
            if ((Input.touches[i].phase == TouchPhase.Began || Input.touches[i].phase == TouchPhase.Stationary || Input.touches[i].phase == TouchPhase.Moved) && area.Contains(Input.touches[i].position))
            {
                Debug.Log("Pressed: " + gameObject.name);
                return true;
            }
        }
        return false;
    }
}
