using UnityEngine;
using System.Collections;

public class BossLevel1Manager : LevelManager {

    protected override void PassLevel()
    {
        SaveScript.save.CompleteLevel(SaveScript.save.GetLevelName(0));
    }
}
