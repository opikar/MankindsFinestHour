using UnityEngine;
using System.Collections;

public class DepressedBoss : ScriptedEnemy {

    private bool shootLeft;


	override protected void Start () {
		base.Start ();

		Action action1 = JumpSideWays;
		Action action2 = JumpAction;
		Action action3 = ShootRapidlyAction;
		Action action4 = DropAction;
        Action action5 = ShootAction;

		currentState.AddAction (action1);
		currentState.AddAction (action2);
		currentState.AddAction (action3);
		currentState.AddAction (action4);
        currentState.AddAction (action5);
	}
}
