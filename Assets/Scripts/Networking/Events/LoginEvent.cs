public class LoginEvent {

    public delegate void LoginHandler(Message message);

    public static event LoginHandler onLoginStart;
    public static event LoginHandler onLoginInfo;
    public static event LoginHandler onLoginSuccessful;
    public static event LoginHandler onLoginFailed;
    public static event LoginHandler onLogout;

    public static void FireOnLoginStart(Message message)
    {
        if (onLoginStart != null)
            onLoginStart(message);
    }

    public static void FireOnLoginInfo(Message message)
    {
        if (onLoginInfo != null)
            onLoginInfo(message);
    }
    
    public static void FireOnLoginSuccessful(Message message)
    {
        if (onLoginSuccessful != null)
            onLoginSuccessful(message);
    }
    
    public static void FireOnLoginFailed(Message message)
    {
        if (onLoginFailed != null)
            onLoginFailed(message);
    }

    public static void FireOnLogout(Message message)
    {
        if (onLogout != null)
            onLogout(message);
    }
}
