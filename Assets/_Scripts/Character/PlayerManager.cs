using UnityEngine;
using System.Collections;

public class PlayerManager : Character
{
    #region MEMBERS
    private GUIManager m_guiManager;
	private int lives = 3;

    public Rect healthBar = new Rect(40, 25, 200, 30);
    private Rect currentHealthBar;
    private Texture2D healthTexture;
    private Texture2D barTexture;
    #endregion

    #region UNITY_METHODS
    public void Start () 
    {
        m_guiManager = GetComponent<GUIManager>();
        if (m_guiManager == null)
        {
            m_guiManager = gameObject.AddComponent<GUIManager>();
        }
        if (m_guiManager == null) 
        {
            Debug.LogError("Component did not load properly in PlayerManager");
        }

        InitHealthDisplay();
	}
	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Enemy" && gameManager.GetState() == State.Running) 
		{
			RaycastHit2D hit;
			EnemyManager em = null;
			Vector2 force;
			float forceStrength = 500f;
			hit = Physics2D.Raycast(m_transform.position, -m_transform.up, 3, 1 << LayerMask.NameToLayer("Enemy"));
			em = other.GetComponent<EnemyManager>();
			if(em != null && hit.normal == Vector2.up)
			{
				force = Vector2.up;
				em.Die();
			}
			else
			{
				force = new Vector2(Mathf.Sign(m_transform.position.x - other.transform.position.x), 1.5f);
				force.Normalize();
				forceStrength = 1000f;
				gameManager.SetState(State.Hurt);
			}
			rigidbody2D.velocity = Vector2.zero;
			rigidbody2D.AddForce( force * forceStrength);
		}
		if (other.tag == "Ground" && gameManager.GetState () == State.Hurt)
			gameManager.SetState (State.Running);
	}
	void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.tag == "Ground" && gameManager.GetState () == State.Hurt)
			gameManager.SetState (State.Running);
	}
    #endregion

    #region METHODS

    void InitHealthDisplay()
    {
        healthTexture = new Texture2D(1, 1);
        healthTexture.SetPixel(0, 0, Color.green);
        barTexture = new Texture2D(1, 1);
        barTexture.SetPixel(0, 0, Color.red);
        healthTexture.Apply();
        barTexture.Apply();
        currentHealthBar = healthBar;

        GUIManager.OnDraw += OnDrawHealth;
    }

    void OnDrawHealth()
    {
        GUI.DrawTexture(healthBar, barTexture);
        currentHealthBar.width = health.f_currentHP / health.fullHP * healthBar.width;
        GUI.DrawTexture(currentHealthBar, healthTexture);
    }

    void OnDestroy()
    {
        GUIManager.OnDraw -= OnDrawHealth;
    }

	public override void Die()
    {
		if (--lives >= 0) { 
			Application.LoadLevel(Application.loadedLevel);
		} else {
			gameObject.SetActive(false);
		}
    }

    public void Pause()
    {
		if(gameManager.GetState() == State.Running){
			gameManager.SetState(State.PauseMenu);
			Time.timeScale = 0f;
		}
		else{
			gameManager.SetState(State.Running);
			Time.timeScale = 1f;
		}
	}
    #endregion
}
