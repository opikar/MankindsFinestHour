using UnityEditor;
using System.Collections;

[CustomEditor(typeof(SpawnerScript))]
public class SpawnScriptEditor : Editor {
	int index;
	string[] keys;

	public void OnEnable() {
		keys = GameState.GetKeys();
		index = FindSelectedIndex ();
	}

	public override void OnInspectorGUI ()
	{
		DrawDefaultInspector();

		index = EditorGUILayout.Popup(index, keys);

		SpawnerScript tmp = target as SpawnerScript;
		tmp.choice = keys[index];

		EditorUtility.SetDirty(target);
	}


	int FindSelectedIndex() {
		SpawnerScript tmp = target as SpawnerScript;
		for(int i = 0; i < keys.Length; i++) {
			if(keys[i] == tmp.choice)
				return i;
		}
		return 0;
	}
}