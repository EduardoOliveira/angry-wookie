using UnityEngine;
using System;
using System.Collections;

public class PlayerSyncModule : MonoBehaviour {
	private TCPHandler tcpHandler = null;
	private Vector3 nextPos;
	private bool needChange = false;
	public bool login = false;
	private GameObject parent = null;

	// Use this for initialization
	void Start () 
	{
		this.tcpHandler = TCPHandler.getInstance();
		parent = gameObject.transform.parent.gameObject;
		LoginEvent.onLoginInfo += OnLoginInfo;
	}
	
	// Update is called once per frame
	void Update () 
	{

	}
	public void OnLoginInfo(Message msg)
    {
			
	}
	
	
	public void nextPosition(float x, float y, float z)
	{
		nextPos = new Vector3(x, y, z);
		needChange = true;
	}
	
	public void Change()
	{
		if(needChange)
		{
		parent.transform.position = nextPos;
		needChange = false;
		}
	}

    void FixedUpdate()
    {
		
		Change();
		if(login)
		{
			MessageBuilder mb = new MessageBuilder(MessageType.MOVEMENT_SYNC);
			mb.Add(parent.transform.position.x);
			mb.Add(parent.transform.position.y);
			mb.Add(parent.transform.position.z);
			tcpHandler.send(mb.Build());
		}
        /*this.tcpHandler.send(
            new MessageBuilder(MessageType.LOGIN)
                .Add(player.transform.position.x)
                .Add(player.transform.position.y)
                .Add(player.transform.position.z)
                .Build()
        );*/
    }


}
