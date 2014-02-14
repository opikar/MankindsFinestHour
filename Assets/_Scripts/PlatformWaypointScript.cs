using UnityEngine;
using System.Collections;

public class PlatformWaypointScript : MonoBehaviour {

	private Vector3 position;
	bool used;

	void Awake()
	{
		position = transform.position;
	}

	void Update()
	{
		transform.position = position;
	}

	public bool GetUsed(){
		return used;
	}
	public void SetUsed(bool b){
		used = b;
	}
	public float radius = 0.5f;
	void OnDrawGizmosSelected() {
		Gizmos.color = Color.red;
		Gizmos.DrawSphere(transform.position, radius);
	}
}
