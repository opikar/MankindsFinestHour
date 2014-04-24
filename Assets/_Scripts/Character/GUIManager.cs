using UnityEngine;
using System.Collections;

public class GUIManager : MonoBehaviour
{
    public delegate void DrawGUI();
    public static event DrawGUI OnDraw = delegate { };

	public static readonly Vector2 screenScale = new Vector2(1920, 1080);

    void OnGUI()
    {
        if (GameManager.instance.GetState() == State.Running)
        {
			Vector3 scale;
			scale.x = Screen.width/screenScale.x;
			scale.y = Screen.height/screenScale.y;
			scale.z = 1;

			Matrix4x4 matrix = GUI.matrix;
			GUI.matrix = Matrix4x4.TRS(Vector3.zero, Quaternion.identity, scale);
            OnDraw();
			GUI.matrix = matrix;
        }
    }
}
