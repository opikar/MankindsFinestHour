using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour
{

    #region MEMBERS
    public Vector2 velocity;
	public Sprite sprite;
	public float damage;
	public bool gravity;
	public bool isPlayer;
	public float speed = 50f;
    #endregion

    #region UNITY_METHODS
	void Start () 
    {
        SpriteRenderer sp = GetComponent<SpriteRenderer>();
        sp.sprite = sprite;
		rigidbody2D.velocity *= speed;

	}

	void OnTriggerEnter2D(Collider2D other)
    {
		if(isPlayer && other.tag == "Enemy")
        {
			other.GetComponent<Character>().ApplyDamage(damage);
			Destroy(gameObject);
		}
		else if(!isPlayer && other.tag == "Player")
        {
			other.GetComponent<Character>().ApplyDamage(damage);
			Destroy(gameObject);
		}
    }

	void OnBecameInvisible() {
		Destroy(gameObject);
	}
    #endregion
}
