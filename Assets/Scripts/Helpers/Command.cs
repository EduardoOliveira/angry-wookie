using UnityEngine;
using System.Collections;
using System.Linq;
using System;

public class Command{

	private static string[] mainCommands = {
		"server","console","exit","spawn","session"
	};

	private string main="";
	private string sub="";
	private string[] parameters = {};

	public string Main{
		get{ return main; }
	}
	
	public string Sub{
		get{ return sub; }
	}
	
	public string[] Parameters{
		get{return parameters;}
	}
	
	public Command(string command){
		string[] splinters = command.Split(' ');
		main = splinters[0].ToLower();
		if(splinters.Length>1)
			sub = splinters[1].ToLower();
		if(splinters.Length>2)
			parameters = splinters.Skip(2).ToArray();

	}

	public static bool isCommand(string text){
		return mainCommands.Contains(text.Split(' ')[0].ToLower());
	}
	
	public string ToString(){
		return main+" "+sub+String.Join(" ",parameters);
	}

}
