using System;
using UnityEngine;

public class HealthBar
{
	//assume default resolution for scaling elements
	private Health health;
	private Rect healthBar;
	private Rect currentHealthBar;
	private Texture2D healthTexture;
	private Texture2D barTexture;

	public HealthBar(Health health, Rect location)
	{
		healthTexture = new Texture2D(1, 1);
		healthTexture.SetPixel(0, 0, Color.green);
		barTexture = new Texture2D(1, 1);
		barTexture.SetPixel(0, 0, Color.red);
		healthTexture.Apply();
		barTexture.Apply();

		this.health = health;
		this.healthBar = location;
		this.currentHealthBar = new Rect(location);

		GUIManager.OnDraw += OnDrawHealth;
	}
		
	void OnDrawHealth()
	{
		if(health == null || !health.gameObject.activeSelf)
			return;
		GUI.DrawTexture(healthBar, barTexture);
		currentHealthBar.width = health.f_currentHP / health.fullHP * healthBar.width;
		GUI.DrawTexture(currentHealthBar, healthTexture);
	}

	~HealthBar() {
		GUIManager.OnDraw -= OnDrawHealth;
	}
}


