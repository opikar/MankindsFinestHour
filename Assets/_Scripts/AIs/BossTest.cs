using UnityEngine;
using System.Collections;

public class BossTest : ScriptedEnemy {

	public float shootingTime = 2f;
	public float bulletPerSecond;
	public float waitAfterAction = 1.5f;

	public float maxY, minY, maxX, minX;
	private EnemyManager manager;
	private Transform player;
	private bool shootLeft;
	private Transform m_transform;

	void Start () {
		m_transform = transform;
		Transform topleft = GameObject.Find ("BossTopLeftPosition").transform;
		Transform downRight = GameObject.Find ("BossDownRightPosition").transform;
		maxY = topleft.position.y;
		maxX = downRight.position.x;
		minX = topleft.position.x;
		minY = downRight.position.y;
		bulletPerSecond = 1 / bulletPerSecond;
		manager = GetComponent<EnemyManager> ();
		player = GameObject.Find ("Player").GetComponent<Transform>();

		Action action1 = ShootOnce;
		Action action2 = Jump;
		Action action3 = ShootRapidly;
		Action action4 = Drop;

		currentState.AddAction (action1);
		currentState.AddAction (action2);
		currentState.AddAction (action3);
		currentState.AddAction (action4);
	}

	protected override void Update()
	{
		base.Update ();
		if (!manager.GetFlipped ()) 
		{
			if (!manager.GetFacingRight() && player.transform.position.x > m_transform.position.x)
				manager.Flip ();
			else if (manager.GetFacingRight() && player.transform.position.x < m_transform.position.x)
				manager.Flip ();
		}
		manager.Move (0);
	}

	IEnumerator Suicide() {
		Debug.Log("The boss had killed itself in the dressing room.");
		Application.LoadLevel("Cutscene1");
		yield return null;
	}
	IEnumerator Jump()
	{
		if(m_transform.position.y < maxY)
		{
			manager.Jump ();
			yield return new WaitForSeconds(waitAfterAction);
		}
		else
			yield return null;
	}
	IEnumerator ShootOnce()
	{
		manager.SetTarget (player);
		manager.ShootPrimaryWeapon ();
		yield return new WaitForSeconds(waitAfterAction);
	}

	IEnumerator ShootRapidly()
	{
		manager.SetTarget (player);
		float timer = 0;
		while (timer < shootingTime) 
		{
			timer += Time.deltaTime;
			manager.ShootPrimaryWeapon();
			yield return new WaitForSeconds(bulletPerSecond);
		}
		yield return new WaitForSeconds(waitAfterAction);
	}
	IEnumerator Drop()
	{
		if (m_transform.position.y > minY) 
		{
			manager.Drop ();
			yield return new WaitForSeconds (waitAfterAction);
		} 
		else
			yield return null;
	}
}
