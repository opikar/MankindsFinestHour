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
		weapon.MoveShootingTarget(axis);
	}
}
