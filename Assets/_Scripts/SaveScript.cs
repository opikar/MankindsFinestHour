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
    public Dictionary<string, bool> passedLevels;

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

    public void CompleteLevel(string level)
    {
        passedLevels[level] = true;
    }

    public void ResetLevel(string level)
    {
        passedLevels[level] = false;
    }

    public void ResetAllLevels()
    {
        passedLevels[levels[0]] = false;
        passedLevels[levels[1]] = false;
        passedLevels[levels[2]] = false;
        passedLevels[levels[3]] = false;
        passedLevels[levels[4]] = false;
    }

	void Save()
	{
        
    }
	void Load()
	{}
}
