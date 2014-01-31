using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour {

	bool shootRight;
	Vector3 spawnBullet;
	public GameObject bullet;
	GameObject clone;
	float bulletSpeed = 25f;
	Transform m_transform;
	Vector3 shootDirection;
	Movement movement;
	public Transform shootSpawn;

	float angle = 0f;
	// Use this for initialization
	void Start () {
		m_transform = transform;
		movement = GetComponent<Movement>();
		shootDirection = m_transform.right;
	}
	
	// Update is called once per frame
	void Update () {

		Debug.DrawLine(m_transform.position, shootSpawn.position);
		shootRight = movement.facingRight;
		if(Input.GetKey(KeyCode.LeftControl))
			ShootNormalGun();
	}

	void ShootNormalGun(){
		clone = Instantiate(bullet, shootSpawn.position, Quaternion.identity) as GameObject;
		clone.rigidbody2D.velocity = (shootSpawn.position - m_transform.position).normalized * bulletSpeed;
		Destroy(clone, 5f);
	}
	void ShootSpecialGun(){

	}
	void MeleeAttack(){

	}

	public void MoveShootingTarget(float axis){
		if((angle < 90 || axis == -1) && (angle > -90 || axis == 1))
			angle += axis * Time.deltaTime * 100f;
		shootSpawn.position = m_transform.position;
		if(shootRight)
			shootSpawn.position += new Vector3(Mathf.Cos(Mathf.Deg2Rad * angle), Mathf.Sin(Mathf.Deg2Rad * angle),  0f) * 2f;
		else
			shootSpawn.position += new Vector3(Mathf.Cos(Mathf.Deg2Rad * angle) * -1, Mathf.Sin(Mathf.Deg2Rad * angle),  0f) * 2f;
	}
	public void ResetShootingTarget(){

	}
}
