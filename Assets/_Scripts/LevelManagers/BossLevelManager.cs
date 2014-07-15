using UnityEngine;
using System.Collections;

[RequireComponent(typeof(GUIText))]
public abstract class BossLevelManager : LevelManager {

	// Use this for initialization
	public override IEnumerator Start () {
        waitingTime = 3f;
        int timer = 3;
        guiText.fontSize = 50;
        guiText.pixelOffset = new Vector2(Screen.width, Screen.height) * 0.5f;
        GameManager.instance.SetState(startState);
        while (timer >= 1)
        {
            guiText.text = timer.ToString();
            timer--;
            yield return new WaitForSeconds(1f);
        }
        guiText.enabled = false;
        GameManager.instance.SetState(State.Running);
	}
	
	
}
