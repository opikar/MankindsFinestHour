using UnityEngine;
using System.Collections;

public class FlappyBoss : ScriptedEnemy {


	override protected void Start () 
	{
		base.Start ();

		Action action1 = ShootAction;
        Action action2 = ShootRapidlyAction;
        Action action3 = JumpAction;
        Action action4 = JumpSideWays;
        Action action5 = DropAction;

		currentState.AddAction (action1);
        currentState.AddAction (action2);
        currentState.AddAction (action3);
        currentState.AddAction (action4);
        currentState.AddAction (action5);
	}
}
