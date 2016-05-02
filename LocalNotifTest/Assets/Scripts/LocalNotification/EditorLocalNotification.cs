using UnityEngine;
using System.Collections;

public class EditorLocalNotification : ILocalNotification
{
	#region ILocalNotification implementation

	public void Dispatch ()
	{
	}

	public void CreateNotification (int id, string title, string text, float delaySeconds)
	{
		Debug.Log (string.Format ("<color=lime>Editor Create local notification title = {0} , \nmessage = {1}, \ndelay = {2} </color>", title, text, delaySeconds));
	}

	public void CancelScheduledNotification (int id)
	{
		Debug.Log ("<color=lime>Editor Cancel scheduled local notification of id : " + id.ToString () + "</color>");
	}

	public void CancelAllScheduledNotification ()
	{
		Debug.Log ("<color=lime>Editor Cancel all scheduled local notification</color>");
	}

	#endregion


}
