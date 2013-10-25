using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HumanEntityFactory : MonoBehaviour 
{
	
	private static Dictionary<string,HumanEntitySync> players = new Dictionary<string,HumanEntitySync>();
    private static Dictionary<string, HumanEntity> entityList = new Dictionary<string, HumanEntity>();
	public GameObject player = null;
    private HumanEntity playerEntity;
    public GameObject prefab = null;
	
	void Start () 
	{
        this.playerEntity = ((PlayerSync)transform.root.GetComponentInChildren<PlayerSync>()).Entity;
        entityList.Add(this.playerEntity.ID, this.playerEntity);
        PlayerEvent.onPlayerSync += onPlayerSync;
	}
	
	void onPlayerSync(Message message)
	{
        string id = message.getNextString();
        if(!entityList.ContainsKey(id))
        {
            entityList.Add(id,new HumanEntity(id));
        }

        entityList[id].Position = new Vector3(message.getNextFloat(), message.getNextFloat(), message.getNextFloat());
        entityList[id].Rotation = new Quaternion(message.getNextFloat(),
            message.getNextFloat(), 
            message.getNextFloat(), 
            message.getNextFloat()
            );
	}
	
    void Update()
    {

        foreach (HumanEntity entity in entityList.Values)
        {
            if (!players.ContainsKey(entity.ID))
            {
                players.Add(entity.ID, (HumanEntitySync)((GameObject)Instantiate(prefab)).GetComponent("HumanEntitySync"));
                entity.Instatiated = true;
            }
            players[entity.ID].Entity = entity;
        }
        
	}

    public static HumanEntity GetEntity(string id)
    {
        return entityList[id];
    }
}
