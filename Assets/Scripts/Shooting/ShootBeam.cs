using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootBeam : MonoBehaviour 
{
	/// <summary>
	/// The energy ball prefab.
	/// </summary>
	private GameObject beamPrefab;

	/// <summary>
	/// Start this instance.
	/// </summary>
	private void Start()
	{
		beamPrefab = Resources.Load ("Beam") as GameObject;
		InvokeRepeating ("Firebeams", 0.0f, 3.0f);
	}

	/// <summary>
	/// Fire the beams
	/// </summary>
	private void Firebeams()
	{
		GameObject newbeam = Instantiate (beamPrefab) as GameObject;
		//Quaternion beamRotation = Quaternion.Euler (90.0f, transform.rotation.y, transform.rotation.z);
		newbeam.transform.position = transform.position;
		newbeam.transform.rotation = transform.rotation;


		Rigidbody rb = newbeam.GetComponent<Rigidbody> ();
		rb.velocity = transform.forward * 15.0f;
	}
}

