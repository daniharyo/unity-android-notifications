package net.agasper.unitynotification;

/**
 * Created by touchtenmac9 on 4/29/16.
 */
public class NotificationModel {

    /*
    String ticker = intent.getStringExtra("ticker");
    String title = intent.getStringExtra("title");
    String message = intent.getStringExtra("message");
    String s_icon = intent.getStringExtra("s_icon");
    String l_icon = intent.getStringExtra("l_icon");
    int color = intent.getIntExtra("color", 0);
    String unityClass = intent.getStringExtra("activity");
    Boolean sound = intent.getBooleanExtra("sound", false);
    Boolean vibrate = intent.getBooleanExtra("vibrate", false);
    Boolean lights = intent.getBooleanExtra("lights", false);
    int id = intent.getIntExtra("id", 0);
    */
    private String ticker = "";
    private String title = "";
    private String message = "";
    private String s_icon = "";
    private String l_icon = "";
    private int color = 0;
    private String unityClass = "";
    private Boolean sound = false;
    private Boolean vibrate = false;
    private Boolean lights = false;
    private int id = 0;

    public NotificationModel(){

    }

    public NotificationModel(String ticker, String title, String message, String s_icon, String l_icon,
                             int color, String unityClass, Boolean sound, Boolean vibrate, Boolean lights, int id){
        this.ticker = ticker;
        this.title = title;
        this.message = message;
        this.s_icon = s_icon;
        this.l_icon = l_icon;
        this.color = color;
        this.unityClass = unityClass;
        this.sound = sound;
        this.vibrate = vibrate;
        this.lights = lights;
        this.id = id;
    }


    public String getTicker() {
        return ticker;
    }

    public void setTicker(String ticker) {
        this.ticker = ticker;
    }

    public String getTitle() {
        return title;
    }

    public void setTitle(String title) {
        this.title = title;
    }

    public String getMessage() {
        return message;
    }

    public void setMessage(String message) {
        this.message = message;
    }

    public String getS_icon() {
        return s_icon;
    }

    public void setS_icon(String s_icon) {
        this.s_icon = s_icon;
    }

    public String getL_icon() {
        return l_icon;
    }

    public void setL_icon(String l_icon) {
        this.l_icon = l_icon;
    }

    public int getColor() {
        return color;
    }

    public void setColor(int color) {
        this.color = color;
    }

    public String getUnityClass() {
        return unityClass;
    }

    public void setUnityClass(String unityClass) {
        this.unityClass = unityClass;
    }

    public Boolean getSound() {
        return sound;
    }

    public void setSound(Boolean sound) {
        this.sound = sound;
    }

    public Boolean getVibrate() {
        return vibrate;
    }

    public void setVibrate(Boolean vibrate) {
        this.vibrate = vibrate;
    }

    public Boolean getLights() {
        return lights;
    }

    public void setLights(Boolean lights) {
        this.lights = lights;
    }

    public int getId() {
        return id;
    }

    public void setId(int id) {
        this.id = id;
    }
}
