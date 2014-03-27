using UnityEngine;
using System.Collections;

public class WallScript : MonoBehaviour {
	Transform leftWall, rightWall;

	void Awake() {
		leftWall = transform.Find("CameraWallLeft");
		rightWall = transform.Find("CameraWallRight");

		MoveWalls ();
	}

	// Update is called once per frame
	void Update () {
		MoveWalls();
	}

	void MoveWalls() {
		Vector3 right = Camera.main.ScreenToWorldPoint (new Vector3 (Screen.width, 0));
		Vector3 left = Camera.main.ScreenToWorldPoint (new Vector3(0, 0));
		
		right.y = left.y = transform.position.y;
		
		rightWall.position = right;
		leftWall.position = left;
	}
}
