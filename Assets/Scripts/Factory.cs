using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class Factory : MonoBehaviour 
{
	
	private Dictionary<string,GameObject> players = new Dictionary<string, GameObject>();
	public GameObject player = null;
	
	void Start () 
	{
		PlayerEvent.onPlayerSync += onPlayerSync;
	}
	
	void onPlayerSync(Message message)
	{
		string id = message.getNextString();
		if(players.ContainsKey(id))
		{
			Vector3 position = new Vector3(message.getNextFloat(),message.getNextFloat(),message.getNextFloat()); 
			Instantiate(player, position, Quaternion.identity);
		}
		else
		{
			GameObject player = players[id];
			PlayerScript script = (PlayerScript)player.GetComponent("PlayerScript");
			script.SyncPlayer();
		}
	}
}
