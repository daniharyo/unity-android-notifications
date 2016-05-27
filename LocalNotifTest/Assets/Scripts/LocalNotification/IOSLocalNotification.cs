using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

#if UNITY_IOS
using Notification = UnityEngine.iOS.NotificationServices;
using iOSLocalNotification = UnityEngine.iOS.LocalNotification;
#endif
public class IOSLocalNotification : ILocalNotification
{

	List<NotificationModel> notificationList;
	private const string UserInfoIDKey = "id";

	public IOSLocalNotification ()
	{
		
		notificationList = new List<NotificationModel> ();
		#if UNITY_IOS

		Notification.RegisterForNotifications (UnityEngine.iOS.NotificationType.Alert);
		Notification.RegisterForNotifications (UnityEngine.iOS.NotificationType.Badge);
		Notification.RegisterForNotifications (UnityEngine.iOS.NotificationType.Sound);
		#endif
	}

	#region ILocalNotification implementation



	public void CreateNotification (int id, string title, string text, float delaySeconds)
	{
		NotificationModel model = new NotificationModel (id, title, text, delaySeconds);

		notificationList.Add (model);


	}

	public void CreateNotification (int id, string title, string text, DateTime time)
	{
		NotificationModel model = new NotificationModel (id, title, text, time);
		notificationList.Add (model);
	}

	public void Dispatch ()
	{
		#if UNITY_IOS
		foreach (NotificationModel model in notificationList) {
			var notif = new UnityEngine.iOS.LocalNotification ();
			notif.fireDate = model.Time;
			notif.alertAction = model.title;
			notif.alertBody = model.message;
			Dictionary<string,string> info = new Dictionary<string,string> ();
			info.Add (UserInfoIDKey, model.id.ToString ());
			notif.userInfo = info;
			Notification.ScheduleLocalNotification (notif);
		}

		notificationList = new List<NotificationModel> ();
		#endif
	}

	public void CancelScheduledNotification (int id)
	{
		
		#if UNITY_IOS

		int notifID = getNotificationIndex (id);
		if (notifID == -1) {
			return;
		}
		Notification.CancelLocalNotification (Notification.GetLocalNotification (notifID));
		#endif
	}

	public void CancelAllScheduledNotification ()
	{
		#if UNITY_IOS
		Notification.CancelAllLocalNotifications ();
		#endif
	}

	#endregion

	#if UNITY_IOS
	private int getNotificationIndex (int id)
	{
		for (int i = 0; i < Notification.scheduledLocalNotifications.Length; i++) {
			if (isIDEqual (Notification.scheduledLocalNotifications [i], id)) {
				return i;
			}
		}
		return -1;
	}

	private bool isIDEqual (iOSLocalNotification notif, int id)
	{
		string notifID = string.Empty;
		Dictionary<string,string> userInfo = (Dictionary<string,string>)notif.userInfo;

		if (userInfo.TryGetValue (UserInfoIDKey, out notifID)) {
			if (notifID == id.ToString ()) {
				return true;
			} else {
				return false;
			}
		}
		return false;
	}
	#endif
}
