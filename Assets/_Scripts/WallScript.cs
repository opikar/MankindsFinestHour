using UnityEngine;
using System.Collections;

public class WallScript : MonoBehaviour {
	Transform leftWall, rightWall;

	// Use this for initialization
	void Start () {
		leftWall = GameObject.Find("CameraWallLeft").transform;
		rightWall = GameObject.Find("CameraWallRight").transform;
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 right = Camera.main.ScreenToWorldPoint (new Vector3 (Screen.width, 0));
		Vector3 left = Camera.main.ScreenToWorldPoint (new Vector3(0, 0));

		right.y = left.y = transform.position.y;

		rightWall.position = right;
		leftWall.position = left;
	}
}
