using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SaveScript
{

	/*
    
    */
	public static SaveScript save = new SaveScript();
	int currentLevel;

	public int lives {
		get;
		set;
	}

	public float hp {
		get;
		set;
	}

	public float laser {
		get;
		set;
	}

	public int score {
		get;
		set;
	}

	private string[] levels = new string[5] {
				"TrueLevel1",
				"TrueLevel2",
				"TrueLevel3",
				"TrueLevel4",
				"TrueLevel5"
		};
	public Dictionary<string, bool> availableLevels;

	SaveScript()
	{
		availableLevels = new Dictionary<string, bool>() 
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
		if (level < 0) {
			level = 0;
		}
		if (level > 4) {
			level = 4;
		}
		return levels [level];
	}

	public void ReachLevel(string level)
	{
		availableLevels [level] = true;
	}

	public void ResetLevel(string level)
	{
		availableLevels [level] = false;
	}

	public void ResetAllLevels()
	{
		availableLevels [levels [0]] = true;
		availableLevels [levels [1]] = false;
		availableLevels [levels [2]] = false;
		availableLevels [levels [3]] = false;
		availableLevels [levels [4]] = false;
	}

	void Save()
	{
        
	}

	void Load()
	{
	}
}
