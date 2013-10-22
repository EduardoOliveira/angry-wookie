﻿using UnityEngine;
using System.Collections;

public class HumanEnitySync : MonoBehaviour {

    public GameObject humanEntity;
    private HumanEntity entity;

	// Use this for initialization
	void Start () 
    {
	    
	}
	
	// Update is called once per frame
	void Update () 
    {
        if(entity!=null)
		{
			humanEntity.transform.position = entity.Position;
		}
	}

    public HumanEntity Entity { get { return entity; } set { entity = value; } }

    public void SetEntity(HumanEntity entity)
    {
        this.entity = entity;
    }
}
