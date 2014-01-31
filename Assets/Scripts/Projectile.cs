using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {

	public Vector2 velocity;
	public Texture2D texture;
	public float damage;
	public bool gravity;
	public bool isPlayer;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

	}

	void OnCollisionEnter2D(Collision2D other){
		if(isPlayer && other.gameObject.tag == "Enemy"){
			other.gameObject.GetComponent<Character>().ApplyDamage(damage);
		}
		else if(!isPlayer && other.gameObject.tag == "Player"){
			other.gameObject.GetComponent<Character>().ApplyDamage(damage);
		}
	}
}
