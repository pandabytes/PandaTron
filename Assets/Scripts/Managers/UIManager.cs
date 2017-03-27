using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
	// Player
	public GameObject player;

	// Ability UI gadgets
	public Image popUpWindow;
	public Button okButton;
	public Text message;

	// Player's shield UI
	public Text shieldText;
	public Image shieldBorder;
	public Image shieldBackground;
	public Image shieldBar;

	public StageEnum stage;

	/// <summary>
	/// Start this instance.
	/// </summary>
	void Start ()
	{
		okButton.onClick.AddListener (ClickOK);

		if (stage == StageEnum.SecondStage)
		{
			player.GetComponent<PlayerMovement> ().CollideIce += new EventHandler (CollideIceHandler);

			// Enable the pop up window
			message.text = Constants.SwitchCameraMessage;
			EnablePopUpWindow ();

			player.GetComponent<KeyHolder> ().AcquireTransformKey += new EventHandler (AcquireTransformKeyHandlder);
		}
		else
		{
			message.text = Constants.BasicControlsMessage;
			EnablePopUpWindow ();

			player.GetComponent<KeyHolder> ().AcquireJumpKey += new EventHandler (AcquireJumpKeyHandler);
			player.GetComponent<KeyHolder> ().AcquireShieldKey += new EventHandler (AcquireShieldKeyHandler);
		}
	}

	/// <summary>
	/// Update this instance.
	/// </summary>
	void Update()
	{
		// Close the pop up window when user preses enter
		if ((Input.GetKeyDown (KeyCode.Return) || Input.GetKeyDown (KeyCode.KeypadEnter)) && IsPopUpWindowEnabled ())
		{
			ClickOK ();
		}
	}
		
	/// <summary>
	/// Handle the acquire jump key event.
	/// Set the jump message visible for player and freeze the game.
	/// </summary>
	/// <param name="sender">Sender.</param>
	/// <param name="e">Event argument.</param>
	private void AcquireJumpKeyHandler(object sender, EventArgs e)
	{		
		message.text = Constants.JumpMessage;
		EnablePopUpWindow ();
	}

	/// <summary>
	/// Handle the acquire shield key event.
	/// Set the shield message visible for player and freeze the game.
	/// </summary>
	/// <param name="sender">Sender.</param>
	/// <param name="e">E.</param>
	private void AcquireShieldKeyHandler(object sender, EventArgs e)
	{
		// Set the shield bar and text visible
		shieldText.gameObject.SetActive (true);
		shieldBorder.gameObject.SetActive (true);
		shieldBackground.gameObject.SetActive (true);
		shieldBar.gameObject.SetActive (true);

		message.text = Constants.ShieldMessage;
		EnablePopUpWindow ();
	}

	/// <summary>
	/// Handle when acquire tranform key.
	/// </summary>
	/// <param name="sender">Sender.</param>
	/// <param name="e">E.</param>
	private void AcquireTransformKeyHandlder(object sender, EventArgs e)
	{
		message.text = Constants.TransformMessage;
		EnablePopUpWindow ();
	}

	/// <summary>
	/// Handle when player touches the ice block.
	/// </summary>
	/// <param name="sender">Sender.</param>
	/// <param name="e">E.</param>
	private void CollideIceHandler(object sender, EventArgs e)
	{
		message.text = Constants.IceBlockMessage;
		EnablePopUpWindow ();
	}

	/// <summary>
	/// Determines whether the pop up window is enabled.
	/// </summary>
	/// <returns><c>true</c> if pop up window is enabled; otherwise, <c>false</c>.</returns>
	public bool IsPopUpWindowEnabled()
	{
		return popUpWindow.gameObject.activeSelf && okButton.gameObject.activeSelf && 
			   message.gameObject.activeSelf;	
	}

	/// <summary>
	/// Enables the pop up window.
	/// </summary>
	private void EnablePopUpWindow()
	{
		Time.timeScale = 0.0f;
		popUpWindow.gameObject.SetActive (true);
		okButton.gameObject.SetActive (true);
		message.gameObject.SetActive (true);
	}

	/// <summary>
	/// Unfreeze the game when player clicks ok.
	/// </summary>
	private void ClickOK()
	{
		Time.timeScale = 1.0f;
		message.text = string.Empty;
		popUpWindow.gameObject.SetActive (false);
		okButton.gameObject.SetActive (false);
		message.gameObject.SetActive (false);
	}
}

