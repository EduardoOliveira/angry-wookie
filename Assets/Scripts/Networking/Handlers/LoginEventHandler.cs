using UnityEngine;
using System.Collections;

public class LoginEventHandler : MonoBehaviour {
	private GameObject player;
	private PlayerSyncModule scriptPlayerHandler;
	// Use this for initialization
	void Start () {
		player = GameObject.Find("Player");
		scriptPlayerHandler = (PlayerSyncModule) player.transform.GetChild(1).GetComponent("PlayerSyncModule");
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
		string text = msg.getNextString();
		float x = msg.getNextFloat();
		float y = msg.getNextFloat();
		float z = msg.getNextFloat();

        Console.Write(text);
        Console.Write(x+"");
        Console.Write(y+"");
        Console.Write(z+"");
		
		
		scriptPlayerHandler.nextPosition(x, y, z);
		
		Console.Write("Login Successful");
        MessageBuilder mb = new MessageBuilder(MessageType.LOGIN_SUCCESSFUL);
		TCPHandler.getInstance().send(mb.Build());
		
		scriptPlayerHandler.login = true;
		
		
		
    }
}
