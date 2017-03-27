using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour 
{
	private float second = 0;
	private float minute = 0;
	private float hour = 0;

	private string timeFormat;
	private string secondText;
	private string minuteText;
	private string hourText;

	public Text timeText;

	// Use this for initialization
	void Start () 
	{
		timeFormat = "{0}:{1}:{2}";
		secondText = "00";
		minuteText = "00";
		hourText = "00";
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (second >= 60.0f)
		{
			second = 0.0f;
			minute++;
		}

		if (minute >= 60.0f)
		{
			minute = 0.0f;
			hour++;
		}
		
		second += Time.deltaTime;

		SetTimerText ();
	}

	/// <summary>
	/// Sets the start time.
	/// </summary>
	/// <param name="startSecond">Start second.</param>
	/// <param name="startMinute">Start minute.</param>
	/// <param name="startHour">Start hour.</param>
	public void SetStartTime(float startSecond, float startMinute, float startHour)
	{
		second = startSecond;
		minute = startMinute;
		hour = startHour;
	}

	/// <summary>
	/// Sets the timer text.
	/// </summary>
	private void SetTimerText()
	{
		if (second < 10)
		{
			secondText = "0" + ((int)second).ToString ();
		}
		else
		{
			secondText = ((int)second).ToString ();
		}

		if (minute < 10)
		{
			minuteText = "0" + ((int)minute).ToString ();
		}
		else
		{
			minuteText = ((int)minute).ToString ();
		}

		if (hour < 10)
		{
			hourText = "0" + ((int)hour).ToString ();
		}
		else
		{
			hourText = ((int)hour).ToString ();
		}


		timeText.text = string.Format (timeFormat, hourText, minuteText, secondText);
	}
}
