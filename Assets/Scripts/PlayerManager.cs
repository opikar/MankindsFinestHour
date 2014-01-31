using UnityEngine;
using System.Collections;

public class PlayerManager : Character {
	public GUIManager guiManager;

	// Use this for initialization
	override public void Start () {
		base.Start();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void AimVertical (float axis)
	{
		if(axis != 0){
			weapon.MoveShootingTarget(axis);
		}
		else{
			weapon.ResetShootingTarget();
		}
	}
}
