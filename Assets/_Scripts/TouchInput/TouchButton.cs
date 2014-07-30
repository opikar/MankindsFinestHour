using UnityEngine;
using System.Collections;

[RequireComponent(typeof(GUITexture))]
public class TouchButton : MonoBehaviour {

    

	void Start () {        
	}
	
	// Update is called once per frame
	void Update () {
        
	}

    public virtual bool Pressed() 
    {
        for (int i = 0; i < Input.touchCount; i++)
        {
            if (Input.touches[i].phase == TouchPhase.Began && guiTexture.pixelInset.Contains(Input.touches[i].position))
            {
                Debug.Log("Pressed: " + gameObject.name);
                return true;
            }
        }
        return false;
    }

    public virtual bool Pressed(int id)
    {
        if (Input.GetTouch(id).phase == TouchPhase.Began && guiTexture.pixelInset.Contains(Input.GetTouch(id).position))
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
            if ((Input.touches[i].phase == TouchPhase.Began || Input.touches[i].phase == TouchPhase.Stationary || Input.touches[i].phase == TouchPhase.Moved) && guiTexture.pixelInset.Contains(Input.touches[i].position))
            {
                Debug.Log("Pressed: " + gameObject.name);
                return true;
            }
        }
        return false;
    }
    public virtual bool Pressing(int id)
    {
        Debug.Log("Pressing");
        if ((Input.GetTouch(id).phase == TouchPhase.Began || Input.GetTouch(id).phase == TouchPhase.Stationary || Input.GetTouch(id).phase == TouchPhase.Moved) && guiTexture.pixelInset.Contains(Input.GetTouch(id).position))
            {
                Debug.Log("Pressed: " + gameObject.name);
                return true;
            }
        return false;
    }
}
