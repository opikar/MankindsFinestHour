using UnityEngine;
using System.Collections;

public class PlayerSpawnerScript : MonoBehaviour {
	public GameObject playerPrefab;

	void OnTriggerEnter2D(Collider2D collider) {
		if(collider.name == "Spawner") {
			if(GameState.player == null) {
				GameState.player = Instantiate(playerPrefab, transform.position, transform.rotation) as GameObject;
				DontDestroyOnLoad(GameState.player);
			}
			else {
				GameState.player.transform.position = transform.position;
				GameState.player.rigidbody2D.velocity = new Vector2(0, 0);
			}

			Destroy (gameObject);
		}
	}
}
