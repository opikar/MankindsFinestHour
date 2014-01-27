using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {
	public float CameraSpeed = 1.2f;
	public GameObject[] waypoints;

	int nextWaypoint = 0;


	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		if (waypoints.Length > nextWaypoint && transform.position.x < waypoints[nextWaypoint].transform.position.x) 
		{
			transform.position = Vector3.MoveTowards(transform.position, waypoints[nextWaypoint].transform.position, CameraSpeed * Time.deltaTime);
		}
	}

	public void MoveToNext() {
		nextWaypoint++;
	}
}
