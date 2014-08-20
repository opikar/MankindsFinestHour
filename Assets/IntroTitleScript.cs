using UnityEngine;
using System.Collections;

public class IntroTitleScript : MonoBehaviour {

	public GameObject introText;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if (renderer.bounds.min.y > 0) {
			return;
		}

		transform.position = new Vector3(0, introText.renderer.bounds.min.y, 0);
	}
}
