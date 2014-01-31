using UnityEngine;
using System.Collections;

public class Epilepsy : MonoBehaviour {
	private Color bgc = new Color(0F,0F,0F,1F);
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		camera.backgroundColor = bgc;
		bgc.r += 0.15F;
		bgc.g += 0.2F;
		bgc.b += 0.1F;
		if(bgc.r > 1){
			bgc.r = 0;
		}
		if(bgc.g > 1){
			bgc.g = 0;
		}
		if(bgc.b > 1){
			bgc.b = 0;
		}
		Debug.Log(bgc.r + " " + bgc.g + " " + bgc.b);
	}
}
