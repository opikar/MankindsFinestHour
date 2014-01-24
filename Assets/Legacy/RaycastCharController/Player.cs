using UnityEngine;
using System.Collections;

[RequireComponent (typeof(CharController))]
public class Player : MonoBehaviour {
	CharController controller;

	// Use this for initialization
	void Start () {
		controller = GetComponent<CharController>();
	}
	
	// Update is called once per frame
	void Update () {
		controller.Move(Input.GetAxis ("Horizontal"));

		if(Input.GetButton ("Jump")) {
			if(Input.GetKey ("down"))
				controller.Drop();
			else
				controller.Jump ();
		}

	}
}
