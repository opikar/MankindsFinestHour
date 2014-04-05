using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SaveScript : MonoBehaviour {

    /*
    
    */
    public static SaveScript save = new SaveScript();
	int currentLevel;
	int lives;

    private string[] levels = new string[5] { "TrueLevel1", "TrueLevel2", "TrueLevel3", "TrueLevel4", "TrueLevel5" };
    Dictionary<string, bool> passedLevels;

    SaveScript()
    {
        passedLevels = new Dictionary<string, bool>() 
        { 
            { levels[0], false },
            { levels[1], false },
            { levels[2], false },
            { levels[3], false },
            { levels[4], false },
        };
    }


    /// <summary>
    /// Get the name of the level by its number. 
    /// </summary>
    /// <param name="level"></param>
    /// <returns></returns>
    public string GetLevelName(int level)
    {
        if (level < 0) level = 0;
        if (level > 4) level = 4;
        return levels[level];
    }

	void Save()
	{
        if (passedLevels["TrueLevel1"])
        { }
    }
	void Load()
	{}
}
