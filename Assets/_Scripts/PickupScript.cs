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

		Color color;
		switch(type) {
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
	}

	void Update() {
		transform.localScale = size + 0.2f * size * Mathf.Abs(Mathf.Sin(Time.time*4));
	}

	void OnTriggerEnter2D(Collider2D col) {
		if(col.name == "Player")
			Destroy (gameObject);
	}
}
