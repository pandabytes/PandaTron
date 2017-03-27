using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ShootArrow : MonoBehaviour 
{
	/// <summary>
	/// The arrow prefab.
	/// </summary>
	private GameObject arrowPrefab;

	/// <summary>
	/// Start this instance.
	/// </summary>
	void Start () 
	{
		arrowPrefab = Resources.Load ("Arrow") as GameObject;
		InvokeRepeating ("FireArrows", 0.0f, 7.0f);
	}

	/// <summary>
	/// Fires the arrows.
	/// </summary>
	private void FireArrows()
	{
		// Create new arrows and shoot them
		GameObject newArrow = Instantiate (arrowPrefab) as GameObject;
//		Vector3 arrowPosition;
//
//		if (transform.forward.z < 0)
//		{
//			arrowPosition = new Vector3 (transform.position.x - 0.7984f, transform.position.y + 0.411f, transform.position.z - 1.115f);
//		}
//		else
//		{
//			arrowPosition = new Vector3 (transform.position.x + 0.7984f, transform.position.y + 0.411f, transform.position.z - 1.115f);
//		}
//
//		if (transform.right.z < 0)
//		{
//			arrowPosition.z += 1.115f;
//		}


		// Assign position and rotation to arrows
		newArrow.transform.position = transform.position;
		Vector3 conePosition = newArrow.transform.GetChild (0).position;
		Vector3 cubePosition = newArrow.transform.GetChild (1).position;
		newArrow.transform.GetChild (0).position = cubePosition;
		newArrow.transform.GetChild (0).rotation = transform.rotation;
		newArrow.transform.GetChild (1).position = conePosition;

		Debug.Log (newArrow.transform.GetChild (0).gameObject.name + " " + newArrow.transform.GetChild (1).gameObject.name);

		// Add velocity to arrows
		Rigidbody rb = newArrow.GetComponent<Rigidbody> ();
		rb.velocity = newArrow.transform.forward * 15f;
	}
}
