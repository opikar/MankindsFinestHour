using UnityEngine;
using System.Collections;

public abstract class Character : MonoBehaviour {

	public GameManager gameManager;
	public Health health;
	public Weapon weapon;
	public Movement movement;

	// Use this for initialization
	public virtual void Start () {
		movement = GetComponent<Movement>();
		health = GetComponent<Health>();
	}
}
