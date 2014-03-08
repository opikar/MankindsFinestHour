using UnityEngine;
using System.Collections;

public class GUIManager : MonoBehaviour
{
    public delegate void DrawGUI();
    public static event DrawGUI OnDraw = delegate { };

    void OnGUI()
    {
        if (GameManager.instance.GetState() == State.Running)
        {
            OnDraw();
        }
    }
}
