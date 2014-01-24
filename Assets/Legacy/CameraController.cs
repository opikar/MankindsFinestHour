using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {
	public float CameraSpeed = 1.2f;



	// Use this for initialization
	void Start () {


	
	}
	
	// Update is called once per frame
	void Update () {
	

		if (transform.position.x < 67.91809f) 
		{
			transform.position += new Vector3 (CameraSpeed * Time.deltaTime, 0, 0);
		}
	}
}
