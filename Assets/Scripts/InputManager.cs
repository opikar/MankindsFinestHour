using UnityEngine;
using System.Collections;

public class InputManager : MonoBehaviour {
	public Movement movement;
	PlayerManager playerManager;

	// Use this for initialization
	void Start () {
		movement = GetComponent<Movement>();
		playerManager = GetComponent<PlayerManager>();
	}
	
	// Update is called once per frame
	void Update () {
		float axis = Input.GetAxisRaw("Vertical");
		playerManager.AimVertical(axis);
		movement.Move(Input.GetAxis("Horizontal"));

		if(Input.GetButtonDown("Jump")) {
			if(Input.GetAxisRaw("Vertical") < 0)
				movement.Drop();
			else
				movement.Jump();
		}
	}
}
