using UnityEngine;
using System.Collections;

public class ShootIce : MonoBehaviour
{
	/// <summary>
	/// The ice prefab.
	/// </summary>
	private GameObject icePrefab;

	// Use this for initialization
	void Start ()
	{
		icePrefab = Resources.Load ("Ice") as GameObject;
	}

    /// <summary>
    /// Fire ice blocks
    /// </summary>
    /// <param name="playerPosition">Where the player's currently at</param>
	public void FireIce(Vector3 playerPosition)
	{
		float distance = Vector3.Distance (playerPosition, transform.position);

		GameObject newIce = Instantiate (icePrefab) as GameObject;
		newIce.transform.position = transform.position;
		newIce.transform.rotation = transform.rotation;

		Rigidbody rb = newIce.GetComponent<Rigidbody> ();
		rb.velocity = transform.forward * distance;
	}
}

