using UnityEngine;
using System.Collections;

public class PlatformMaterialScript : MonoBehaviour {

    private Material m;

	// Use this for initialization
	void Start () {
        m = renderer.material;
        float x, y;
        x = transform.localScale.x;
        y = transform.localScale.y;
        m.SetTextureScale("_MainTex", new Vector2(x, y));
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
