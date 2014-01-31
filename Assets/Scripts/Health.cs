using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour {

	public float fullHP = 100f;
	float currentHP;
	Character character;
	// Use this for initialization
	void Start () {
		character = GetComponent<Character>();
		currentHP = fullHP;
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	public void ApplyDamage (float dmg)
	{
		currentHP -= dmg;
		if(currentHP <= 0f)
			character.Die();
	}
}
