using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeComplete : MonoBehaviour 
{
	private Text timeCompleteText;
	public GameState gameState;

	// Use this for initialization
	void Start () 
	{
		timeCompleteText = GetComponent<Text> ();
		timeCompleteText.text = gameState.TimeComplete;	
	}
}
