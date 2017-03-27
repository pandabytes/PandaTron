using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootLaser : MonoBehaviour 
{
	/// <summary>
	/// The LineRenderer component
	/// </summary>
	private LineRenderer line;

	/// <summary>
	/// The laser sound.
	/// </summary>
	private AudioSource laserSound;

	/// <summary>
	/// Start this instance.
	/// </summary>
	void Start () 
	{
		line = gameObject.GetComponent<LineRenderer> ();
		laserSound = gameObject.GetComponent<AudioSource> ();
		line.enabled = false;
	}

	/// <summary>
	/// Update this instance.
	/// </summary>
	void Update () 
	{
		if (Input.GetMouseButton(1))
		{
			StopCoroutine ("FireLaser");
			StartCoroutine ("FireLaser");
		}
	}

	/// <summary>
	/// Fires the laser.
	/// </summary>
	/// <returns>The laser.</returns>
	private IEnumerator FireLaser()
	{
		line.enabled = true;
		RaycastHit hit;

		// Continously fire laser when space key is pressed
		while (Input.GetMouseButton(1))
		{
			if (!laserSound.isPlaying)
				laserSound.Play ();
			// Make the laser spins
			line.GetComponent<Renderer>().material.mainTextureOffset = new Vector2(0, Time.time);

			// Create a ray that starts from the laser tip to a direction it points to
			Ray ray = new Ray (transform.position, transform.forward);
			line.SetPosition (0, ray.origin);

			// Check if the laser hits some object
			if (Physics.Raycast (ray, out hit, 100))
			{
				line.SetPosition (1, hit.point);

				// Add force to the laser if rigidbody is detected
				// Drain HP
				if (hit.rigidbody)
				{
					EnemyHealth enemyHealth = hit.rigidbody.gameObject.GetComponent<EnemyHealth> ();
					if (enemyHealth != null)
						enemyHealth.ReceiveDamage (Constants.LaserDamage);
				}
			}
			else
			{
				line.SetPosition (1, ray.GetPoint (100));
			}

			yield return null;
		}
		laserSound.Stop ();
		line.enabled = false;
	}
}