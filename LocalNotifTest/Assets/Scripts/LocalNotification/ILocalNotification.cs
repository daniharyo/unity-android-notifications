using UnityEngine;
using System.Collections;



public interface ILocalNotification
{
	void CreateNotification (int id, string title, string text, float delaySeconds);
	void Dispatch();
	void CancelScheduledNotification (int id);
	void CancelAllScheduledNotification ();
}


public class NotificationModel
{
	public int id;
	public string title;
	public string message;
	public float delaySeconds;

	public NotificationModel(int id,string title,string message,float delaySecond)
	{
		this.id = id;
		this.title = title;
		this.message = message;
		this.delaySeconds = delaySecond;
	}
}