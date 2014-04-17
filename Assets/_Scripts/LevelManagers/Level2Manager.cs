using UnityEngine;
using System.Collections;

public class Level2Manager : LevelManager {

    public override void CompleteLevel()
    {
        GameObject.Find("Manager").GetComponent<GameManager>().SetState(State.Running);
        Application.LoadLevel("BossLevel2");
    }
}
