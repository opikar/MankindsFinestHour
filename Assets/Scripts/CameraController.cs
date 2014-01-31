using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {
	public float CameraSpeed = 1.2f;
	public GameObject[] waypoints;

	[SerializeField]
	int nextWaypoint = -1;
	float cameraZ;

	// Use this for initialization
	void Start () {
		cameraZ = transform.position.z;
	}
	
	// Update is called once per frame
	void Update () {
		if(nextWaypoint >= waypoints.Length || nextWaypoint  < 0)
			return;
		if (transform.position.x < waypoints[nextWaypoint].transform.position.x) 
		{
			Vector3 movement = Vector3.MoveTowards(transform.position, waypoints[nextWaypoint].transform.position, CameraSpeed * Time.deltaTime);
			movement.z = cameraZ;
			transform.position = movement;
		}
	}

	public void MoveToNext() {
		nextWaypoint++;
	}
}
