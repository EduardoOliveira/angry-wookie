using UnityEngine;
using System.Collections;

public class MessageDispatcher{

    public MessageDispatcher(byte[] message) 
    { 
   
        //this.body = new byte[]
    
    }


    public static void Dispatch(Message msg)
    {
        byte b = msg.getOpCode();
        Console.Write("" + b);
        switch (msg.getOpCode()) { 
            //** Login events
            case MessageType.LOGIN:
                LoginEvent.FireOnLoginStart(msg);
                break;
            case MessageType.LOGIN_FAILURE:
                LoginEvent.FireOnLoginFailed(msg);
                break;
            case MessageType.LOGIN_INFO:
                Console.Write("info");
                LoginEvent.FireOnLoginInfo(msg);
                break;

            //** Player events
            case MessageType.MOVEMENT_SYNC:
                PlayerEvent.FireOnPlayerSync(msg);
                break;
        }
    }
}
