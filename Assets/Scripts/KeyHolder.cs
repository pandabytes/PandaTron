using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyHolder : MonoBehaviour 
{
	public Text keyCountText;
	public StageEnum stage;
	private int keysAcquired;
	private const int maxNumberKeys = 2;
	private List<GameObject> keysList;

	public bool AcquiredAllKeys
	{
		get { return keysAcquired == maxNumberKeys; }
	}

	// Use this for initialization
	void Start () 
	{
		keysAcquired = 0;
		keysList = new List<GameObject> ();
		SetKeyCountText ();
	}

	/// <summary>
	/// Raises the trigger enter event.
	/// Acquire the key when touch.
	/// </summary>
	/// <param name="other">The collied object.</param>
	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag ("Key"))
		{
			// Set key to inactive and store it in the list
			GameObject key = other.gameObject;
			key.SetActive (false);

			keysAcquired++;
			keysList.Add (key);
			SetKeyCountText ();

			// Raise the acquire key event
			OnAcquireKey (key, EventArgs.Empty);
		}
	}
		
	/// <summary>
	/// Sets the key count text.
	/// </summary>
	private void SetKeyCountText()
	{
		if (stage == StageEnum.FirstStage)
			keyCountText.text = "Key: " + keysAcquired.ToString () + "/" + maxNumberKeys.ToString();
	}

	/// <summary>
	/// Occurs when acquire a key.
	/// </summary>
	public event EventHandler AcquireJumpKey;

	/// <summary>
	/// Occurs when acquire shield key.
	/// </summary>
	public event EventHandler AcquireShieldKey;

	/// <summary>
	/// Occurs when acquire transform key.
	/// </summary>
	public event EventHandler AcquireTransformKey;

	/// <summary>
	/// Raises the acquire key event.
	/// </summary>
	/// <param name="sender">Sender.</param>
	/// <param name="e">E.</param>
	protected virtual void OnAcquireKey(object sender, EventArgs e)
	{
		GameObject key = sender as GameObject;
		if (key.name == Constants.JumpKeyName && AcquireJumpKey != null)
		{
			AcquireJumpKey (sender, e);
		}
		else if (key.name == Constants.ShieldKeyName && AcquireShieldKey != null)
		{
			AcquireShieldKey (sender, e);
		}
		else if (key.name == Constants.TransformKeyName && AcquireTransformKey != null)
		{
			AcquireTransformKey (sender, e);
		}
	}
}
