using UnityEngine;
using System;
using System.Collections;

public class PlayerSync : MonoBehaviour {
	private TCPHandler tcpHandler = null;
	public bool login = false;
	private GameObject playerObj = null;
    private Vector3 positionOverride = Vector3.zero;
	// Use this for initialization
	void Start () 
	{
		this.tcpHandler = TCPHandler.getInstance();
		playerObj = gameObject.transform.parent.gameObject;
		LoginEvent.onLoginInfo += OnLoginInfo;
	}
	
	// Update is called once per frame
	void Update () 
	{
        if (positionOverride != Vector3.zero) 
        {
            playerObj.transform.position = positionOverride;
            positionOverride = Vector3.zero;
        }
	}

	public void OnLoginInfo(Message msg)
    {
        Console.Write("login");
        msg.getNextString();
        positionOverride = new Vector3(msg.getNextFloat(), msg.getNextFloat(), msg.getNextFloat());
        login = true;
	}
	
    void FixedUpdate()
    {
		if(login)
		{
			MessageBuilder mb = new MessageBuilder(MessageType.MOVEMENT_SYNC);
            mb.Add(playerObj.transform.position.x);
            mb.Add(playerObj.transform.position.y);
            mb.Add(playerObj.transform.position.z);
			tcpHandler.send(mb.Build());
		}
    }

}
