using UnityEngine;
using System.Collections;

public class FlappyBoss : ScriptedEnemy {


	override protected void Start () 
	{
		base.Start ();

		Action action1 = Suicide;

		currentState.AddAction (action1);
	}

	IEnumerator Suicide() {
		Debug.Log("The boss had killed itself in the dressing room.");
		Application.LoadLevel("Cutscene1");
		yield return null;
	}
}
