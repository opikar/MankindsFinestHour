using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {
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
			float speed = waypoints[nextWaypoint].GetComponent<WaypointScript>().SpeedToWaypoint;
			Vector3 movement = Vector3.MoveTowards(transform.position, waypoints[nextWaypoint].transform.position, speed * Time.deltaTime);
			movement.z = cameraZ;
			transform.position = movement;
		} else
			MoveToNext();
	}

	public void MoveToNext() {
		nextWaypoint++;
	}
}
