using UnityEngine;
using System.Collections;

public class InputManager : MonoBehaviour {
	public PlayerManager playerManager;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		playerManager.movement.Move(Input.GetAxis("Horizontal"));

		if(Input.GetButtonDown("Jump"))
			playerManager.movement.Jump();

		if(Input.GetButtonDown("Jump") && Input.GetAxis("Vertical") > 0)
			playerManager.movement.Drop();

	}
}
