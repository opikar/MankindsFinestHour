using UnityEngine;
using System.Collections;

public class BossTest : ScriptedEnemy {
	void Start () {
		Action action1 = print;

		currentState.AddAction(action1);
	}

	IEnumerator print() {
		print ("Foo");
		yield return null;
	}
}
