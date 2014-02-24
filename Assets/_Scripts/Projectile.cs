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
    #endregion

    #region UNITY_METHODS
	void Start () 
    {
        SpriteRenderer sp = GetComponent<SpriteRenderer>();
        sp.sprite = sprite;
	}

	void Update()
	{
		if(!renderer.isVisible)
			Destroy(gameObject);
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
    #endregion
}
