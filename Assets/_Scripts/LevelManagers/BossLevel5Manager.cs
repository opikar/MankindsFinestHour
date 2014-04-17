using UnityEngine;
using System.Collections;

public class BossLevel5Manager : LevelManager {



    public override void CompleteLevel()
    {
        Application.LoadLevel("Level5CutsceneFinal");
    }

}