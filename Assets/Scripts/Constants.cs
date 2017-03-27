using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StageEnum
{
	FirstStage, SecondStage
}

public static class Constants
{
	/// <summary>
	/// The time complete format.
	/// </summary>
	public const string TimeCompleteFormat = "Time Complete: {0}";

	#region Damage Amounts

	/// <summary>
	/// The touch damaged.
	/// </summary>
	public const float TouchDamaged = 1.0f;

	/// <summary>
	/// The arrow damaged.
	/// </summary>
	public const float ArrowDamaged = 5.0f;

	/// <summary>
	/// The particle damaged.
	/// </summary>
	public const float ParticleDamaged = 5.0f;

	/// <summary>
	/// The ice damaged.
	/// </summary>
	public const float IceDamaged = 2.50f;

	/// <summary>
	/// The laser damage amount.
	/// </summary>
	public const float LaserDamage = 0.088f;

	/// <summary>
	/// The energy ball damage.
	/// </summary>
	public const float EnergyBallDamage = 4.0f;

	/// <summary>
	/// The fire damage.
	/// </summary>
	public const float FireDamage = 0.004f;

	#endregion

	#region Shield and energy health

	/// <summary>
	/// The shield depletion.
	/// </summary>
	public const float ShieldDepletion = 0.6f;

	/// <summary>
	/// The shield restoration.
	/// </summary>
	public const float ShieldRestoration = 0.2f;

	/// <summary>
	/// The energy depletion.
	/// </summary>
	public const float EnergyDepletion = 5.0f;

	/// <summary>
	/// The flame depletion.
	/// </summary>
	public const float FlameDepletion = 1.0f;

	#endregion

	#region Key constants

	/// <summary>
	/// The name of the jump key.
	/// </summary>
	public const string JumpKeyName = "JumpKey";

	/// <summary>
	/// The jump message.
	/// </summary>
	public const string JumpMessage = "Jump ability acquired!\n\nPress \"Space\" to jump.";

	/// <summary>
	/// The name of the shield key.
	/// </summary>
	public const string ShieldKeyName = "ShieldKey";

	/// <summary>
	/// The shield message.
	/// </summary>
	public const string ShieldMessage = "Shield acquired!\n\nRight-click to activate shield.";

	/// <summary>
	/// The name of the transform key.
	/// </summary>
	public const string TransformKeyName = "TransformKey";

	/// <summary>
	/// The transform message.
	/// </summary>
	public const string TransformMessage = "Transformation Acquired!\n\nPress 1 to switch form\nFlamethrower: Left-Click\nLaser: Right-Click";


    #endregion

    #region Popup Messages

    /// <summary>
    /// The ice block message
    /// </summary>
    public const string IceBlockMessage = "Oh no! The ice is too cold!\n\nMaybe there is a way to melt it?";

    /// <summary>
    /// Basic controls instruction in Stage 1
    /// </summary>
	public const string BasicControlsMessage = "Stage 1\n\n\nControls\n\nLeft-Click to shoot\nUse W-A-S-D keys to move";

    /// <summary>
    /// Basic controls instruction ins Stage 2
    /// </summary>
	public const string SwitchCameraMessage = "Stage 2\n\nMiddle-click to switch camera view";

    #endregion

}
