using UnityEngine;
using System;
using System.Collections;

public class PlayerSync : MonoBehaviour {
	private TCPHandler tcpHandler = null;
	public bool login = false;
	private GameObject playerObj = null;
    private Vector3 positionOverride = Vector3.zero;
    private HumanEntity entity = new HumanEntity("sd ");
	// Use this for initialization
	void Start () 
	{
		
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
        entity.Position = transform.position;
        entity.Rotation = transform.rotation;
	}

	public void OnLoginInfo(Message msg)
    {
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
            mb.Add(playerObj.transform.rotation.x);
            mb.Add(playerObj.transform.rotation.y);
            mb.Add(playerObj.transform.rotation.z);
            mb.Add(playerObj.transform.rotation.w);

//Debug.Log(playerObj.transform.rotation.x+" - "+playerObj.transform.rotation.y+" - "+playerObj.transform.rotation.z+" - "+playerObj.transform.rotation.w);
			TCPHandler.getInstance().send(mb.Build());
		}
    }

    public HumanEntity Entity 
    {
        get { return entity; }
    }

}
