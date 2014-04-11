using UnityEngine;
using System.Collections;

public class Level1Manager : LevelManager {

    public override void CompleteLevel()
    {
        Application.LoadLevel("BossLevel1");
    }
}
