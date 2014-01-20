using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour {

	public GameObject bullet;
	public Transform bulletSpawn;
	GameObject clone;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Z))
		{
			clone = Instantiate(bullet, bulletSpawn.position, bulletSpawn.rotation) as GameObject;
		}
	}
}
