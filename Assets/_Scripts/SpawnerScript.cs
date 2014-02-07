using UnityEngine;
using System.Collections;

public class SpawnerScript : MonoBehaviour {
	[HideInInspector]
	public string choice;

	public Vector2 gizmoSize = new Vector2(2, 3);

	void OnTriggerEnter2D(Collider2D collider) {
		if(collider.name == "Spawner") {
			GameObject character = GameState.GetCharacter(choice);

			character.transform.position = transform.position;
			character.rigidbody2D.velocity = new Vector2(0, 0);

			Destroy (gameObject);
		}
	}

	void OnDrawGizmos() {
		Gizmos.color = Color.cyan;
		Gizmos.DrawCube(transform.position, gizmoSize);
	}
}
