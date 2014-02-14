using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MovingPlatform : MonoBehaviour {

	public GameObject start;

	private int i_index;
	private Transform m_transform;
	private float f_range = 5f;
	private Vector3 v_moveDirection;
	private List<Transform> waypoint;

	void Start()
	{
		m_transform = transform;
		waypoint = GetPath();
	}

	void Update()
	{
		if ((m_transform.position - waypoint[i_index].position).sqrMagnitude > f_range)
		{
			v_moveDirection = waypoint [i_index].position - m_transform.position;
			v_moveDirection.Normalize();
		}
		else NextIndex();

		m_transform.Translate(v_moveDirection * Time.deltaTime * 20f);
	}

	void NextIndex()
	{
		if (++i_index >= waypoint.Count) i_index = 0;
	}

	List<Transform> GetPath()
	{
		string str = null;
		int index=0;
		str = start.tag;
		Transform[] t = GetComponentsInChildren<Transform>();
		List<GameObject> gos = new List<GameObject>();
		for(int i = 0; i < t.Length; i++)
		{
			if(t[i].tag == "PlatformWaypoint")
				gos.Add(t[i].gameObject);
			print (t[i].position);
		}

		List<Transform> waypoints = new List<Transform>();
		waypoints.Add (start.transform);
		start.GetComponent<PlatformWaypointScript>().SetUsed(true);
		while(true){
			Transform closest = FindClosest(waypoints[index],gos);
			if(closest != null)
				waypoints.Add (closest);
			if(++index >= gos.Count)break;
		}
		return waypoints;
	}

	Transform FindClosest(Transform start, List<GameObject> obj){
		Transform closest = null;
		float distance = Mathf.Infinity;
		foreach(GameObject go in obj){
			PlatformWaypointScript sc = go.GetComponent<PlatformWaypointScript>();
			if(start!=go.transform && !sc.GetUsed()){
				//if(!Physics.Linecast (start.position,go.transform.position,layerMask)){
				float dist = (start.position - go.transform.position).magnitude;
				if(dist < distance){
					distance = dist;
					closest = go.transform;
				}
				//}
			}
		}
		if(closest != null){
			closest.gameObject.GetComponent<PlatformWaypointScript>().SetUsed (true);
		}
		return closest;
	}




}
