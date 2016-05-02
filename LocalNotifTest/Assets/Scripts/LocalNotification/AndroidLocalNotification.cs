using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

/// <summary>
/// Local notification android.
/// Don't forget to add
/// <receiver android:name="net.agasper.unitynotification.UnityNotificationManager"></receiver>
/// </summary>
public class AndroidLocalNotification :ILocalNotification
{
	#region ILocalNotification implementation

	public void CreateNotification (int id, string title, string text, float delaySeconds)
	{
		SendNotification (id, new TimeSpan (0, 0, (int)delaySeconds), title, text);
	}

	public void CancelScheduledNotification (int id)
	{
		CancelNotification (id);
	}

	public void CancelAllScheduledNotification ()
	{
		CancelAllNotifications ();
	}

	#endregion


	public enum NotificationExecuteMode
	{
		Inexact = 0,
		Exact = 1,
		ExactAndAllowWhileIdle = 2
	}

	private static readonly string gameName = Application.productName;
	#if UNITY_ANDROID && !UNITY_EDITOR
	private static string fullClassName = "net.agasper.unitynotification.UnityNotificationManager";
	private static string mainActivityClassName = "com.unity3d.player.UnityPlayerNativeActivity";
	#endif

	/// <summary>
	/// Sends the notification.
	/// Don't forget to set notification image "notify_icon_small" at "plugins/Android/res/drawable"
	/// </summary>
	/// <param name="id">Identifier.</param>
	/// <param name="delay">Delay.</param>
	/// <param name="title">Title.</param>
	/// <param name="message">Message.</param>
	public static void SendNotification (int id, TimeSpan delay, string title, string message)
	{
		SendNotification (id, (int)delay.TotalSeconds, title, message, new Color(246/255f,135/255f,44/255f));
	}

	public static void SendNotification (int id, long delay, string title, string message, Color32 bgColor, bool sound = true, bool vibrate = true, bool lights = true, string bigIcon = "", NotificationExecuteMode executeMode = NotificationExecuteMode.Inexact)
	{
		#if UNITY_ANDROID && !UNITY_EDITOR
		AndroidJavaClass pluginClass = new AndroidJavaClass(fullClassName);
		if (pluginClass != null){
		pluginClass.CallStatic("SetNotification", id, delay * 1000L,gameName, title, message, message, sound ? 1 : 0, vibrate ? 1 : 0, lights ? 1 : 0, bigIcon, "notify_icon_small", bgColor.r * 65536 + bgColor.g * 256 + bgColor.b, (int)executeMode, mainActivityClassName);
		SavingNotification.AddNotification(id);
		}
		#endif
	}

	public static void CancelNotification (int id)
	{
		Debug.Log ("unity cancel id " + id);
		#if UNITY_ANDROID && !UNITY_EDITOR
		AndroidJavaClass pluginClass = new AndroidJavaClass(fullClassName);
		if (pluginClass != null) {
		pluginClass.CallStatic("CancelNotification", id);
		SavingNotification.RemoveNotification(id);
		}
		#endif
	}

	public static void CancelAllNotifications ()
	{
		Debug.Log ("unity cancel all");      
		#if UNITY_ANDROID && !UNITY_EDITOR
		AndroidJavaClass pluginClass = new AndroidJavaClass (fullClassName);
		if (pluginClass != null) {
		foreach (int id in SavingNotification.GetAllNotificationId()) {
		CancelNotification(id);
		}
		}
		#endif
	}

}

