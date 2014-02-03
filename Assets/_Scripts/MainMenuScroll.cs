using UnityEngine;
using System.Collections;

public class MainMenuScroll : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(transform.position.x <= -18.8f)
		{
			transform.position = new Vector3(18.8f, 1, 0);
		}
		transform.position -= new Vector3(0.2f, 0, 0) * Time.deltaTime;
	}
}
