using UnityEngine;
using System.Collections;

public class BossLevel3Manager : LevelManager {



    public override void CompleteLevel()
    {
        SaveScript.save.ReachLevel(SaveScript.save.GetLevelName(3));
        Application.LoadLevel("Level3CutsceneFinal");
    }

}