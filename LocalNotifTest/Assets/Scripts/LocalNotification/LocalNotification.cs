using UnityEngine;
using System.Collections;
using System;


public class LocalNotification : MonoBehaviour
{

	// Use this for initialization

	[Range (0, 23)]
	public int DailyRewardNotificationTime;


	const int StaminaNotificationID = 1;
	const int ChapterUnlockNotificationID = 3;
	const int LetsPlayNotificationID = 10;
	const int DailyRewardNotificationID = 12;

	private ILocalNotification notification;

	private ILocalNotification NotificationServices {
		get {
			if (notification == null) {
				#if UNITY_ANDROID
				notification = new AndroidLocalNotification ();
				#elif UNITY_IOS
				notification = new IOSLocalNotification();
				#else
				notification = new EditorLocalNotification();
				#endif
			}
			return notification;
		}
	}

	void CancelAllLocalNotification ()
	{
		NotificationServices.CancelAllScheduledNotification ();
	}

	void Start ()
	{
		DontDestroyOnLoad (this.gameObject);
		CancelAllLocalNotification ();
	}


			


	void CreateNotificationBatch ()
	{
		CreateStaminaFullNotification ();
		CreateChapterUnlockedNotification ();
		CreateNotPlayingForAwhileNotification ();
		CreateDailyRewardAvailableNotification ();
	}

	void CreateStaminaFullNotification ()
	{

		string StaminaFullTitle = "notification.stamina_full_title";
		string StaminaFullMessage = "notification.stamina_full_message";
		int delay = 5;
		LogNotification ("StaminaNotification", delay);

		CreateNotification (StaminaNotificationID, delay, StaminaFullTitle, StaminaFullMessage);



	}

	void CreateChapterUnlockedNotification ()
	{

		string WorldUnlockedTitle = "notification.world_unlocked_title";
		string WorldUnlokcedMessage = "notification.world_unlocked_message";
		int delay = 20;
		LogNotification ("ChapterUnlockNptification", delay);

		CreateNotification (ChapterUnlockNotificationID, delay, WorldUnlockedTitle, WorldUnlokcedMessage);
		
	}

	void CreateNotPlayingForAwhileNotification ()
	{
		string LetsPlayTitle = "notification.lets_play_title";
		string LetsPlayMessage = "notification.lets_play_message";
		int delay = 40;

		LogNotification ("LetsPlayNotification", delay);
		CreateNotification (LetsPlayNotificationID, delay, LetsPlayTitle, LetsPlayMessage);
	}

	void CreateDailyRewardAvailableNotification ()
	{
		int delay = 60;
		string RewardReadyTitle = "notification.reward_ready_title";
		string RewardReadyMessage = "notification.reward_ready_message";

	

		LogNotification ("DailyRewardNotification", delay);
		CreateNotification (DailyRewardNotificationID, delay, RewardReadyTitle, RewardReadyMessage);
	}

	void LogNotification (string title, int delay)
	{
		Debug.Log ("<color=lime>Create notification of " + title + " with delay = " + delay.ToString () + "</color>");
	}

	int GetDailyDelay (int dayDiff)
	{
		int ret = 0;
		DateTime now = DateTime.Now;
		DateTime target = new DateTime (now.Year, now.Month, now.Day + dayDiff, DailyRewardNotificationTime, 0, 0);
		TimeSpan timeDiff = target - now;
		ret = (int)timeDiff.TotalSeconds;
		return ret;
	}


	void CreateNotification (int id, float waitTime, string title, string summary)
	{
		NotificationServices.CreateNotification (id, title, summary, waitTime);
	}


	void OnApplicationPause (bool isPaused)
	{
		if (isPaused) {
			Debug.Log ("Create Notification");
			CreateNotificationBatch ();
				NotificationServices.Dispatch();
		} else {
			Debug.Log ("Cancel Notification");
			CancelAllLocalNotification ();
		}
	}

}
