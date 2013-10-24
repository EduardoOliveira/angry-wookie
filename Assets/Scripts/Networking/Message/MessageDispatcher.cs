using UnityEngine;
using System.Collections;

public class MessageDispatcher{

    public static void Dispatch(Message msg)
    {
        byte b = msg.getOpCode();
        Console.Write("Op" + b);
        switch (msg.getOpCode()) { 
            //** Login events
            case MessageType.LOGIN:
                LoginEvent.FireOnLoginStart(msg);
                break;
            case MessageType.LOGIN_FAILURE:
                LoginEvent.FireOnLoginFailed(msg);
                break;
            case MessageType.LOGIN_INFO:
                LoginEvent.FireOnLoginInfo(msg);
                break;

            //** Player events
            case MessageType.MOVEMENT_SYNC:
                Console.Write("sync");
                PlayerEvent.FireOnPlayerSync(msg);
                break;

            //** Shotting events
            case MessageType.SHOOTING_SLAVE_SIMPLE:
                Debug.Log("shot Recieved");
                ProjectileEvent.FireOnShootindSlaveSimple(msg);
                break;
        }
    }
}
