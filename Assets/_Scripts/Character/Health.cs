using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour
{
    #region MEMBERS
    public float fullHP = 20f;
    public float f_currentHP;

    private Character m_character;
    #endregion

    #region UNITY_METHODS
    void Start ()
    {
		m_character = GetComponent<Character>();
		f_currentHP = fullHP;
	}
    #endregion

    #region METHODS
    public void ApplyDamage (float dmg)
	{
		f_currentHP -= dmg;
		if (f_currentHP <= 0f) {
			if(m_character.tag == "Enemy") {
				PlayerManager.score += 10;
			}
			m_character.Die();
		}
    }

	public void RestoreHP() {
		f_currentHP = fullHP;
	}

	public void RestoreAmount(int amount) {
		f_currentHP += amount;
		f_currentHP = f_currentHP > fullHP ? fullHP : f_currentHP;
	}
    #endregion
}
