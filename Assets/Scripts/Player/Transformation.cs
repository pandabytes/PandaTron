using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

public class Transformation : MonoBehaviour
{
	public GameObject otherForm;
	public GameObject energy;
	public GameObject energyBeastMode;
	public Text shieldText;
	public Image shieldBorder;
	public Image shieldBackground;
	public Image shieldBar;

	public bool InBeastMode;

	private KeyHolder keyHolder;
	private bool transformAcquired;

	// Use this for initialization
	void Start ()
	{
		if (!InBeastMode)
		{
			keyHolder = GetComponent<KeyHolder> ();
			keyHolder.AcquireTransformKey += new EventHandler (AcquireTransformKeyHandlder);
			transformAcquired = false;
		}
		else
		{
			transformAcquired = true;
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (Input.GetKeyDown (KeyCode.Alpha1) && transformAcquired)
		{
			if (!InBeastMode)
			{
				energy.SetActive (false);
				energyBeastMode.SetActive (true);
				shieldText.gameObject.SetActive (false);
				shieldBorder.gameObject.SetActive (false);
				shieldBackground.gameObject.SetActive (false);
				shieldBar.gameObject.SetActive (false);
			}
			else
			{
				energy.SetActive (true);
				energyBeastMode.SetActive (false);
				shieldText.gameObject.SetActive (true);
				shieldBorder.gameObject.SetActive (true);
				shieldBackground.gameObject.SetActive (true);
				shieldBar.gameObject.SetActive (true);
			}
				
			otherForm.SetActive (true);
			otherForm.transform.position = transform.position;
			otherForm.transform.rotation = transform.rotation;
			this.gameObject.SetActive (false);
		}
	}

	/// <summary>
	/// Handle when acquire tranform key.
	/// </summary>
	/// <param name="sender">Sender.</param>
	/// <param name="e">E.</param>
	private void AcquireTransformKeyHandlder(object sender, EventArgs e)
	{
		transformAcquired = true;
	}
}

