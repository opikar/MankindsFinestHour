using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Boss4 : ScriptedEnemy {


    private Vector3 north, northEast, northWest, west, east, south, southEast, southWest, target, middle, topLeftCorner, topRightCorner;
    private int nextAction;
    public float collisionDamage = 20f;
    public float moveSpeed = 10f;
    bool rage;

	override protected void Start () {
		base.Start ();

        Action action = MoveToTarget;

        north = GameObject.Find("North").transform.position;
        northEast = GameObject.Find("Northeast").transform.position;
        northWest = GameObject.Find("Northwest").transform.position;
        west = GameObject.Find("West").transform.position;
        east = GameObject.Find("East").transform.position;
        south = GameObject.Find("South").transform.position;
        southEast = GameObject.Find("Southeast").transform.position;
        southWest = GameObject.Find("Southwest").transform.position;
        middle = GameObject.Find("Middle").transform.position;
        topLeftCorner = GameObject.Find("TopLeftCorner").transform.position;
        topRightCorner = GameObject.Find("TopRightCorner").transform.position;

        target = south;
		currentState.AddAction (action);
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            PlayerManager pl = other.GetComponent<PlayerManager>();
            if (pl != null)
                pl.ApplyDamage(collisionDamage);
        }
    }

    public override void Stomped()
    {
        
    }

    public IEnumerator MoveToTarget() 
    {
        while (Vector3.SqrMagnitude(target - transform.position) >= 0.5f)
        {
            if (health.f_currentHP < GetMaxHealth() * .5f)
                Rage();
            
            transform.Translate((target - transform.position).normalized * Time.deltaTime * moveSpeed);
            if (Vector3.SqrMagnitude(target - transform.position) < 0.5f)
            {
                if (target == north)
                    yield return StartCoroutine(Swoop());

                else if (target == northEast || target == northWest || target == east || target == west)
                    if (!rage)
                        yield return StartCoroutine(ShootAction());
                    else
                        StartCoroutine(ShootAction());

                if (target == topRightCorner || target == topLeftCorner)
                        yield return StartCoroutine(Barrage());

                else
                    NextTarget();
            }
            yield return null;
        }
    }

    public IEnumerator Swoop()
    {
        target = middle;
        while (Vector3.SqrMagnitude(target - transform.position) > 0.5f)
        {
            transform.Translate((target - transform.position).normalized * Time.deltaTime * moveSpeed);
            yield return null;
        }

        target = north;

        while (Vector3.SqrMagnitude(target - transform.position) > 0.5f)
        {
            transform.Translate((target - transform.position).normalized * Time.deltaTime * moveSpeed);
            yield return null;
        }
    }

    public IEnumerator Barrage()
    {
        float originalX = target.x;

        if (target == topLeftCorner)
            target = topRightCorner;
        else if (target == topRightCorner)
            target = topLeftCorner;

        int shotCount = 0;
        
        while (Vector3.SqrMagnitude(target - transform.position) > 0.5f)
        {
            transform.Translate((target - transform.position).normalized * Time.deltaTime * moveSpeed);
            if (originalX - shotCount * 10f > transform.position.x && target == topLeftCorner
                || originalX + shotCount * 10f < transform.position.x && target == topRightCorner)
            {
                StartCoroutine(ShootBigBullet());
                shotCount++;
            }
            yield return null;
        }

        if (target == topLeftCorner)
            target = northWest;
        else if (target == topRightCorner)
            target = northEast;

    }

    protected override IEnumerator ShootAction()
    {
        lastAction = ShootAction;
        timer = 0;
        int num = UnityEngine.Random.Range(1, 6);
        while (timer < num)
        {
            SetTarget(player);
            ShootPrimaryWeapon();
            yield return new WaitForSeconds(0.15f);
            timer++;
        }
        yield return new WaitForSeconds(waitAfterAction);
    }

    protected override IEnumerator ShootBigBullet()
    {
        lastAction = ShootBigBullet;
        SwapBullet();
        yield return StartCoroutine(ShootDown());
        SwapBullet();
    }

    protected IEnumerator ShootDown()
    {       
        lastAction = ShootOnce;
        AimVertical(-1f, 0);
        ShootPrimaryWeapon();
        yield return null;
    }

    private void NextTarget()
    {

        if (target == north)
        {
            nextAction = Random.Range(0, 4);
            if (nextAction == 0)
                target = northEast;
            else if (nextAction == 1)
                target = northWest;
            else if (nextAction == 2)
                target = topLeftCorner;
            else
                target = topRightCorner;
        }
        else if (target == northEast)
        {
            nextAction = Random.Range(0, 4);
            if (nextAction == 0)
                target = north;
            else if (nextAction == 1)
                target = east;
            else if (nextAction == 2)
                target = northWest;
            else
                target = topRightCorner;
        }
        else if (target == northWest)
        {
            nextAction = Random.Range(0, 4);
            if (nextAction == 0)
                target = north;
            else if (nextAction == 1)
                target = west;
            else if (nextAction == 2)
                target = northEast;
            else
                target = topLeftCorner;
        }
        else if (target == west)
        {
            nextAction = Random.Range(0, 2);
            if (nextAction == 0)
                target = south;
            else
                target = northWest;
        }
        else if (target == east)
        {
            nextAction = Random.Range(0, 2);
            if (nextAction == 0)
                target = south;
            else
                target = northEast;
        }
        else if (target == south)
        {
            InvokeRepeating("ShootInvoked", 0, bulletPerSecond);
            nextAction = Random.Range(0, 2);
            if (nextAction == 0)
                target = southWest;
            else
                target = southEast;
        }
        else if (target == southEast)
        {
            if (IsInvoking("ShootInvoked"))
                CancelInvoke("ShootInvoked");
            target = east;
        }
        else if (target == southWest)
        {
            if (IsInvoking("ShootInvoked"))
                CancelInvoke("ShootInvoked");
            target = west;
        }
        else if (target == topLeftCorner)

        { }

        else if (target == topRightCorner)

        { }

    }

    void Rage()
    {
        waitAfterAction = 0;
        rage = true;
        moveSpeed = 12f;
    }

    void ShootInvoked()
    {
        lastAction = ShootOnce;
        SetTarget(player);
        ShootPrimaryWeapon();
    }
    
}
