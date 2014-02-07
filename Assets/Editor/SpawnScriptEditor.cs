using UnityEditor;
using System.Collections;

[CustomEditor(typeof(SpawnerScript))]
public class SpawnScriptEditor : Editor {
	int index = 0;
	string[] keys;

	public void OnEnable() {
		keys = GameState.GetKeys();
	}

	public override void OnInspectorGUI ()
	{
		DrawDefaultInspector();

		index = EditorGUILayout.Popup(index, keys);

		SpawnerScript tmp = target as SpawnerScript;
		tmp.choice = keys[index];

		EditorUtility.SetDirty(target);
	}
}