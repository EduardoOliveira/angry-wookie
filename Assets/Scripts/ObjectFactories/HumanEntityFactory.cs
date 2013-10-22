using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HumanEntityFactory : MonoBehaviour 
{
	
	private static HumanEnitySync players;
    private static HumanEntity entityList = null;
    private Message message = null;
	public GameObject player = null;
    public GameObject prefab = null;
	
	void Start () 
	{
		PlayerEvent.onPlayerSync += onPlayerSync;
	}
	
	void onPlayerSync(Message message)
	{
        this.message = message;
        string id = message.getNextString();

        Vector3 pos = new Vector3(message.getNextFloat(), message.getNextFloat(), message.getNextFloat());
        if (entityList == null)
            entityList = new HumanEntity(id,pos);
      
		entityList.Position = pos;
		Debug.Log("New pos: " + pos);
	}
	
    void Update()
    {

        if(entityList != null)
        {
			HumanEntity entity = entityList;
            if (!entity.Instatiated)
            {
                if (players == null)
                {
                    players = (HumanEnitySync)((GameObject)Instantiate(prefab)).GetComponent("HumanEnitySync");
					
					
                    Debug.Log("Entity: " + entity + "(" + entity.ID + ")");
                    players.SetEntity(entity);
                    entity.Instatiated = true;
                }
            }
            //players[entity.ID].MoveTo(new Vector3(message.getNextFloat(), message.getNextFloat(), message.getNextFloat()));
		}
		
        
	}
}
