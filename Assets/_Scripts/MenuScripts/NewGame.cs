using UnityEngine;
using System.Collections;

public class NewGame : MonoBehaviour {
    
    void OnEnable()
    {
        GetComponent<GuiButtons>().pressedButton += Reset;
    }
    void OnDisable()
    {
        GetComponent<GuiButtons>().pressedButton -= Reset;
    }

    private void Reset()
    {
        SaveScript.save.ResetAllLevels();
    }
}
