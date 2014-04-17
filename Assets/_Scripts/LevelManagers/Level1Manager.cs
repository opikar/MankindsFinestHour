using UnityEngine;
using System.Collections;

public class Level1Manager : LevelManager {

    public override void CompleteLevel()
    {
        GameObject.Find("Manager").GetComponent<GameManager>().SetState(State.Running);
        Application.LoadLevel("BossLevel1");
    }
}
