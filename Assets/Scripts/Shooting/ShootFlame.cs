using UnityEngine;
using DigitalRuby.PyroParticles;
using System.Collections;

public class ShootFlame : MonoBehaviour
{
	/// <summary>
	/// The full energy amount.
	/// </summary>
	private float fullEnergy;

	/// <summary>
	/// The current energy amount.
	/// </summary>
	private float currentEnergy;

	/// <summary>
	/// The timer for restoring energy.
	/// </summary>
	private float timer;

	/// <summary>
	/// The time to restore energy.
	/// </summary>
	private float timeToRestore;

	/// <summary>
	/// Indicate if player has used the flamethrower
	/// </summary>
	private bool isFired;

	/// <summary>
	/// Gets or sets the current energy.
	/// </summary>
	/// <value>The current energy.</value>
	public float CurrentEnergy 
	{
		get { return currentEnergy; }
		set { currentEnergy = value; }
	}

	/// <summary>
	/// The player energy.
	/// </summary>
	public GameObject playerEnergy;

	private GameObject flamethrowerObject;
	private FireBaseScript flamethrowerScript;
	private GameObject flamethrowerPrefab;

	// Use this for initialization
	void Start ()
	{
		fullEnergy = 100.0f;
		currentEnergy = fullEnergy;
		timer = 0.0f;
		timeToRestore = 2.0f;
		isFired = false;
		flamethrowerPrefab = Resources.Load ("Flamethrower") as GameObject;
	}
	
	// Update is called once per frame
	void Update ()
	{
		isFired = false;
		if (Input.GetButton ("Fire1"))
		{
			StopFiring ();
			BeginFiring ();
			DepleteEnergy ();
			isFired = true;
		}

		// Restore energy when the time has been 2 seconds and 
		// player has not fired any energy balls and current energy is less than total energy
		if ((int)timer >= timeToRestore && !isFired && currentEnergy < fullEnergy)
		{
			RestoreEnergy ();
		}
		else if (isFired)
		{
			timer = 0.0f;
			isFired = false;
		}

		timer += Time.deltaTime;
	}

	/// <summary>
	/// Begin the flamethrower.
	/// </summary>
	private void BeginFiring()
	{
		if (currentEnergy <= 0.0f)
			return;
		
		Vector3 pos;
		float yRot = transform.rotation.eulerAngles.y;
		Vector3 forwardY = Quaternion.Euler(0.0f, yRot, 0.0f) * Vector3.forward;
		Vector3 forward = transform.forward;
		Vector3 right = transform.right;
		Vector3 up = transform.up;
		Quaternion rotation = Quaternion.identity;

		flamethrowerObject = GameObject.Instantiate(flamethrowerPrefab);
		flamethrowerScript = flamethrowerObject.GetComponent<FireBaseScript>();

		// temporary effect, like a fireball
		if (flamethrowerScript.IsProjectile)
		{
			rotation = transform.rotation;
			pos = transform.position;
		}
		else
		{
			pos = transform.position + (forwardY * 10.0f);
		}

		FireProjectileScript projectileScript = flamethrowerObject.GetComponentInChildren<FireProjectileScript>();
		if (projectileScript != null)
		{
			// make sure we don't collide with other friendly layers
			projectileScript.ProjectileCollisionLayers &= (~UnityEngine.LayerMask.NameToLayer("FriendlyLayer"));
		}

		flamethrowerObject.transform.position = pos;
		flamethrowerObject.transform.rotation = rotation;
	}

	/// <summary>
	/// Stops the flame.
	/// </summary>
	private void StopFiring()
	{
		// if we are running a constant effect like wall of fire, stop it now
		if (flamethrowerScript != null && flamethrowerScript.Duration > 10000)
		{
			flamethrowerScript.Stop();
		}
		flamethrowerObject = null;
		flamethrowerScript = null;
	}

	/// <summary>
	/// Depletes the energy.
	/// </summary>
	private void DepleteEnergy()
	{
		if (currentEnergy <= 0.0f)
			return;

		currentEnergy -= Constants.FlameDepletion;
		float scaledEnergy = currentEnergy / fullEnergy;
		SetEnergy (scaledEnergy);
	}

	/// <summary>
	/// Restores the energy.
	/// </summary>
	private void RestoreEnergy()
	{
		currentEnergy += Constants.FlameDepletion;
		float scaledEnergy = currentEnergy / fullEnergy;
		SetEnergy (scaledEnergy);
	}

	/// <summary>
	/// Sets the energy.
	/// </summary>
	private void SetEnergy(float scaledDepletion)
	{
		float y = playerEnergy.transform.localScale.y;
		float z = playerEnergy.transform.localScale.z;
		playerEnergy.transform.localScale = new Vector3 (scaledDepletion, y, z);
	}
}

