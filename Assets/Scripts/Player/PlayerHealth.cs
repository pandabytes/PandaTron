using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour 
{
	/// <summary>
	/// Full health
	/// </summary>
	private static float fullHealth;

	/// <summary>
	/// The current health.
	/// </summary>
	private static float currentHealth;

	/// <summary>
	/// Indicate whether the player is damaged.
	/// </summary>
	private static bool isDamaged;

	/// <summary>
	/// The color of the flash.
	/// </summary>
	private static Color flashColor;

	/// <summary>
	/// Reference to the Health Bar object
	/// </summary>
	public GameObject healthBar;

	/// <summary>
	/// Flash this image when damage.
	/// </summary>
	public Image damageImage;

	/// <summary>
	/// The state of the game.
	/// </summary>
	public GameState gameState;

	/// <summary>
	/// The time text.
	/// </summary>
	public Text timeText;

	/// <summary>
	/// The stage number.
	/// </summary>
	public StageEnum stage;

	/// <summary>
	/// Start this instance.
	/// </summary>
	void Awake () 
	{
		fullHealth = 100.0f;
		currentHealth = fullHealth;

		isDamaged = false;
		damageImage.color = Color.clear;
		damageImage.gameObject.SetActive (true);
		flashColor = new Color (1.0f, 0.0f, 0.0f, 0.1f);
	}

	/// <summary>
	/// Update this instance.
	/// </summary>
	void Update()
	{
		// Load GameOver scene when health == 0
		if (currentHealth <= 0)
		{
			gameState.TimeComplete = string.Format(Constants.TimeCompleteFormat, timeText.text);
			gameState.IsGameOver = true;
			gameState.Stage = stage;
			SceneManager.LoadScene ("GameOver");
			return;
		}

		if (isDamaged)
		{
			damageImage.color = flashColor;
		}
		else
		{
			damageImage.color = Color.clear;
		}
		isDamaged = false;
	}
	
	/// <summary>
	/// Sets the health.
	/// </summary>
	/// <param name="scaledDamage">Scaled damage.</param>
	private void SetHealth(float scaledDamage)
	{
		float y = healthBar.transform.localScale.y;
		float z = healthBar.transform.localScale.z;
		healthBar.transform.localScale = new Vector3 (scaledDamage, y, z);	
	}

	/// <summary>
	/// Receives the damage.
	/// </summary>
	/// <param name="damage">Damage amount.</param>
	public void ReceiveDamage(float damage)
	{
		isDamaged = true;
		if (currentHealth <= 0)
		{
			return;
		}

		currentHealth -= damage;
		float scaledDamage = currentHealth / fullHealth;
		SetHealth (scaledDamage);
	}
}