using UnityEngine;
using System.Collections;
using System;

public class SessionModule {
	private string token = "";
	private static SessionModule instance = null;

	public static void HandleCommand(Command cmd){
		SessionModule.getInstance();
		Console.Write(cmd.Sub);
		switch(cmd.Sub){
			case "login":
				LogIn(cmd);
				break;
			case "logout":
				LogOut(cmd);
				break;
		}

	}
	

	private static void LogIn(Command cmd){
		string str = cmd.Parameters[0];
		instance.token = str;
		Console.Write("msg"+str);
        MessageBuilder mb = new MessageBuilder(MessageType.LOGIN);
        mb.Add(str);
        TCPHandler.getInstance().send(mb.Build());
	}
	
	private static void LogInSuccess()
	{
			
	}

	private static void LogOut(Command cmd){

	}

	public static SessionModule getInstance(){
		return (instance==null?(instance=new SessionModule()):instance);
	}

	private SessionModule(){}

}
