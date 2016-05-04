using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;

/// <summary>
/// Local notification android.
/// Don't forget to add
/// <receiver android:name="net.agasper.unitynotification.UnityNotificationManager"></receiver>
/// </summary>
public class AndroidLocalNotification :ILocalNotification
{
	#region ILocalNotification implementation

	Color bgColor = new Color (246 / 255f, 135 / 255f, 44 / 255f);
	List<NotificationModel> notificationList;

	public AndroidLocalNotification ()
	{
		notificationList = new List<NotificationModel> ();
	}

	public void CreateNotification (int id, string title, string text, float delaySeconds)
	{
		NotificationModel model = new NotificationModel (id, title, text, delaySeconds);
		notificationList.Add (model);
	}

	public void Dispatch ()
	{
		if (notificationList != null && notificationList.Count > 0) {
			List<NotificationModel> sortedList = notificationList.OrderBy (x => x.delaySeconds).ToList ();

			NotificationModel fModel = sortedList [0];

			SendNotification (fModel.id, 
				(int)fModel.delaySeconds, 
				fModel.title, 
				fModel.message, 
				new List<string> ().ToArray (), 
				Color.white);
			
			for (int i = 1; i < sortedList.Count; i++) {
				List<string> messages = new List<string> ();
				for (int j = i - 1; j >= 0; j--) {
//					if (sortedList [j].id == sortedList [i].id) {
					messages.Add (sortedList [j].message);
//					}
				}
				messages.Add (sortedList [i].message);

				SendNotification (sortedList [i].id, 
					(int)sortedList [i].delaySeconds, 
					sortedList [i].title, 
					sortedList [i].message,
					messages.ToArray (), 
					bgColor
				);
				
			}

			notificationList = new List<NotificationModel> ();
		}
	}

	public void CancelScheduledNotification (int id)
	{
		CancelNotification (id);
		for (int i = notificationList.Count - 1; i > notificationList.Count; i--) {
			if (notificationList [i].id == id) {
				notificationList.RemoveAt (i);
			}
		}
	}

	public void CancelAllScheduledNotification ()
	{
		CancelAllNotifications ();
		notificationList.Clear();
	}

	#endregion


	public enum NotificationExecuteMode
	{
		Inexact = 0,
		Exact = 1,
		ExactAndAllowWhileIdle = 2
	}



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
	//	public static void SendNotification (int id, TimeSpan delay, string title, string message)
	//	{
	////		SendNotification (id, (int)delay.TotalSeconds, title, message, Color.white);
	//	}
	//
	public static void SendNotification (int id, long delay, string title, string summary, string[] message, Color32 bgColor, bool sound = true, bool vibrate = true, bool lights = true, string bigIcon = "", NotificationExecuteMode executeMode = NotificationExecuteMode.Inexact)
	{

		#if UNITY_ANDROID && !UNITY_EDITOR
		AndroidJavaClass pluginClass = new AndroidJavaClass (fullClassName);

		object[] msgParam = new object[15];
		msgParam [0] = id;
		msgParam [1] = delay * 1000L;
		msgParam [2] = title;
		msgParam [3] = summary;
		msgParam [4] = message;
		msgParam [5] = summary;
		msgParam [6] = sound ? 1 : 0;
		msgParam [7] = vibrate ? 1 : 0;
		msgParam [8] = lights ? 1 : 0;
		msgParam [9] = bigIcon;
		msgParam [10] = "notify_icon_small";
		msgParam [11] = bgColor.r * 65536 + bgColor.g * 256 + bgColor.b;
		msgParam [12] = (int)executeMode;
		msgParam [13] = mainActivityClassName;
		msgParam [14] = Application.productName;
		if (pluginClass != null) {
			pluginClass.CallStatic ("SetNotification", msgParam);
			SavingNotification.AddNotification (id);
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


