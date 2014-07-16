using UnityEngine;
using System.Collections;

public class FlappyBoss : ScriptedEnemy {

    


	override protected void Start () 
	{
		base.Start ();

		//Action action1 = ShootAction;
        //Action action2 = ShootRapidlyAction;
        Action action3 = JumpAction;
        Action action4 = JumpSideWays;
        Action action5 = DropAction;


		//currentState.AddAction (action1);
        //currentState.AddAction (action2);
        currentState.AddAction (action3);
        currentState.AddAction (action4);
        currentState.AddAction (action5);
	}

    protected IEnumerator JumpSideWays()
    {        
        if (GetGrounded() && lastAction != JumpSideWays)
        {
            
            lastAction = JumpSideWays;
            float move = 0;
            move = (Mathf.Abs(maxX - transform.position.x) < Mathf.Abs(minX - transform.position.x)) ? -1f : 1f;

            if (move > 0)
            {
                while (m_transform.position.x < maxX)
                {
                    Move(move);
                    if (m_transform.position.x > platformPosition.x && GetGrounded())
                        Jump();
                    yield return null;
                }
            }
            else if(move < 0)
            {
                while (m_transform.position.x > minX)
                {
                    Move(move);
                    if (m_transform.position.x < platformPosition.x && GetGrounded())
                        Jump();
                    yield return null;
                }
            }


            /*do
            {
                Move(move);

                if (GetGrounded())
                {
                    if (move < 0 && m_transform.position.x < platformPosition.x)
                        Jump();
                    else if (move > 0 && m_transform.position.x > platformPosition.x)
                        Jump();
                }

                yield return null;
            } while (m_transform.position.x > minX && m_transform.position.x < maxX);*/
            yield return new WaitForSeconds(waitAfterAction);
            
        }

        /*if (GetGrounded() && lastAction != JumpSideWays)
        {
            lastAction = JumpSideWays;
            float move = 0;
            if (Mathf.Abs(maxX - transform.position.x) < Mathf.Abs(minX - transform.position.x))
            {
                yield return StartCoroutine(MoveToPoint(platformPosition.x - platformScale.x * 0.4f));
                move = -1f;
            }
            else
            {
                yield return StartCoroutine(MoveToPoint(platformPosition.x + platformScale.x * 0.4f));
                move = 1;
            }
            Jump();
            yield return new WaitForSeconds(0.1f);
            hitGround = false;
            while (m_transform.position.x > minX && m_transform.position.x < maxX)
            {
                Move(move);
                yield return null;
            }
            while (!GetGrounded() || !hitGround) yield return null;
            yield return StartCoroutine(MoveToPoint(platformPosition.x));
            yield return new WaitForSeconds(waitAfterAction);
        }*/
    }
    protected IEnumerator MoveToPoint(float pointX)
    {
        float moving = 0;
        float sign = Mathf.Sign(transform.position.x - pointX);
        if (pointX > transform.position.x)
            moving = 1f;
        else if (pointX < transform.position.x)
            moving = -1f;
        while (Mathf.Abs(transform.position.x - pointX) > 0.2f && Mathf.Sign(transform.position.x - pointX) == sign)
        {
            Move(moving);
            yield return null;
        }
    }
}
