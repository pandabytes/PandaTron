using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour 
{
	/// <summary>
	/// The target tag.
	/// </summary>
	public string TargetTag;

	// Use this for initialization
	void Start () 
	{
		Invoke ("RemoveProjectile", 5.0f);
	}

	/// <summary>
	/// Removes the arrow from the scene.
	/// </summary>
	private void RemoveProjectile()
	{
		Destroy (this.gameObject);
	}

	/// <summary>
	/// Raises the trigger enter event.
	/// </summary>
	/// <param name="other">Other.</param>
	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag(TargetTag))
		{
			if (TargetTag == "Enemy")
			{
				other.gameObject.GetComponent<EnemyHealth> ().ReceiveDamage (Constants.EnergyBallDamage);
				Destroy (this.gameObject);
			}
			else if (TargetTag == "Player" && other.gameObject.CompareTag("Player") && gameObject.tag == "Ice")
			{
				other.gameObject.GetComponent<PlayerHealth> ().ReceiveDamage (Constants.IceDamaged);
			}
			else
			{
				other.gameObject.GetComponent<PlayerHealth> ().ReceiveDamage (Constants.ParticleDamaged);
				Destroy (this.gameObject);
			}

		}
		else if (other.gameObject.CompareTag("Shield") && TargetTag == "Player")
		{
			Destroy (this.gameObject);	
		}
	}
}
