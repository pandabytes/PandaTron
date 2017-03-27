using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchCamera : MonoBehaviour 
{
	public Camera camera1;
	public Camera camera2;
	public Camera camera3;

	private int nextCamera;

	// Use this for initialization
	void Start () 
	{
		nextCamera = 2;
	}

	/// <summary>
	/// Late update.
	/// </summary>
	void LateUpdate()
	{
		if (Input.GetMouseButtonDown (2))
		{
			if (nextCamera == 1)
			{
				camera1.gameObject.GetComponent<AudioListener> ().enabled = true;
				camera2.gameObject.GetComponent<AudioListener> ().enabled = false;
				camera3.gameObject.GetComponent<AudioListener> ().enabled = false;
				camera1.enabled = true;
				camera2.enabled = false;
				camera3.enabled = false;
			}
			else if (nextCamera == 2)
			{
				camera1.gameObject.GetComponent<AudioListener> ().enabled = false;
				camera2.gameObject.GetComponent<AudioListener> ().enabled = true;
				camera3.gameObject.GetComponent<AudioListener> ().enabled = false;
				camera1.enabled = false;
				camera2.enabled = true;
				camera3.enabled = false;
			}
			else
			{
				camera1.gameObject.GetComponent<AudioListener> ().enabled = false;
				camera2.gameObject.GetComponent<AudioListener> ().enabled = false;
				camera3.gameObject.GetComponent<AudioListener> ().enabled = true;
				camera1.enabled = false;
				camera2.enabled = false;
				camera3.enabled = true;
				nextCamera = 1;
				return;
			}
			nextCamera++;
		}
	}
}
