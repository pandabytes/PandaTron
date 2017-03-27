using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyHealth : MonoBehaviour 
{
	/// <summary>
	/// Full health
	/// </summary>
	private float fullHealth;

	/// <summary>
	/// The current health.
	/// </summary>
	private float currentHealth;

	/// <summary>
	/// Reference to healthBar object
	/// </summary>
	public GameObject healthBar;

	/// <summary>
	/// Is this enemy the final boss?
	/// </summary>
	public bool isBoss;

	/// <summary>
	/// The animation.
	/// </summary>
	private Animation anim;

	/// <summary>
	/// Gets the current health.
	/// </summary>
	/// <value>The current health.</value>
	public float CurrentHealth 
	{
		get { return currentHealth; }
	}

	// Use this for initialization
	void Start () 
	{
		fullHealth = (isBoss) ? 1000.0f : 100.0f;
		currentHealth = fullHealth;
		anim = GetComponent<Animation> ();
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
	/// <param name="damage">Damage.</param>
	public void ReceiveDamage(float damage)
	{
		// Raise the EnemyDead for the EnemyManager class
		if (currentHealth <= 0.0f && !isBoss)
		{
			this.gameObject.SetActive (false);
			OnEnemyDeath (EventArgs.Empty);
			return;
		}

		// The boss simply dies
		if (currentHealth <= 0.0f && isBoss)
		{
			if (anim.isPlaying)
			{
				anim.Stop ();
				anim.Play ("death");
				GetComponent<EnemyMovement> ().enabled = false;
				GetComponent<NavMeshAgent> ().enabled = false;
				GetComponent<EnemyAttacks> ().enabled = false;
			}
			return;
		}

        // Only flame and laser can damage the boss
		if (isBoss && damage == Constants.EnergyBallDamage)
		{
			damage = 0.0f;
		}

		currentHealth -= damage;
		float scaledDamage = currentHealth / fullHealth;
		SetHealth (scaledDamage);
	}

	/// <summary>
	/// Occurs when enemy is dead.
	/// </summary>
	public event EventHandler EnemyDeath;

	/// <summary>
	/// Raises the enemy death event.
	/// </summary>
	/// <param name="e">Event arguments.</param>
	protected virtual void OnEnemyDeath(EventArgs e)
	{
		if (EnemyDeath != null)
		{
			EnemyDeath (this, e);
		}
	}

}
