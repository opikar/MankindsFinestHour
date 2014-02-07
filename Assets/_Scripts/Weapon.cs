using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour {

	#region MEMBERS
	public GameObject bullet;
	public Transform shootSpawn;
	public float rateOfFire = .3f;
	public bool autoShoot;

    private new Transform transform;
	private bool b_shootRight;
	private GameObject m_clone;
	private float f_bulletSpeed = 25f;	
	private float f_lastShot;
#pragma warning disable 414
	private Vector3 v_shootDirection;
#pragma warning restore 414
	private Movement m_movement;
	private float f_angle = 0f;
	#endregion

    #region UNITY_METHODS
    void Start () 
    {
		f_lastShot = Time.time;
		transform = base.transform;
		m_movement = GetComponent<Movement>();
		v_shootDirection = transform.right;
	}
	
	// Update is called once per frame
	void Update () 
    {
		Debug.DrawLine(transform.position, shootSpawn.position);
		
	}
    #endregion
    public void ShootPrimaryWeapon()
    {
		m_clone = Instantiate(bullet, shootSpawn.position, Quaternion.identity) as GameObject;
		m_clone.rigidbody2D.velocity = (shootSpawn.position - transform.position).normalized * f_bulletSpeed;
		Destroy(m_clone, 5f);
	}
	public void ShootSpecialGun()
    {

	}
	public void MeleeAttack()
    {

	}

	public void MoveShootingTarget(float axisVertical, float axisHorizontal)
    {
		f_angle = axisVertical * 90f;

		if(axisVertical != 0 && axisHorizontal != 0)
			f_angle = 30f * axisVertical;
        
		b_shootRight = m_movement.facingRight;
		shootSpawn.position = transform.position;
		if(b_shootRight)
			shootSpawn.position += new Vector3(Mathf.Cos(Mathf.Deg2Rad * f_angle), Mathf.Sin(Mathf.Deg2Rad * f_angle),  0f) * 2f;
		else
			shootSpawn.position += new Vector3(Mathf.Cos(Mathf.Deg2Rad * f_angle) * -1, Mathf.Sin(Mathf.Deg2Rad * f_angle),  0f) * 2f;
	}
	/*public void MoveShootingTarget(float axis){
		if((angle < 90 || axis == -1) && (angle > -90 || axis == 1))
			angle += axis * Time.deltaTime * 100f;
		if(angle > 90f) angle = 90f;
		if(angle < -90f) angle = -90f;
		shootSpawn.position = m_transform.position;
		if(shootRight)
			shootSpawn.position += new Vector3(Mathf.Cos(Mathf.Deg2Rad * angle), Mathf.Sin(Mathf.Deg2Rad * angle),  0f) * 2f;
		else
			shootSpawn.position += new Vector3(Mathf.Cos(Mathf.Deg2Rad * angle) * -1, Mathf.Sin(Mathf.Deg2Rad * angle),  0f) * 2f;
	}*/
}
