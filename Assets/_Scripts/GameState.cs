using UnityEngine;
using System.Collections.Generic;
using System;

public static class GameState {
	private static Dictionary<string, GameObject> prefabs = new Dictionary<string, GameObject>();
	private static Dictionary<string, List<WeakReference>> objects = new Dictionary<string, List<WeakReference>>();

	private static string[] uniqueNames = { "Player" };
	private static Dictionary<string, GameObject> uniques = new Dictionary<string, GameObject>();

	static GameState() {
		GameObject[] objs = Resources.LoadAll<GameObject>("Characters");
		foreach(GameObject obj in objs) {
			if(Array.IndexOf<string>(uniqueNames, obj.name) >= 0) {
				uniques.Add(obj.name, null);
			}
			prefabs.Add(obj.name, obj);
		}

	}

	public static  string[] GetKeys() {
		string[] keys = new string[prefabs.Count];

		prefabs.Keys.CopyTo(keys, 0);

		return keys;

	}

	public static GameObject GetCharacter(string name) {
		GameObject tmp;

		if(uniques.ContainsKey(name)) {
			if(uniques[name] == null) {
				tmp = MonoBehaviour.Instantiate(prefabs[name]) as GameObject;
				MonoBehaviour.DontDestroyOnLoad(tmp);
				uniques[name] = tmp;
				return tmp;
			} else {
				uniques[name].SetActive(true);
				return uniques[name];
			}
		}

		if(!objects.ContainsKey(name)) {
			if(prefabs.ContainsKey(name)) {
				tmp = MonoBehaviour.Instantiate(prefabs[name]) as GameObject;
				objects.Add (name, new List<WeakReference>());
				objects[name].Add(new WeakReference(tmp));
				return tmp;
			} else {
				return null;
			}
		} else {
			foreach(WeakReference reference in objects[name]) {
				if(!reference.IsAlive) {
					tmp = MonoBehaviour.Instantiate(prefabs[name]) as GameObject;
					reference.Target = tmp;
					return tmp;
				} else {
					tmp = reference.Target as GameObject;
					if(!tmp.activeSelf) {
						tmp.SetActive(true);
						return tmp;
					}
				}
			}

			tmp = MonoBehaviour.Instantiate(prefabs[name]) as GameObject;
			objects[name].Add(new WeakReference(tmp));
			return tmp;
		}
	}
}
