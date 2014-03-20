using UnityEditor;
using UnityEngine;
using System.Collections;

[CustomEditor(typeof(PickupScript))]
public class PickupEditor : Editor {
	private void UpdateColor() {
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

	private void ChangeName() {
		PickupScript script = target as PickupScript;
		target.name = script.type.ToString();
	}

	public void OnEnable() {
		UpdateColor ();
		ChangeName();
	}

	public void OnDisable() {
		UpdateColor ();
		ChangeName();
	}
}

