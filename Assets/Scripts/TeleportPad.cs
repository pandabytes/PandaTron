using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TeleportPad : MonoBehaviour 
{
	/// <summary>
	/// The destination teleportation.
	/// </summary>
	public GameObject destinationTeleportation;

	/// <summary>
	/// The state of the game.
	/// </summary>
	public GameState gameState;

	/// <summary>
	/// The time text.
	/// </summary>
	public Text timeText;

	/// <summary>
	/// Check if this is the last telportation pad in the game.
	/// </summary>
	public bool isFinalTeleportationPad;

	/// <summary>
	/// The timer for letting the player teleports back
	/// </summary>
	private float timer = 0.0f;

	/// <summary>
	/// Update this instance.
	/// </summary>
	void Update()
	{
		if (timer > 0.0f)
			timer -= Time.deltaTime;
	}

	/// <summary>
	/// Raises the trigger enter event.
	/// </summary>
	/// <param name="other">The collied object.</param>
	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag ("Player") && destinationTeleportation == null && !isFinalTeleportationPad)
		{
			gameState.TimeComplete = string.Format(Constants.TimeCompleteFormat, timeText.text);
			gameState.IsGameOver = false;
			SceneManager.LoadScene ("SecondStage");
			return;	
		}

		// Only teleport the player if the collied object is the player and
		// if the timer is expired
		if (other.gameObject.CompareTag ("Player") && timer <= 0.0f)
		{
			Vector3 newPosition = destinationTeleportation.transform.position;
			newPosition.y += 1;

			other.gameObject.transform.position = newPosition;

			if (isFinalTeleportationPad)
				OnReachFinalDestination (this, EventArgs.Empty);
			else
				destinationTeleportation.GetComponent<TeleportPad> ().timer = 2;
		}
	}

	/// <summary>
	/// Occurs when reach final destination.
	/// </summary>
	public event EventHandler ReachFinalDestination;

	/// <summary>
	/// Raises the reach final destination event.
	/// </summary>
	/// <param name="sender">Sender.</param>
	/// <param name="e">E.</param>
	protected virtual void OnReachFinalDestination(object sender, EventArgs e)
	{
		if (ReachFinalDestination != null)
		{
			ReachFinalDestination (sender, e);
		}
	}

}
