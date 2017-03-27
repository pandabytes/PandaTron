using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeltIce : MonoBehaviour 
{
	public GameObject Text3D;
	private float timeToMelt;
	private bool onFire;

	/// <summary>
	/// Start this instance.
	/// </summary>
	void Start () 
	{
		timeToMelt = 0.0f;	
	}

	/// <summary>
	/// Update this instance.
	/// </summary>
	void Update()
	{
		if ((int)timeToMelt == 1)
		{
			Text3D.SetActive (true);
			this.gameObject.SetActive (false);
			return;
		}
			
		if (!onFire)
		{
			timeToMelt = 0.0f;
		}
		else
		{
			timeToMelt += Time.deltaTime;
			onFire = false;
		}
	}

	/// <summary>
	/// Raises the trigger enter event.
	/// </summary>
	/// <param name="other">Other.</param>
	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag("Fire"))
			onFire = true;
	}
}
