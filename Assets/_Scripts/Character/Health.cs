using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour
{
    #region MEMBERS
    public float fullHP = 10f;
	
    public float f_currentHP;
	public Rect healthBar = new Rect(40, 25, 200, 30);

	private Rect currentHealthBar;
	private Character m_character;
	private Texture2D healthTexture;
	private Texture2D barTexture;
    #endregion

    #region UNITY_METHODS
    void Start ()
    {
		m_character = GetComponent<Character>();
		f_currentHP = fullHP;

		healthTexture = new Texture2D(1, 1);
		healthTexture.SetPixel(0, 0, Color.green);
		barTexture = new Texture2D(1, 1);
		barTexture.SetPixel(0, 0, Color.red);
		healthTexture.Apply();
		barTexture.Apply();
		currentHealthBar = healthBar;
	}

	void OnGUI()
	{
		GUI.DrawTexture(healthBar, barTexture);
		currentHealthBar.width = f_currentHP/fullHP*healthBar.width;
		GUI.DrawTexture(currentHealthBar, healthTexture);

	}
    #endregion

    #region METHODS
    public void ApplyDamage (float dmg)
	{
		f_currentHP -= dmg;
		if(f_currentHP <= 0f)
			m_character.Die();
    }

	public void RestoreHP() {
		f_currentHP = fullHP;
	}
    #endregion
}
