using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootParticle : MonoBehaviour 
{
	/// <summary>
	/// The energy ball prefab.
	/// </summary>
	private GameObject particlePrefab;

	/// <summary>
	/// Start this instance.
	/// </summary>
	private void Start()
	{
		particlePrefab = Resources.Load ("Particle") as GameObject;
		InvokeRepeating ("FireParticles", 0.0f, 7.0f);
	}

	/// <summary>
	/// Fire the particles
	/// </summary>
	private void FireParticles()
	{
		GameObject newParticle = Instantiate (particlePrefab) as GameObject;
		//Quaternion particleRotation = Quaternion.Euler (90.0f, transform.rotation.y, transform.rotation.z);
		newParticle.transform.position = transform.position;
		newParticle.transform.rotation = transform.rotation;


		Rigidbody rb = newParticle.GetComponent<Rigidbody> ();
		rb.velocity = transform.forward * 15.0f;
	}
}

