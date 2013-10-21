using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class PlayerFactory : MonoBehaviour 
{
	
	private Dictionary<string,HumanEnitySync> players = new Dictionary<string,HumanEnitySync>();
	public GameObject player = null;
	
	void Start () 
	{
		PlayerEvent.onPlayerSync += onPlayerSync;
	}
	
	void onPlayerSync(Message message)
	{
		string id = message.getNextString();
        Console.Write(id);
		if(!players.ContainsKey(id))
        {
            players[id] = (HumanEnitySync)Instantiate(Resources.Load("ThirdParty"));
        }
        players[id].MoveTo(new Vector3(message.getNextFloat(), message.getNextFloat(), message.getNextFloat()));
	}
}
