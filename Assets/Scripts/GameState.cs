using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameData")]
public class GameState : ScriptableObject 
{
	/// <summary>
	/// The time complete.
	/// </summary>
	public string TimeComplete;

	/// <summary>
	/// Is the game over?
	/// </summary>
	public bool IsGameOver;

	/// <summary>
	/// The stage number.
	/// </summary>
	public StageEnum Stage;
}
