using UnityEngine;
using System.Collections;

public class PlatformMaterialScript : MonoBehaviour {

    private int multiplier = 40;

    private Material m;

	// Use this for initialization
	void Start () {
        m = renderer.material;
        float x, y;
        x = 1f / transform.localScale.x * multiplier;
        y = transform.localScale.y;
        m.SetTextureScale("_MainTex", new Vector2(x, y));
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
