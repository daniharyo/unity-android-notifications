package net.agasper.unitynotification;

import android.app.Activity;
import android.app.AlarmManager;
import android.app.Notification;
import android.app.NotificationManager;
import android.app.PendingIntent;
import android.content.BroadcastReceiver;
import android.content.Context;
import android.content.Intent;
import android.content.res.Resources;
import android.graphics.BitmapFactory;
import android.graphics.Color;
import android.media.RingtoneManager;
import android.os.Build;
import android.util.Log;

import com.unity3d.player.UnityPlayer;

import java.net.Inet4Address;

public class UnityNotificationManager extends BroadcastReceiver {

    public static void SetNotification(int id, long delayMs, String title, String summary, String[] message, String ticker, int sound, int vibrate,
                                       int lights, String largeIconResource, String smallIconResource, int bgColor, int executeMode, String unityClass,String gameName) {
        Activity currentActivity = UnityPlayer.currentActivity;
        AlarmManager am = (AlarmManager)currentActivity.getSystemService(Context.ALARM_SERVICE);
        Intent intent = new Intent(currentActivity, UnityNotificationManager.class);
        intent.putExtra("ticker", ticker);
        intent.putExtra("title", title);
        intent.putExtra("summary",summary);
        intent.putExtra("message", message);
        intent.putExtra("id", id);
        intent.putExtra("color", bgColor);
        intent.putExtra("sound", sound == 1);
        intent.putExtra("vibrate", vibrate == 1);
        intent.putExtra("lights", lights == 1);
        intent.putExtra("l_icon", largeIconResource);
        intent.putExtra("s_icon", smallIconResource);
        intent.putExtra("activity", unityClass);
        intent.putExtra("gameName",gameName);

        Log.v("UnityClass", unityClass);
        if (Build.VERSION.SDK_INT >= Build.VERSION_CODES.M) {
            if (executeMode == 2)
                am.setExactAndAllowWhileIdle(0, System.currentTimeMillis() + delayMs, PendingIntent.getBroadcast(currentActivity, id, intent, PendingIntent.FLAG_UPDATE_CURRENT));
            else if (executeMode == 1)
                am.setExact(0, System.currentTimeMillis() + delayMs, PendingIntent.getBroadcast(currentActivity, id, intent, PendingIntent.FLAG_UPDATE_CURRENT));
            else
                am.set(0, System.currentTimeMillis() + delayMs, PendingIntent.getBroadcast(currentActivity, id, intent, PendingIntent.FLAG_UPDATE_CURRENT));
        }
        else
            am.set(0, System.currentTimeMillis() + delayMs, PendingIntent.getBroadcast(currentActivity, id, intent, PendingIntent.FLAG_UPDATE_CURRENT));
    }



    public void onReceive(Context context, Intent intent) {
        NotificationManager notificationManager = (NotificationManager)context.getSystemService(Context.NOTIFICATION_SERVICE);

        String ticker = intent.getStringExtra("ticker");
        String title = intent.getStringExtra("title");
        String summary = intent.getStringExtra("summary");
        String[] message = intent.getStringArrayExtra("message");
        String s_icon = intent.getStringExtra("s_icon");
        String l_icon = intent.getStringExtra("l_icon");
        int color = intent.getIntExtra("color", 0);
        String unityClass = intent.getStringExtra("activity");
        Boolean sound = intent.getBooleanExtra("sound", false);
        Boolean vibrate = intent.getBooleanExtra("vibrate", false);
        Boolean lights = intent.getBooleanExtra("lights", false);
        int id = intent.getIntExtra("id", 0);
        String gameName = intent.getStringExtra("gameName");
        Resources res = context.getResources();


        Log.v("UnityClass", unityClass);
        Class<?> unityClassActivity = null;
        try {
            unityClassActivity = Class.forName(unityClass);
        } catch (ClassNotFoundException e) {
            e.printStackTrace();
            return;
        }
        Intent notificationIntent = new Intent(context, unityClassActivity);

//        notificationIntent.setFlags(Intent.FLAG_ACTIVITY_NEW_TASK|Intent.FLAG_ACTIVITY_CLEAR_TASK);

        PendingIntent contentIntent = PendingIntent.getActivity(context, 0, notificationIntent, PendingIntent.FLAG_UPDATE_CURRENT);

        Notification.Builder builder = new Notification.Builder(context);

        builder.setContentIntent(contentIntent)
                .setWhen(System.currentTimeMillis())
                .setAutoCancel(true)
                .setContentTitle(title)
                .setContentText(summary);

        Notification.InboxStyle inboxStyle = new Notification.InboxStyle();

        for(String m:message)
        {
            inboxStyle.addLine(m);
        }
        inboxStyle.setBigContentTitle(gameName);
        builder.setStyle(inboxStyle);


        if (Build.VERSION.SDK_INT >= 21)
            builder.setColor(color);

        if(ticker != null && ticker.length() > 0)
            builder.setTicker(ticker);

        if (s_icon != null && s_icon.length() > 0)
            builder.setSmallIcon(res.getIdentifier(s_icon, "drawable", context.getPackageName()));

        if (l_icon != null && l_icon.length() > 0)
            builder.setLargeIcon(BitmapFactory.decodeResource(res, res.getIdentifier(l_icon, "drawable", context.getPackageName())));

        if(sound)
            builder.setSound(RingtoneManager.getDefaultUri(2));

        if(vibrate)
            builder.setVibrate(new long[] {
                    1000L, 1000L
            });

        if(lights)
            builder.setLights(Color.GREEN, 3000, 3000);


        Notification notification = builder.build();
        notification.flags |= Notification.FLAG_AUTO_CANCEL;
        notificationManager.notify(112, notification);

    }

    public static void CancelNotification(int id) {
        Activity currentActivity = UnityPlayer.currentActivity;
        AlarmManager am = (AlarmManager)currentActivity.getSystemService(Context.ALARM_SERVICE);
        Intent intent = new Intent(currentActivity, UnityNotificationManager.class);
        PendingIntent pendingIntent = PendingIntent.getBroadcast(currentActivity, id, intent,PendingIntent.FLAG_UPDATE_CURRENT);
        am.cancel(pendingIntent);
    }

    public static void CancelAll(){
        NotificationManager notificationManager = (NotificationManager)UnityPlayer.currentActivity.getApplicationContext().getSystemService(Context.NOTIFICATION_SERVICE);
        notificationManager.cancelAll();
    }
}
