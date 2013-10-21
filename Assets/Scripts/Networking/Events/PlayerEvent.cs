using UnityEngine;
using System.Collections;

public class PlayerEvent{
	public delegate void Factory(Message message);
	public static event Factory onPlayerSync;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public static void FireOnPlayerSync(Message msg)
	{
        if (onPlayerSync != null)
            onPlayerSync(msg);
	}
}
