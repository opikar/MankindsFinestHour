using UnityEngine;
using System.Collections;

public class CameraSpawnerScript : MonoBehaviour {

	public Transform leftWall, rightWall;

	// Use this for initialization
	void Start () {
		GameManager.resolutionChanged += UpdateScale;
	}

	public void UpdateScale() {
		float magicNumber = 3;
		Vector3 scale = transform.localScale;
		scale.x = rightWall.position.x - leftWall.position.x+magicNumber;
		transform.localScale = scale;

		Vector3 pos = transform.position;
		pos.x = (rightWall.position.x + leftWall.position.x)/2;
		transform.position = pos;
	}
}
