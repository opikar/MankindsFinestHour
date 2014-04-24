using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Boss4 : ScriptedEnemy {


    private Vector3 north, northEast, northWest, west, east, south, southEast, southWest, target, middle, topLeftCorner, topRightCorner;
    private int nextAction;
    public float moveSpeed = 0.001f;

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

        target = east;
		currentState.AddAction (action);
	}

    public IEnumerator MoveToTarget() 
    {
        while (Vector3.SqrMagnitude(target - transform.position) >= 0.5f)
        {
            transform.Translate((target - transform.position).normalized * Time.deltaTime * moveSpeed);
            if (Vector3.SqrMagnitude(target - transform.position) < 0.5f)
            {
                //yield return new WaitForSeconds(waitAfterAction);
                if (target == north)
                    yield return StartCoroutine(Swoop());

                else if (target == northEast || target == northWest || target == east || target == west)
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
        yield return null;
    }

    public IEnumerator Barrage()
    {
        if (target == topLeftCorner)
            target = topRightCorner;
        else if (target == topRightCorner)
            target = topLeftCorner;

        while (Vector3.SqrMagnitude(target - transform.position) > 0.5f)
        {
            transform.Translate((target - transform.position).normalized * Time.deltaTime * moveSpeed);
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

    private void NextTarget()
    {
        nextAction = Random.Range(0,4);

        if (target == north)

            if (nextAction == 0)
                target = northEast;
            else
                target = northWest;

        else if (target == northEast)

            if (nextAction == 0)
                target = north;
            else if (nextAction == 1)
                target = east;
            else if (nextAction == 2)
                target = northWest;
            else
                target = topRightCorner;

        else if (target == northWest)

            if (nextAction == 0)
                target = north;
            else if (nextAction == 1)
                target = west;
            else if (nextAction == 2)
                target = northEast;
            else
                target = topLeftCorner;

        else if (target == west)

            if (nextAction == 0)
                target = south;
            else
                target = northWest;

        else if (target == east)

            if (nextAction == 0)
                target = south;
            else
                target = northEast;

        else if (target == south)

            if (nextAction == 0)
                target = southWest;
            else
                target = southEast;

        else if (target == southEast)

            target = east;

        else if (target == southWest)

            target = west;

        else if (target == topLeftCorner)
        
        { }

        else if (target == topRightCorner)
        
        { }

    }
}
