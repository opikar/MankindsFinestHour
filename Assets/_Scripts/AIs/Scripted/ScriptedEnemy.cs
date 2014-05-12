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
    public float collisionDamage = 20f;
    public float startRageHealth = 0.5f;

    private float maxY, minY, minX, maxX;
    private bool hitGround;
    protected Transform player;
    protected bool rage;
    protected float timer;
    protected Action lastAction;
    private Vector3 platformPosition, platformScale;
	private HealthBar hpBar;

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
        minX = top.position.x;
        maxX = down.position.x;
        maxY = top.position.y;
        minY = down.position.y;

        bulletPerSecond = 1 / bulletPerSecond;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();


        FlipBoss();

		InitializeHpBar();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            PlayerManager pl = other.GetComponent<PlayerManager>();
            if (pl != null)
                pl.ApplyDamage(collisionDamage);
        }

        if (m_transform.position.y < other.transform.position.y || other.tag != "Ground") return;
        platformPosition = other.transform.position;
        platformScale = other.transform.localScale;
        hitGround = true;
    }

   

	void InitializeHpBar() {
		Rect location = new Rect(0, 1070, 1920, 10);
		hpBar = new HealthBar(health, location);
	}

	// Update is called once per frame
	protected virtual void Update () {
        if (GameManager.instance.GetState() != State.Running) return;
		if(!actionRunning) {
			StartCoroutine(RunAction ());
		}
        if (health.f_currentHP < GetMaxHealth() * startRageHealth)
            Rage();
        FlipBoss();
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

    public override void Die()
    {
		GameManager.instance.SetState(State.Postgame);
		GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManager>().HardReset();
        GameObject.Find("LevelManager").GetComponent<LevelManager>().CompleteLevel();
    }

    protected virtual void Rage()
    {
        rage = true;
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
    protected virtual IEnumerator ShootAction()
    {
        if (GetGrounded())
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
    }

    protected virtual IEnumerator ShootBigBullet()
    {
        if (!GetGrounded()) yield break;
        lastAction = ShootBigBullet;
        SwapBullet(1);
        yield return StartCoroutine(ShootOnce());
        SwapBullet(0);
    }

    protected virtual IEnumerator ShootOnce()
    {
        if (GetGrounded())
        {
            lastAction = ShootOnce;
            SetTarget(player);
            ShootPrimaryWeapon();
            yield return new WaitForSeconds(waitAfterAction);
        }
    }

    protected virtual IEnumerator ShootRapidlyAction()
    {
        if (GetGrounded())
        {
            lastAction = ShootRapidlyAction;
            SetTarget(player);
            timer = 0;
            while (timer < shootingTime)
            {
                print(timer);
                timer += bulletPerSecond;
                ShootPrimaryWeapon();
                yield return new WaitForSeconds(bulletPerSecond);
            }
            yield return new WaitForSeconds(waitAfterAction);
        }
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
        }
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