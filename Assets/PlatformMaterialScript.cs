using UnityEngine;
using System.Collections;

public class PlatformMaterialScript : MonoBehaviour {

    private Material m;
    /*

	What is levelName?
	What is a color bound to levelName?
	Set the color to be LevelColor

	private Color LevelColor = new Color(0F,0F,0F,1F);

	After saying "Color" too many times, "Color" loses it's meaning.
	Color makes you start to question yourself about Color.
	Does "color" look weird to you now?

    */

	// Use this for initialization
	void Start () {
        m = renderer.material;
        float x, y;
        x = transform.localScale.x;
        y = transform.localScale.y;
        m.SetTextureScale("_MainTex", new Vector2(x, y));
        //m.color = LevelColor;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
