using UnityEngine;
using System.Collections;
using System;

public class PlayerManager : Character
{
    #region MEMBERS
    private GUIManager m_guiManager;
	private static int lives = 3;
	private PlayerState p_state;
	public GUIStyle style;

    private Rect healthBar = new Rect(50, 30, 300, 40);
	private Rect scoreArea = new Rect(1800, 30, 100, 50);
	private Rect laserArea = new Rect(1800, 1000, 100, 50);
	private Rect livesArea = new Rect(350, 30, 100, 40);
	private static int _score = 0;
	public static int score {
		get {
			return _score;
		}
		set {
			if(value > 200) {
				_score = 0;
				lives++;
			} else {
				_score = value;
			}
		}
	}
	private HealthBar hpBar;
    #endregion

    #region UNITY_METHODS
    protected override void Awake () 
    {
		base.Awake();
		p_state = PlayerState.Normal;
        m_guiManager = GetComponent<GUIManager>();
        if (m_guiManager == null)
        {
            m_guiManager = gameObject.AddComponent<GUIManager>();
        }
        if (m_guiManager == null) 
        {
            Debug.LogError("Component did not load properly in PlayerManager");
        }
		health = GetComponent<Health>();
		if (health == null)
		{
			health = gameObject.AddComponent<Health>();
		}
		weapon = GetComponent<Weapon>();
		if (weapon == null)
		{
			weapon = gameObject.AddComponent<Weapon>();
		}

        InitHealthDisplay();

		GUIManager.OnDraw += OnDrawLaserAmmo;
		GUIManager.OnDraw += OnDrawScore;
		GUIManager.OnDraw += OnDrawLives;

		HardReset();
	}

	void HandlePickup(PickupScript pickup) {
		switch(pickup.type) {
		case PickupType.health:
			health.RestoreAmount(pickup.amount);
			break;
		case PickupType.score:
			score += pickup.amount;
			break;
		case PickupType.laser:
			weapon.laserAmmo += pickup.amount;
			break;
		}
	}

