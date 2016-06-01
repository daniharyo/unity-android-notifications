using UnityEngine;
using System.Collections;
using System;



public interface ILocalNotification
{
	void CreateNotification (int id, string title, string text, float delaySeconds);

	void CreateNotification (int id, string title, string text, System.DateTime time);

	void Dispatch ();

	void CancelScheduledNotification (int id);

	void CancelAllScheduledNotification ();
}


public class NotificationModel
{
	public readonly int id;
	public readonly string title;
	public readonly string message;
	public readonly float delaySeconds;
	public readonly DateTime Time;
	public readonly NotificationDelayType delayType;

	public NotificationModel (int id, string title, string message, float delaySecond)
	{
		this.id = id;
		this.title = title;
		this.message = message;
        this.delaySeconds = delaySecond;
        this.Time = DateTime.Now.AddSeconds(delaySecond);
		delayType = NotificationDelayType.DELAY_SECONDS;
	}

	public NotificationModel (int id, string title, string message, DateTime time)
	{
		this.id = id;
		this.title = title;
		this.message = message;
		this.Time = time;
		delayType = NotificationDelayType.ON_TIME;
	}

	public float getDispatchDelay()
	{
		float delay = 0f;
		if (delayType == NotificationDelayType.DELAY_SECONDS) {
			delay = delaySeconds;
		} else if (delayType == NotificationDelayType.ON_TIME) {
			var tdelay = Time - DateTime.Now;
			delay = (float)tdelay.TotalSeconds;
		}

		return delay;

	}
}

public enum NotificationDelayType
{
	DELAY_SECONDS,
	ON_TIME
}