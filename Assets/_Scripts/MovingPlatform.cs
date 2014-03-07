using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MovingPlatform : MonoBehaviour {

	public GameObject start;
	public float speed = 10f;
	public bool moveAlways;

	private int i_index;
	private Transform m_transform;
	private float f_range = 5f;
	private Vector3 v_moveDirection;
	private List<Transform> waypoint;
	private bool b_moving = false;
	private float f_timer;
	private float f_delay = 2f;

	void Start()
	{
		m_transform = transform;
		waypoint = GetPath();
	}

	void Update()
	{
		if(b_moving || moveAlways)
		{
			f_timer += Time.deltaTime;
			if(moveAlways || f_timer > f_delay)
			{
				if ((m_transform.position - waypoint[i_index].position).sqrMagnitude > f_range)
				{
					v_moveDirection = waypoint [i_index].position - m_transform.position;
					v_moveDirection.Normalize();
				}
				else NextIndex();

				m_transform.Translate(v_moveDirection * Time.deltaTime * speed);
			}
		}
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
			if(t[i].tag == str)//"PlatformWaypoint")
				gos.Add(t[i].gameObject);
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

	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.tag == "Player" && other.transform.position.y > m_transform.position.y)
			b_moving = true;
	}




}
