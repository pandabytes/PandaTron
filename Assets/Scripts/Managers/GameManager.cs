using UnityEngine;
using System;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	public StageEnum sceneStage;
	public GameState gameState;
	public Text timeText;
	public TeleportPad finalTeleportationPad;

	public GameObject player;
	public GameObject beastMode;
	public GameObject boss;

	public Camera camera1;
	public Camera camera2;
	public Camera camera3;
	public Camera camera4;

	private Timer timer;
	private float time;

	// Use this for initialization
	void Start ()
	{
		if (!gameState.IsGameOver && sceneStage == StageEnum.SecondStage)
		{
			// Continue the time
			Timer timer = GetComponent<Timer> ();
			timeText.text = gameState.TimeComplete; 
			float second = float.Parse(timeText.text.Substring (timeText.text.Length - 2));
			float minute = float.Parse(timeText.text.Substring (timeText.text.Length - 5, 2));
			float hour = float.Parse(timeText.text.Substring (timeText.text.Length - 8, 2));

			finalTeleportationPad.ReachFinalDestination += new EventHandler (ReachFinalDestinationHandler);
			timer.SetStartTime (second, minute, hour);
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
		// Display Win scene
		if (boss != null && boss.activeSelf && boss.GetComponent<EnemyHealth> ().CurrentHealth <= 0.0f)
		{
			time += Time.deltaTime;
			if (time >= 5.0f)
			{
				gameState.TimeComplete = string.Format(Constants.TimeCompleteFormat, timeText.text);
				gameState.IsGameOver = true;
				gameState.Stage = StageEnum.FirstStage;
				SceneManager.LoadScene ("Win");
			}
		}
	}

	/// <summary>
	/// Handle the reach final destination event.
	/// </summary>
	/// <param name="sender">Sender.</param>
	/// <param name="e">E.</param>
	private void ReachFinalDestinationHandler(object sender, EventArgs e)
	{
		boss.SetActive (true);
		player.GetComponent<SwitchCamera> ().enabled = false;
		beastMode.GetComponent<SwitchCamera> ().enabled = false;

		camera1.gameObject.SetActive (false);
		camera2.gameObject.SetActive (false);
		camera3.gameObject.SetActive (false);

		camera4.GetComponent<CameraFollow> ().enabled = true;
		camera4.GetComponent<AudioListener> ().enabled = true;
		camera4.enabled = true;
	}
}

