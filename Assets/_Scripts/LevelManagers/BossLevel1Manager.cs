using UnityEngine;
using System.Collections;

public class BossLevel1Manager : LevelManager {



    protected override void CompleteLevel()
    {
        SaveScript.save.ReachLevel(SaveScript.save.GetLevelName(1));
        Application.LoadLevel("Level1CutsceneFinal");
    }

}