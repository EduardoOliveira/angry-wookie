using UnityEngine;
using System.Collections;

public class LoginEventHandler : MonoBehaviour {

	// Use this for initialization
	void Start () {

        LoginEvent.onLoginStart += OnLoginStart;
        LoginEvent.onLoginInfo += OnLoginInfo;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void OnLoginStart(Message msg) 
    {
        Console.Write(msg.getNextString());
    }

    public void OnLoginInfo(Message msg)
    {		
		TCPHandler.getInstance().send(new MessageBuilder(MessageType.LOGIN_SUCCESSFUL).Build());	
    }
}
