using UnityEngine;
using System.Collections;

public class BossLevel4Manager : LevelManager {



    public override void CompleteLevel()
    {
        SaveScript.save.ReachLevel(SaveScript.save.GetLevelName(4));
        Application.LoadLevel("Level4CutsceneFinal");
    }

}