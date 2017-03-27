using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicSingleton : MonoBehaviour 
{
	/// <summary>
	/// The music singleton.
	/// </summary>
	private static MusicSingleton musicSingleton = null;

	/// <summary>
	/// Gets the music.
	/// </summary>
	/// <value>The music.</value>
	public static MusicSingleton Music 
	{
		get { return musicSingleton; }
	}

	// Use this for initialization
	void Awake () 
	{
		if (musicSingleton != null) 
		{
			Destroy (this.gameObject);
			return;
		}
		else 
		{
			musicSingleton = this;
		}

		DontDestroyOnLoad (this.gameObject);
	}
}
