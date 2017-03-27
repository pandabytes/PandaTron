using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Utils : MonoBehaviour
{
	/// <summary>
	/// The state of the game.
	/// </summary>
	public GameState gameState;

	/// <summary>
	/// Loads the play scene.
	/// </summary>
	public void LoadPlayScene()
	{
		gameState.IsGameOver = false;

		if (gameState.Stage == StageEnum.FirstStage)
		{
			gameState.TimeComplete = string.Format (Constants.TimeCompleteFormat, "00:00:00");
			SceneManager.LoadScene ("FirstStage");
		}
		else
		{
			SceneManager.LoadScene ("SecondStage");
		}
	}

	/// <summary>
	/// Quit this instance.
	/// </summary>
	public void Quit()
	{
		gameState.IsGameOver = false;
		gameState.TimeComplete = string.Format (Constants.TimeCompleteFormat, "00:00:00");
		gameState.Stage = StageEnum.FirstStage;
		Application.Quit ();
	}
}
