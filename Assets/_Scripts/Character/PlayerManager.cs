using UnityEngine;
using System.Collections;

public class PlayerManager : Character
{
    #region MEMBERS
    private GUIManager m_guiManager;
	private int lives = 3;
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
	}
	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Enemy") 
		{
			RaycastHit2D hit;
			EnemyManager em = null;
			hit = Physics2D.Raycast(m_transform.position, -m_transform.up, 3, 1 << LayerMask.NameToLayer("Enemy"));
			em = other.GetComponent<EnemyManager>();
			if(em != null && hit.normal == Vector2.up)
				em.Die();
			Vector2 force = new Vector2(Mathf.Sign(m_transform.position.x - other.transform.position.x), .3f);
			force.Normalize();
			float forceStrength = 500f;
			print (force);
			rigidbody2D.velocity = Vector2.zero;
			rigidbody2D.AddForce( force * forceStrength);
		}
	}
    #endregion

    #region METHODS

	public override void Die()
    {
		if (--lives >= 0) { 
			Application.LoadLevel(Application.loadedLevel);
		} else {
			gameObject.SetActive(false);
		}
    }
    #endregion
}
