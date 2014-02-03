using UnityEngine;
using System.Collections;

public class SpawnerScript : MonoBehaviour {
	public GameObject enemyType;

	void OnTriggerEnter2D(Collider2D collider) {
		Instantiate(enemyType, transform.position, transform.rotation);
		Destroy (gameObject);
	}
}
