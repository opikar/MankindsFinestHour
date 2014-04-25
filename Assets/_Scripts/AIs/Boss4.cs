using UnityEngine;
using System.Collections;

public class Boss4 : ScriptedEnemy {

    Vector3 topLeft, topRight, downLeft, downRight;

	override protected void Start () {
		base.Start ();
		Action action1 = ShootRapidlyAction;
        Action action2 = ShootBigBullet;

        topLeft = Camera.main.ScreenToWorldPoint(new Vector3(0,Screen.height));
        topRight = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height));
        downLeft = Camera.main.ScreenToWorldPoint(Vector3.zero);
        downRight = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0));


		currentState.AddAction(action1);
		currentState.AddAction(action2);
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

    protected virtual IEnumerator ShootOnce()
    {
        lastAction = ShootOnce;
        SetTarget(player);
        ShootPrimaryWeapon();
        yield return new WaitForSeconds(waitAfterAction);
    }

    protected IEnumerator ShootRapidlyAction()
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
}
