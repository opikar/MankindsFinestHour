using UnityEngine;
using System.Collections;

public class DepressedBoss : ScriptedEnemy {

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

		Action action1 = ShootAction;
		Action action2 = JumpAction;
		Action action3 = ShootRapidlyAction;
		Action action4 = DropAction;

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
	IEnumerator JumpAction()
	{
		if(m_transform.position.y < maxY)
		{
			lastAction = JumpAction;
			Jump ();
			yield return new WaitForSeconds(waitAfterAction);
		}
		else
			yield return null;
	}
	IEnumerator ShootAction()
	{
		lastAction = ShootAction;
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

	IEnumerator ShootRapidlyAction()
	{
		lastAction = ShootRapidlyAction;
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
	IEnumerator DropAction()
	{
		if (m_transform.position.y > minY) 
		{
			lastAction = DropAction;
			Drop ();
			yield return new WaitForSeconds (waitAfterAction);
		} 
		else
			yield return null;
	}
}
