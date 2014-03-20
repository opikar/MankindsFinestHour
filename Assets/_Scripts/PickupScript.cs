using UnityEngine;
using System.Collections;

public enum PickupType {
	health,
	score,
	laser
}

public class PickupScript : MonoBehaviour {
	public PickupType type;
	public int amount;

	Vector2 size;
	void Start() {
		size = transform.localScale;
	}

	void Update() {
		transform.localScale = size + 0.2f * size * Mathf.Abs(Mathf.Sin(Time.time*4));
	}

	void OnTriggerEnter2D(Collider2D col) {
		if(col.name == "Player")
			Destroy (gameObject);
	}
}
