using UnityEngine;
using System.Collections;

public class TurretEnemy : MonoBehaviour {

	public float distance = 1000f;

	private Transform t_player;
	private Transform m_transform;
	private EnemyManager enemyManager;

	// Use this for initialization
	void Start () {
		enemyManager = GetComponent<EnemyManager>();
		t_player = GameObject.Find("Player").GetComponent<Transform>();
		m_transform = transform;
	}
	
	// Update is called once per frame
	void Update () {
		if(Vector3.SqrMagnitude(t_player.position - m_transform.position) < distance) {
			enemyManager.SetTarget(t_player);
			enemyManager.ShootPrimaryWeapon();
		}
	}
}
