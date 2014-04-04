// ------------------------------------------------------------------------------
//  <autogenerated>
//      This code was generated by a tool.
//      Mono Runtime Version: 4.0.30319.1
// 
//      Changes to this file may cause incorrect behavior and will be lost if 
//      the code is regenerated.
//  </autogenerated>
// ------------------------------------------------------------------------------
using System;
using UnityEngine;

public class HealthBar
{
	private Health health;
	private Rect healthBar = new Rect(40, 25, 200, 30);
	private Rect currentHealthBar;
	private Texture2D healthTexture;
	private Texture2D barTexture;

	public HealthBar(Health health, Rect location, Texture2D barTexture, Texture2D healthTexture)
	{
		this.health = health;
		this.healthBar = location;
		this.barTexture = barTexture;
		this.healthTexture = healthTexture;
		currentHealthBar = healthBar;

		GUIManager.OnDraw += OnDrawHealth;
	}
		
	void OnDrawHealth()
	{
		if(!health.gameObject.activeSelf)
			return;
		GUI.DrawTexture(healthBar, barTexture);
		currentHealthBar.width = health.f_currentHP / health.fullHP * healthBar.width;
		GUI.DrawTexture(currentHealthBar, healthTexture);
	}

	~HealthBar() {
		GUIManager.OnDraw -= OnDrawHealth;
	}
}


