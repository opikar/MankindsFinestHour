using UnityEngine;
using System.Collections;

public class DestroyerScript : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Player") 
		{
			PlayerManager manager = other.gameObject.GetComponent<PlayerManager> ();
			if (manager != null)
				manager.Die ();
		} 
		else if (other.tag == "Enemy") 
		{
			EnemyManager manager = other.gameObject.GetComponent<EnemyManager> ();
			if (manager != null)
				manager.Die ();
		} 
	}
}
