package net.agasper.unitynotification;

import android.os.Debug;

import java.util.ArrayList;
import java.util.List;

/**
 * Created by touchtenmac9 on 4/29/16.
 */
public class StackNotificationHelper {

    private static List<NotificationModel> notificationModelList;


    public static void AddNotification(NotificationModel notificationModel){
        if (notificationModelList == null){
            notificationModelList = new ArrayList<NotificationModel>();
        }

        notificationModelList.add(notificationModel);
    }

    public static void ClearNotificationById(int id){
        if(notificationModelList!=null) {
            for (NotificationModel model : notificationModelList) {
                if(model.getId()==id)notificationModelList.remove(model);
            }
        }
    }

    public static void ClearNotification(){
        notificationModelList.clear();
    }

    public static List<NotificationModel> getNotificationModelList(){
        return notificationModelList;
    }


}
