using UnityEngine;
using System.Collections;

public class EnemyManager : Character {

	// Use this for initialization
	override public void Start () {
		base.Start();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public override void Die ()
	{
		throw new System.NotImplementedException ();
	}
}
