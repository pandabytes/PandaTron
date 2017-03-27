using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour 
{
	/// <summary>
	/// The timer used for when to display the ice block message.
	/// </summary>
	float timer = 0.0f;

	/// <summary>
	/// Horizontal input
	/// </summary>
	private float moveHorizontal;

	/// <summary>
	/// Vertical input
	/// </summary>
	private float moveVertical;

	/// <summary>
	/// The jump height above the ground.
	/// </summary>
	private float jumpHeight;

	/// <summary>
	/// True if jump ability is now enabled.
	/// </summary>
	private bool jumpAcquired;

	/// <summary>
	/// Check whether player is in the air.
	/// </summary>
	private bool isInAir;

	/// <summary>
	/// Vector representing the player's movment
	/// </summary>
	private Vector3 movement;

	/// <summary>
	/// The player rigid body.
	/// </summary>
	private Rigidbody playerRigidbody;

	/// <summary>
	/// The key holder.
	/// </summary>
	private KeyHolder keyHolder;

	/// <summary>
	/// The state of the game.
	/// </summary>
	public GameState gameState;

	/// <summary>
	/// The timer text.
	/// </summary>
	public Text timeText;

	/// <summary>
	/// Check if in beast mode
	/// </summary>
	public bool inBeastMode;

	/// <summary>
	/// The stage.
	/// </summary>
	public StageEnum stage;

	/// <summary>
	/// Player speed.
	/// </summary>
	public float speed;

	/// <summary>
	/// Start this instance.
	/// </summary>
	void Start () 
	{
		jumpHeight = 450.0f;
		isInAir = false;
		playerRigidbody = GetComponent<Rigidbody> ();

		if (inBeastMode)
		{
			jumpAcquired = false;
		}
		else if (!inBeastMode && stage == StageEnum.FirstStage)
		{
			jumpAcquired = false;
			keyHolder = GetComponent<KeyHolder> ();

			// Register jump key event
			keyHolder.AcquireJumpKey += new EventHandler (AcquireJumpKeyHandler);
		}
		else
		{
			jumpAcquired = true;
		}
	}

	/// <summary>
	/// Call FixedUpdate
	/// </summary>
	private void FixedUpdate () 
	{
		// Load GameOver scene
		if (transform.position.y < 0.0f)
		{
			gameState.TimeComplete = string.Format(Constants.TimeCompleteFormat, timeText.text);
			gameState.IsGameOver = true;
			gameState.Stage = stage;
			SceneManager.LoadScene ("GameOver");
			return;
		}
		
		moveHorizontal = Input.GetAxis("Horizontal");
		moveVertical = Input.GetAxis("Vertical");

		// Jump
		if (Input.GetKeyDown (KeyCode.Space) && !isInAir && jumpAcquired && !inBeastMode)
		{
			playerRigidbody.AddForce (Vector3.up * jumpHeight);
			isInAir = true;
		}

		// Move the player
		movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);
		movement = movement.normalized * speed * Time.deltaTime;
		movement = Camera.main.transform.TransformDirection (movement);
		movement.y = 0.0f;
		playerRigidbody.MovePosition (transform.position + movement);

		// Rotate to face the mouse cursor
		Plane playerPlane = new Plane(Vector3.up, transform.position);
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		float hitDistance = 0.0f;

		if (playerPlane.Raycast (ray, out hitDistance))
		{
			Vector3 targetPoint = ray.GetPoint (hitDistance);
			Quaternion targetRotation = Quaternion.LookRotation (targetPoint - transform.position);
			transform.rotation = Quaternion.Slerp (transform.rotation, targetRotation, 35.0f * Time.deltaTime);
		}

		// Countdown the timer
		if (timer > 0.0f)
			timer -= Time.deltaTime;
	}

	/// <summary>
	/// Raises the collision enter event.
	/// Use to detec if player is not in the air.
	/// </summary>
	/// <param name="other">Other.</param>
	private void OnCollisionEnter(Collision other)
	{
		if (other.gameObject.CompareTag ("Floor"))
		{
			isInAir = false;
		}
		else if (other.gameObject.CompareTag ("Ice Block") && timer <= 0.0f)
		{
			// Publish this event again after it has popped up 3 seconds ago
			timer = 3.0f;
			OnCollideIce (this, EventArgs.Empty);
		}
	}

	/// <summary>
	/// Raises the collision stay event.
	/// </summary>
	/// <param name="other">Other.</param>
	private void OnCollisionStay(Collision other)
	{
		// Move along with the platforms
		if (other.gameObject.CompareTag ("Moving Platform"))
		{
			if (transform.parent == null)
			{
				isInAir = false;
				transform.parent = other.gameObject.transform;
			}
		}
	}

	/// <summary>
	/// Raises the collision exit event.
	/// </summary>
	/// <param name="other">Other.</param>
	private void OnCollisionExit(Collision other)
	{
		if (other.gameObject.CompareTag ("Moving Platform"))
		{
			transform.parent = null;
		}
	}

	/// <summary>
	/// Handle the acquire key event.
	/// Enable the jump mechanic.
	/// </summary>
	/// <param name="sender">Sender.</param>
	/// <param name="e">Event argument.</param>
	private void AcquireJumpKeyHandler(object sender, EventArgs e)
	{
		jumpAcquired = true;
	}

	/// <summary>
	/// Occurs when collide ice.
	/// </summary>
	public event EventHandler CollideIce;

	/// <summary>
	/// Raises the collide ice event.
	/// </summary>
	/// <param name="sender">Sender.</param>
	/// <param name="e">E.</param>
	protected virtual void OnCollideIce(object sender, EventArgs e)
	{
		if (CollideIce != null)
			CollideIce (sender, e);
	}
}
