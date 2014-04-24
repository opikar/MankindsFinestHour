using UnityEngine;
using System.Collections;

public class Boss3 : ScriptedEnemy {


	override protected void Start () {
		base.Start ();
		Action action1 = JumpAction;
		Action action2 = ShootRapidlyAction;
		Action action3 = DropAction;
        Action action4 = ShootAction;
        Action action5 = ShootBigBullet;

		currentState.AddAction(action1);
		currentState.AddAction(action2);
		currentState.AddAction(action3);
		currentState.AddAction(action4);
        currentState.AddAction(action5);
	}

    public override void Stomped()
    {
        print("boss3");
    }
}
