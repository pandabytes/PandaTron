using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour 
{
	/// <summary>
	/// Reference to the player object
	/// </summary>
	public GameObject player;

	/// <summary>
	/// The beast mode.
	/// </summary>
	public GameObject beastMode;

	/// <summary>
	/// The offset for the camera
	/// </summary>
	private Vector3 offset;

	/// <summary>
	/// Start this instance.
	/// </summary>
	void Start () 
	{
		offset = (player.activeSelf) ? (transform.position - player.transform.position) : (transform.position - beastMode.transform.position);
	}
	
	/// <summary>
	/// Lates the update.
	/// </summary>
	void LateUpdate () 
	{
		if (player.activeSelf)
			transform.position = player.transform.position + offset;
		else if (!player.activeSelf && beastMode != null)
			transform.position = beastMode.transform.position + offset;
	}
}
