using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour {

	float fullHP = 100f;
	float currentHP;
	// Use this for initialization
	void Start () {
		currentHP = fullHP;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void TakeDamage(int damage){
		currentHP -= damage;
		if(currentHP <= 0)
			Die();
	}

	void Die(){

	}
}
