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

	void OnCollisionEnter2D(Collision2D other)
    {
		if(isPlayer && other.gameObject.tag == "Enemy")
        {
			other.gameObject.GetComponent<Character>().ApplyDamage(damage);
		}
		else if(!isPlayer && other.gameObject.tag == "Player")
        {
			other.gameObject.GetComponent<Character>().ApplyDamage(damage);
		}
        Destroy(gameObject);
    }
    #endregion
}
