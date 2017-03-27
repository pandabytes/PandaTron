using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerShield : MonoBehaviour 
{
	/// <summary>
	/// The shield full health.
	/// </summary>
	private float shieldFullHealth;

	/// <summary>
	/// The current shield health.
	/// </summary>
	private float currentShieldHealth;

	/// <summary>
	/// Check if shield has been acquired.
	/// </summary>
	private bool shieldAcquired;

	/// <summary>
	/// The key holder.
	/// </summary>
	private KeyHolder keyHolder;

	/// <summary>
	/// The shield.
	/// </summary>
	public GameObject shield;

	/// <summary>
	/// The shield bar.
	/// </summary>
	public GameObject shieldBar;

	/// <summary>
	/// Check if in beast mode.
	/// </summary>
	public bool inBeastMode;

	/// <summary>
	/// The stage.
	/// </summary>
	public StageEnum stage;

	/// <summary>
	/// Gets a value indicating whether this instance is active.
	/// </summary>
	/// <value><c>true</c> if this instance is active; otherwise, <c>false</c>.</value>
	public bool IsActive 
	{
		get { return shield.activeSelf; }
	}

	// Use this for initialization
	void Start () 
	{
		shieldFullHealth = 100;
		currentShieldHealth = shieldFullHealth;

		if (!inBeastMode && stage == StageEnum.FirstStage)
		{
			keyHolder = GetComponent<KeyHolder> ();
			keyHolder.AcquireShieldKey += new EventHandler (AcquireShieldKeyHandler);
		}
		else if (!inBeastMode && stage == StageEnum.SecondStage)
		{
			shieldAcquired = true;
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		// Do nothing if shield has not been acquired yet
		if (!shieldAcquired)
			return;
		
		if (Input.GetMouseButtonDown(1) && !inBeastMode)
		{
			// Activate and deactivate shield
			if (shield.activeSelf)
			{
				DeactivateShield ();
			}
			else
			{
				ActivateShield ();
			}
		}

		// Deplete and restore shield health
		if (shield.activeSelf)
		{
			DepleteShieldHealth ();
		}
		else
		{
			RestoreShieldHealth ();
		}
	}

	/// <summary>
	/// Activates the shield.
	/// </summary>
	private void ActivateShield()
	{
		shield.SetActive (true);
	}

	/// <summary>
	/// Deactivates the shield.
	/// </summary>
	private void DeactivateShield()
	{
		shield.SetActive (false);
	}

	/// <summary>
	/// Depletes the shield health.
	/// </summary>
	private void DepleteShieldHealth()
	{
		if (currentShieldHealth > 0)
		{
			currentShieldHealth -= Constants.ShieldDepletion;
			float scaledDepletion = currentShieldHealth / shieldFullHealth;
			SetShieldHealth (scaledDepletion);
		}
		else
		{
			shield.SetActive (false);
		}
	}

	/// <summary>
	/// Restores the shield health.
	/// </summary>
	private void RestoreShieldHealth()
	{
		if (currentShieldHealth < shieldFullHealth)
		{
			currentShieldHealth += Constants.ShieldRestoration;
			float scaledHealth = currentShieldHealth / shieldFullHealth;
			SetShieldHealth (scaledHealth);
		}
	}

	/// <summary>
	/// Sets the shield health.
	/// </summary>
	private void SetShieldHealth(float scaledDepletion)
	{
		float y = shieldBar.transform.localScale.y;
		float z = shieldBar.transform.localScale.z;
		shieldBar.transform.localScale = new Vector3 (scaledDepletion, y, z);	
	}

	/// <summary>
	/// Handle the acquire key event.
	/// Enable shield.
	/// </summary>
	/// <param name="sender">Sender.</param>
	/// <param name="e">E.</param>
	private void AcquireShieldKeyHandler(object sender, EventArgs e)
	{
		shieldAcquired = true;
	}
}