	void OnTriggerEnter2D(Collider2D other)
	{
        if (gameObject.activeSelf)
        {
            if (other.tag == "Pickup")
            {
                HandlePickup(other.gameObject.GetComponent<PickupScript>());
            }

            if (other.tag == "Enemy" && GameManager.instance.GetState() == State.Running)
            {
                EnemyManager em = null;
                em = other.GetComponent<EnemyManager>();
                if (em != null)
                    HitEnemy(em, other.transform.position);

            }
            if (other.tag == "Ground" && GetPlayerState() == PlayerState.Hurt)
                SetPlayerState(PlayerState.Normal);
        }
	}
	void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.tag == "Ground" && GetPlayerState () != PlayerState.Normal)
			SetPlayerState (PlayerState.Normal);
	}
    #endregion

    #region METHODS

    void InitHealthDisplay()
    {        
		hpBar = new HealthBar(health, healthBar);
    }

	void OnDrawLives() {
		GUI.Box(livesArea, lives.ToString(), style); 
	}

	void OnDrawScore() {
		GUI.Box (scoreArea, score.ToString(), style);
	}

	void OnDrawLaserAmmo() {
		float ammo = weapon.curLaserAmmo < 0 ? 0 : weapon.curLaserAmmo;
		GUI.Box (laserArea, ammo.ToString("#.00"), style);
	}

    void OnDestroy()
    {
		GUIManager.OnDraw -= OnDrawScore;
    }

	public void SaveState() {
		SaveScript.save.hp = health.f_currentHP;
		SaveScript.save.laser = weapon.curLaserAmmo;
		SaveScript.save.score = score;
	}

	public override void Die()
    {
        if (GetPlayerState() == PlayerState.Dead) return;
        SetPlayerState(PlayerState.Dead);
		transform.parent = null;
        StartCoroutine(DyingAnimation());
		
    }
    private IEnumerator DyingAnimation()
    {
        Camera.main.GetComponent<CameraController>().speed = 0;
        Vector3 direction = new Vector3(UnityEngine.Random.Range(-.5f, .5f), 1f);
        direction = direction.normalized;
        collider2D.enabled = false;

        float gravity = rigidbody2D.gravityScale;
        rigidbody2D.velocity = Vector2.zero;
        rigidbody2D.gravityScale = 0;

        yield return new WaitForSeconds(1f);

        rigidbody2D.gravityScale = gravity;
        rigidbody2D.AddForce(direction * 9000f);
        yield return new WaitForSeconds(3f);
        collider2D.enabled = true;

        SetPlayerState(PlayerState.Normal);
        if (--lives >= 0)
        {
            
            GameObject.Find("LevelManager").GetComponent<LevelManager>().GameOver(true);
            //Application.LoadLevel(Application.loadedLevel);
        }
        else
        {
            gameObject.SetActive(false);
            HardReset();
            GameObject.Find("LevelManager").GetComponent<LevelManager>().GameOver(false);
            //GameManager.instance.SetState(State.StartMenu);
            //Application.LoadLevel("MechList");
        }
    }

	public void HitEnemy(EnemyManager em, Vector3 position)
	{
		RaycastHit2D hit;
		Vector2 force = Vector2.zero;
		bool isHit = false;
		float forceStrength = 500f;
		Vector3 scale = new Vector3(m_transform.localScale.x * .5f, m_transform.localScale.y * 0.25f, 0);
		Debug.DrawRay (m_transform.position + scale, m_transform.right * Mathf.Sign(m_transform.localScale.x));
		hit = Physics2D.Raycast(m_transform.position + scale, m_transform.right, scale.x * 2f, 1 << LayerMask.NameToLayer("Enemy"));
		//hit2 = Physics2D.Raycast(m_transform.position, m_transform.up, 3, 1 << LayerMask.NameToLayer("Enemy"));
		for (int i = 0; i < 3; i++) 
		{
			if(hit)
				break;
			Vector3 pos = new Vector3(m_transform.position.x - m_transform.localScale.x * .5f + m_transform.localScale.x * i * 0.5f, m_transform.position.y, m_transform.position.z);
			hit = Physics2D.Raycast(pos, -m_transform.up, 3, 1 << LayerMask.NameToLayer("Enemy"));
			if (hit.normal == Vector2.up) 
			{
				force = Vector2.up;
				em.Stomped ();
				isHit = true;
				break;
			}
			hit = Physics2D.Raycast(pos, m_transform.up, 3, 1 << LayerMask.NameToLayer("Enemy"));
			if(hit.normal == -Vector2.up)
			{
				rigidbody2D.velocity = Vector2.zero;
				force = Vector2.zero;
				isHit = true;
				break;
			}
		}
		if(!isHit)
		{
			force = new Vector2(Mathf.Sign(m_transform.position.x - position.x), 1.5f);
			force.Normalize();
			forceStrength = 1000f;
			SetPlayerState(PlayerState.Hurt);
		}
		rigidbody2D.velocity = Vector2.zero;
		rigidbody2D.AddForce( force * forceStrength);
	}

    public void Pause()
    {
        if (GameManager.instance.GetState() == State.Running)
        {
            GameManager.instance.SetState(State.PauseMenu);
			Time.timeScale = 0f;
		}
		else{
            GameManager.instance.SetState(State.Running);
			Time.timeScale = 1f;
		}
	}

	public void SetPlayerState(PlayerState state)
	{
		p_state = state;
	}

	public PlayerState GetPlayerState()
	{
		return p_state;
	}

	public void HardReset() {
		base.Reset();
        p_state = PlayerState.Normal;
		weapon.curLaserAmmo = weapon.laserAmmo;
		health.RestoreHP();
		lives = 3;
		score = 0;
		SaveState();
	}

	public override void Reset() {
		base.Reset();
        
        p_state = PlayerState.Normal;
		weapon.curLaserAmmo = SaveScript.save.laser;
		health.f_currentHP = SaveScript.save.hp;
		score = SaveScript.save.score;
        if (!movement.facingRight) movement.Flip();
	}

    #endregion
}
[Flags]
public enum PlayerState
{
	Hurt,
	Normal,
    Dead
}