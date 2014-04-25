using UnityEngine;
using System.Collections;

public class Boss4 : ScriptedEnemy {

    private Vector3 topLeft, topRight, downLeft, downRight, center;

	override protected void Start () {
		base.Start ();
		Action action1 = ShootRapidlyAction;
        Action action2 = ShootBigBullet;
        Action action3 = CornerShot;

        topLeft = Camera.main.ScreenToWorldPoint(new Vector3(0,Screen.height, 0));
        topRight = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
        downLeft = Camera.main.ScreenToWorldPoint(Vector3.zero);
        downRight = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0));
        center = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height) * 0.5f);

        topLeft.z = 0;
        topRight.z = 0;
        downLeft.z = 0;
        downRight.z = 0;
        center.z = 0;

		currentState.AddAction(action1);
		currentState.AddAction(action2);
        currentState.AddAction(action3);
	}

    public override void Stomped()
    {
        print("boss4");
    }

    protected override IEnumerator ShootBigBullet()
    {
        lastAction = ShootBigBullet;
        SwapBullet();
        yield return StartCoroutine(ShootOnce());
        SwapBullet();
    }

    protected override IEnumerator ShootOnce()
    {
        lastAction = ShootOnce;
        SetTarget(player);
        ShootPrimaryWeapon();
        yield return new WaitForSeconds(waitAfterAction);
    }

    protected override IEnumerator ShootRapidlyAction()
    {
        lastAction = ShootRapidlyAction;
        timer = 0;
        while (timer < shootingTime)
        {
            SetTarget(player);
            timer += bulletPerSecond;
            ShootPrimaryWeapon();
            yield return new WaitForSeconds(bulletPerSecond);
        }
        yield return new WaitForSeconds(waitAfterAction);
    }

    protected IEnumerator CornerShot()
    {
        if (health.f_currentHP > GetMaxHealth() * 0.75f) yield break;

        lastAction = CornerShot;
        weapon.ShootFrom(topLeft, center);
        weapon.ShootFrom(topRight, center);
        weapon.ShootFrom(downLeft, center);
        weapon.ShootFrom(downRight, center);

        if (rage)
        {
            weapon.ShootFrom((topLeft + topRight) * 0.5f, center);
            weapon.ShootFrom((topRight + downRight) * 0.5f, center);
            weapon.ShootFrom((downLeft + topLeft) * 0.5f, center);
            weapon.ShootFrom((downRight + downLeft) * 0.5f, center);
        }


        yield return new WaitForSeconds(waitAfterAction);
        
    }
}
