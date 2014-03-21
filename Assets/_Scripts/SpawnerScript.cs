using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
using System.Collections;

public class SpawnerScript : MonoBehaviour {
	[HideInInspector]
	public string choice;

	public Vector2 gizmoSize = new Vector2(2, 3);

	void OnTriggerEnter2D(Collider2D collider) {
		if(collider.name == "Spawner") {
			GameObject character = GameState.GetCharacter(choice);

			character.GetComponent<Character>().Reset();
			character.transform.position = transform.position;

			Destroy (gameObject);
		}
	}
#if UNITY_EDITOR
	void OnDrawGizmos() {
		Gizmos.color = Color.cyan;
		Gizmos.DrawCube(transform.position, gizmoSize);
		Handles.Label(transform.position + new Vector3(-gizmoSize.x/2, gizmoSize.y, 0), choice);
	}
#endif
}
