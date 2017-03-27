using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectAnimation : MonoBehaviour 
{	
	/// <summary>
	/// Rotate the key around the y-axis
	/// </summary>
	void FixedUpdate () 
	{
		transform.Rotate (new Vector3 (0.0f, 5.0f, 0.0f));
	}
}
