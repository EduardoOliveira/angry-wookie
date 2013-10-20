using UnityEngine;
using System.Collections;

public class Console : MonoBehaviour {
	
	private Vector2 scrollPosition;
	private static string consoleText="";
	private string inputText="";
	private bool focus = false;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnGUI () {
		GUI.enabled = false;
		GUI.SetNextControlName("Console");
		consoleText = GUI.TextArea (new Rect (10, Screen.height - 10 - 500 - 40, 400, 500), consoleText);
		GUI.enabled = true;
		GUI.SetNextControlName("ConsoleInput");
		inputText = GUI.TextField (new Rect(10,Screen.height - 10 - 30,400, 30), inputText,100);
		
		if (Event.current.type == EventType.KeyDown && Event.current.character == '\n'){
			if(!focus){
				GUI.FocusControl("ConsoleInput");
				focus=true;
			}else{
				Write(inputText);
				handleCommand(inputText);
				focus=false;
				GUI.FocusControl("");
				inputText = "";
			}
		}	
	}

	void handleCommand(string text){
		if(Command.isCommand(text)){
			Command cmd = new Command(text);
			Write(cmd.ToString());
			switch(cmd.Main){
				case "server": 
					//networkHandler.handleNetworkCommand(cmd);
					break;
				case "console":
					HandleConsoleCommand(cmd);
					break;
				case "exit":
					Write (">exiting");
					Application.Quit();
					break;
				case "spawn":
					//PlayerEvents.fireOnCreatePlayer("qwe");
					break;
				case "session":
//					PlayerEvents.fireOnCreatePlayer("qwe");
					Console.Write("session");
					SessionModule.HandleCommand(cmd);
					break;
				default:
					Write("chat");
					//networkHandler.handleChat(text);
					break;
			}
		}
	}

	public void clearConsole(){
		Write (">Clear");
		consoleText="";
	}

	private void HandleConsoleCommand(Command cmd){
		switch(cmd.Sub){
		case "clear":
			clearConsole();
			break;
		}
	}

	public static void Write(string text){
		if(!text.Equals(""))
			consoleText+= "\n"+text;
	}
}
