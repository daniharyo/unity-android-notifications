using UnityEngine;
using System.Collections;
using System.Collections.Generic;




public class SavingNotification
{
	const string FileName = "GameNotification";
	const char CharSeparate = '_';

	public static void AddNotification (int id)
	{
		string savingData = getSavingData ();
		string newSavingData = string.Concat (savingData, CharSeparate, id);
		saveAllData (newSavingData);
	}

	public static void RemoveNotification (int id)
	{
		List<int> notificationsId = GetAllNotificationId ();
		notificationsId.RemoveAll (obj => obj == id);
		string notificationData = "";
		for (int i = 0; i < notificationsId.Count; i++) {
			notificationData = string.Concat (notificationData, CharSeparate, notificationsId [i]);
		}

		saveAllData (notificationData);
	}

	public static List<int> GetAllNotificationId ()
	{
		List<int> notificationsId = new List<int> ();

		string[] savingData = getSavingData ().Split (CharSeparate);

		for (int i = 0; i < savingData.Length; i++) {
			try {
				notificationsId.Add (int.Parse (savingData [i]));
			} catch (System.Exception ex) {
					
			}
		}
		return notificationsId;
	}


	#region save & load

	static string getSavingData ()
	{
		if (PlayerPrefs.HasKey (FileName)) {
			return PlayerPrefs.GetString (FileName);
		} else {
			return "";
		}
	}


	static void saveAllData (string saveData)
	{
		PlayerPrefs.SetString (FileName, saveData);
	}

	#endregion
}

