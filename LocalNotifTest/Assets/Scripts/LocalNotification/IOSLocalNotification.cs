using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

#if UNITY_IOS
using Notification = UnityEngine.iOS.NotificationServices;
#endif
public class IOSLocalNotification : ILocalNotification
{

	List<NotificationModel> notificationList;

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
		foreach(NotificationModel model in notificationList)
		{
			var notif = new UnityEngine.iOS.LocalNotification();
			notif.fireDate = model.Time;
			notif.alertAction = model.title;
			notif.alertBody = model.message;
			Notification.ScheduleLocalNotification(notif);
		}

		notificationList = new List<NotificationModel> ();
		#endif
	}

	public void CancelScheduledNotification (int id)
	{
		#if UNITY_IOS
		Notification.CancelLocalNotification(Notification.GetLocalNotification(id));
		#endif
	}

	public void CancelAllScheduledNotification ()
	{
		#if UNITY_IOS
		Notification.CancelAllLocalNotifications();
		#endif
	}

	#endregion
	
}
