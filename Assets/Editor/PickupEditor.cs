using UnityEditor;
using UnityEngine;
using System.Collections;

[CustomEditor(typeof(PickupScript))]
public class PickupEditor : Editor {
	public void UpdateColor() {
		Color color;
		PickupScript script = target as PickupScript;
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
		script.GetComponent<SpriteRenderer>().color = color;
	}

	public void OnEnable() {
		UpdateColor ();
	}

	public void OnDisable() {
		UpdateColor ();
	}
}

