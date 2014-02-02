using UnityEngine;
using System.Collections;

public class Epilepsy : MonoBehaviour 
{
	private Color c_bgc = new Color(0F,0F,0F,1F);

	void Update () 
    {
		camera.backgroundColor = c_bgc;
		c_bgc.r += 0.15F;
		c_bgc.g += 0.2F;
		c_bgc.b += 0.1F;
		if(c_bgc.r > 1)
        {
			c_bgc.r = 0;
		}
		if(c_bgc.g > 1)
        {
			c_bgc.g = 0;
		}
		if(c_bgc.b > 1)
        {
			c_bgc.b = 0;
		}
		Debug.Log(c_bgc.r + " " + c_bgc.g + " " + c_bgc.b);
	}
}
