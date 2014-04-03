using UnityEngine;
using System.Collections;
using System;

public delegate IEnumerator Action();

public abstract class ScriptedEnemy : EnemyManager {
	public ActionState[] states;
	bool actionRunning = false;
	protected ActionState currentState;

    public float shootingTime = 2f;
    public float bulletPerSecond;
    public float waitAfterAction = 1.5f;

    private float maxY, minY, maxX, minX;
    private Transform player;
    private float timer;
    private Action lastAction;
    private Vector3 platformPosition, platformScale;

	override protected void Awake() {
		base.Awake ();
		currentState = states[0];
	}

    override protected void Start()
    {
        base.Start();
        lastAction = null;
        Transform top = GameObject.Find("BossMaxYPosition").transform;
        Transform down = GameObject.Find("BossMinYPosition").transform;
        maxX = top.position.x;
        minX = down.position.x;
        maxY = top.position.y;
        minY = down.position.y;

        bulletPerSecond = 1 / bulletPerSecond;
        player = GameObject.Find("Player").GetComponent<Transform>();

        FlipBoss();
    }

	// Update is called once per frame
	protected virtual void Update () {
		if(!actionRunning) {
			StartCoroutine(RunAction ());
		}
        FlipBoss();
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        //if (GetGrounded())
        {
            platformPosition = other.transform.position;
            platformScale = other.transform.localScale;
        }
    }

    protected IEnumerator RunAction()
    {
		actionRunning = true;
		foreach(Action action in currentState.GetAction().GetInvocationList()) {
			yield return StartCoroutine(action());
		}
		actionRunning = false;
	}

	protected void ChangeState(string state) {
		for(int i = 0; i < states.Length; i++) {
			if(states[i].stateName == state) {
				currentState = states[i];
				break;
			}
		}
	}


    //ACTIONS FOR ALL THE BOSSES

    protected void FlipBoss()
    {
        if (!GetFacingRight() && player.transform.position.x > m_transform.position.x)
            Flip();
        else if (GetFacingRight() && player.transform.position.x < m_transform.position.x)
            Flip();
        Move(0);
    }

    protected IEnumerator Suicide()
    {
        Debug.Log("The boss had killed itself in the dressing room.");
        Application.LoadLevel("Cutscene1");
        yield return null;
    }
    protected IEnumerator JumpAction()
    {
        if (m_transform.position.y < maxY)
        {
            lastAction = JumpAction;
            Jump();
            yield return new WaitForSeconds(waitAfterAction);
        }
        else
            yield return null;
    }
    protected IEnumerator ShootAction()
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

    protected IEnumerator ShootRapidlyAction()
    {
        lastAction = ShootRapidlyAction;
        SetTarget(player);
        timer = 0;
        while (timer < shootingTime)
        {
            timer += Time.deltaTime;
            ShootPrimaryWeapon();
            yield return new WaitForSeconds(bulletPerSecond);
        }
        yield return new WaitForSeconds(waitAfterAction);
    }
    protected IEnumerator DropAction()
    {
        if (m_transform.position.y > minY)
        {
            lastAction = DropAction;
            Drop();
            yield return new WaitForSeconds(waitAfterAction);
        }
        else
            yield return null;
    }
    protected IEnumerator JumpSideWays()
    {
        if (GetGrounded() && lastAction != JumpSideWays)
        {
            lastAction = JumpSideWays;
            print("Jump");
            float move = 0;
            if (Mathf.Abs(minX - transform.position.x) < Mathf.Abs(maxX - transform.position.x))
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
            while (!GetGrounded())
            {
                Move(move);
                yield return null;
            }
            yield return StartCoroutine(MoveToPoint(platformPosition.x));
            yield return new WaitForSeconds(waitAfterAction);
        }
    }
    protected IEnumerator MoveToPoint(float pointX)
    { 
        float moving = 0;
        if (pointX > transform.position.x)
            moving = 1f;
        else if (pointX < transform.position.x)
            moving = -1f;
        while (Mathf.Abs(transform.position.x - pointX) > 0.2f)
        {
            Move(moving);
            yield return null;
        }
        print("Got to a point");
    }
}