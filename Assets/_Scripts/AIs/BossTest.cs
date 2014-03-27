using UnityEngine;
using System.Collections;

public class BossTest : ScriptedEnemy {

	public float shootingTime = 2f;
	public float bulletPerSecond;
	public float waitAfterAction = 1.5f;

	private float timer;
	private float maxY, minY;
	private Transform player;
	private bool shootLeft;
	private Action lastAction;

	override protected void Start () {
		base.Start ();
		lastAction = null;
		Transform top = GameObject.Find ("BossMaxYPosition").transform;
		Transform down = GameObject.Find ("BossMinYPosition").transform;
		maxY = top.position.y;
		minY = down.position.y;

		bulletPerSecond = 1 / bulletPerSecond;
		player = GameObject.Find ("Player").GetComponent<Transform>();

		FlipBoss ();

		Action action1 = Shoot;
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
		FlipBoss ();
	}

	void FlipBoss()
	{
		if (!GetFacingRight() && player.transform.position.x > m_transform.position.x)
			Flip ();
		else if (GetFacingRight() && player.transform.position.x < m_transform.position.x)
			Flip ();
		Move (0);
	}

	IEnumerator Suicide() {
		Debug.Log("The boss had killed itself in the dressing room.");
		Application.LoadLevel("Cutscene1");
		yield return null;
	}
	IEnumerator Jump()
	{
		if(m_transform.position.y < maxY && lastAction != Drop)
		{
			lastAction = Jump;
			Jump ();
			yield return new WaitForSeconds(waitAfterAction);
		}
		else
			yield return null;
	}
	IEnumerator Shoot()
	{
		lastAction = Shoot;
		timer = 0;
		int num = Random.Range (1, 6);
		while (timer < num) 
		{
			SetTarget (player);
			ShootPrimaryWeapon ();
			yield return new WaitForSeconds(0.15f);
			timer++;
		}
		yield return new WaitForSeconds(waitAfterAction);
	}

	IEnumerator ShootRapidly()
	{
		lastAction = ShootRapidly;
		SetTarget (player);
		timer = 0;
		while (timer < shootingTime) 
		{
			timer += Time.deltaTime;
			ShootPrimaryWeapon();
			yield return new WaitForSeconds(bulletPerSecond);
		}
		yield return new WaitForSeconds(waitAfterAction);
	}
	IEnumerator Drop()
	{
		if (m_transform.position.y > minY && lastAction != Jump) 
		{
			lastAction = Drop;
			Drop ();
			yield return new WaitForSeconds (waitAfterAction);
		} 
		else
			yield return null;
	}
}
