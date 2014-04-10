using UnityEngine;
using System.Collections;

public class BossLevel1Manager : LevelManager {

    protected override void CompleteLevel()
    {
        SaveScript.save.CompleteLevel(SaveScript.save.GetLevelName(0));
        Application.LoadLevel("Level1CutsceneFinal");
    }
}
