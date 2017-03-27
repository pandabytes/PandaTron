using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootEnergyBall : MonoBehaviour 
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
	/// Indicate if player has fired an energy ball
	/// </summary>
	private bool isFired;

	/// <summary>
	/// The energy ball prefab.
	/// </summary>
	private GameObject energyBallPrefab;

	/// <summary>
	/// The player energy.
	/// </summary>
	public GameObject playerEnergy;

	/// <summary>
	/// The user interface manger.
	/// </summary>
	public UIManager uiManger;

	/// <summary>
	/// Gets or sets the current energy.
	/// </summary>
	/// <value>The current energy.</value>
	public float CurrentEnergy 
	{
		get { return currentEnergy; }
		set { currentEnergy = value; }
	}

	// Use this for initialization
	void Start () 
	{
		fullEnergy = 100.0f;
		currentEnergy = fullEnergy;
		timer = 0.0f;
		timeToRestore = 0.5f;
		isFired = false;
		energyBallPrefab = Resources.Load ("EnergyBall") as GameObject;	
	}

	/// <summary>
	/// Update is called once per frame
	/// </summary>
	void Update () 
	{
		isFired = false;
		if (Input.GetButtonDown ("Fire1"))
		{
			// Fire and deplete energy
			Fire ();
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
	/// Fire energy balls.
	/// </summary>
	private void Fire()
	{
		if (currentEnergy <= 0.0f || uiManger.IsPopUpWindowEnabled())
			return;

		// Instantiate new energy ball
		GameObject newEnergyBall = Instantiate (energyBallPrefab) as GameObject;
		newEnergyBall.transform.position = transform.position;
		newEnergyBall.transform.rotation = transform.rotation;

		// Add velocity
		Rigidbody rb = newEnergyBall.GetComponent<Rigidbody> ();
		rb.velocity = newEnergyBall.transform.forward * 15f;
	}

	/// <summary>
	/// Depletes the energy.
	/// </summary>
	private void DepleteEnergy()
	{
		if (currentEnergy <= 0.0f)
			return;
		
		currentEnergy -= Constants.EnergyDepletion;
		float scaledEnergy = currentEnergy / fullEnergy;
		SetEnergy (scaledEnergy);
	}

	/// <summary>
	/// Restores the energy.
	/// </summary>
	private void RestoreEnergy()
	{
		currentEnergy += Constants.EnergyDepletion;
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
