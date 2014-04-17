using UnityEngine;
using System.Collections;

public class BossLevel2Manager : LevelManager {



    public override void CompleteLevel()
    {
        SaveScript.save.ReachLevel(SaveScript.save.GetLevelName(2));
        Application.LoadLevel("Level2CutsceneFinal");
    }

}