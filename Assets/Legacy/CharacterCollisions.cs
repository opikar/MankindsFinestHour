﻿using UnityEngine;
using System.Collections;

public class CharacterCollisions : MonoBehaviour {

	Vector2 velocity;
	Vector2 direction;
	Ray2D ray;
	RaycastHit2D rayHit;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		ray = new Ray2D(transform.position, -Vector2.up);
		Debug.DrawRay(transform.position, -Vector2.up);

	}
}