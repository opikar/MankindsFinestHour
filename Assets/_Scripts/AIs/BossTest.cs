using UnityEngine;
using System.Collections;

public class BossTest : ScriptedEnemy {
	void Start () {
		Action action1 = print1;
		action1 += print2;
		Action action2 = print3;

		currentState.AddAction (action1);
		currentState.AddAction (action2);
	}

	IEnumerator print1() {
		print ("Foo");
		yield return null;
	}

	IEnumerator print2() {
		print ("Bar");
		yield return null;
	}

	IEnumerator print3() {
		print ("Baz");
		yield return null;
	}
}
