using UnityEngine;
using System.Collections;

[RequireComponent(typeof(GUITexture))]
public abstract class BossLevelManager : LevelManager {

    public Texture[] numbers;
    private Rect size;
	// Use this for initialization
	public override IEnumerator Start () {
        waitingTime = 3f;
        int timer = 2;
        size = new Rect();
        size.x = 0.5f * (Screen.width - numbers[0].width);
        size.y = 0.5f * (Screen.height - numbers[0].height);
        size.xMax = size.x + numbers[0].width;
        size.yMax = size.y + numbers[0].height;
        guiTexture.pixelInset = size;
        print(size);
        GameManager.instance.SetState(startState);
        while (timer >= 0)
        {
            guiTexture.texture = numbers[timer];
            timer--;
            yield return new WaitForSeconds(1f);
        }
        guiTexture.enabled = false;
        GameManager.instance.SetState(State.Running);
	}
	
	
}
