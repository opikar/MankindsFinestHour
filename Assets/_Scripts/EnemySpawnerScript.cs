using UnityEngine;
using System.Collections;

public class EnemySpawnerScript : MonoBehaviour {
	public GameObject enemyType;

	void OnTriggerEnter2D(Collider2D collider) {
		Instantiate(enemyType, transform.position, transform.rotation);
		Destroy (this);
	}
}
