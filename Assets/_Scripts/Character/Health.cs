using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour
{
    #region MEMBERS
    public float fullHP = 10f;
	
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
		if(f_currentHP <= 0f)
			m_character.Die();
    }
    #endregion
}
