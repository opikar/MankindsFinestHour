using UnityEditor;
using UnityEngine;
using System.Collections;

[CustomEditor(typeof(PickupScript))]
public class PickupEditor : Editor {
	private void UpdateColor() {
		Color color;
		PickupScript script = target as PickupScript;
		SpriteRenderer renderer = script.GetComponent<SpriteRenderer>();
		switch(script.type) {
		case PickupType.health:
			color = Color.red;
			break;
		case PickupType.laser:
			color = new Color(255, 0, 255);
			break;
		case PickupType.score:
			color = Color.yellow;
			break;
		default:
			color = Color.gray;
			break;
		}
		if(renderer.color != color) {
		 	renderer.color = color;
			EditorUtility.SetDirty(target);
		}
	}

	private void ChangeName() {
		PickupScript script = target as PickupScript;
		if(target.name != script.type.ToString()) {
			target.name = script.type.ToString();
			EditorUtility.SetDirty(target);
		}
	}

	public override void OnInspectorGUI ()
	{
		DrawDefaultInspector();
		
		UpdateColor ();
		ChangeName();
	}
}

