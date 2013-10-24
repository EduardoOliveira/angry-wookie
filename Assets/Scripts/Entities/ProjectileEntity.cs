using UnityEngine;
using System.Collections;

public class ProjectileEntity {
    private Vector3 position;
    private Quaternion rotation;
    private string owner;
    private string type;
    private bool instantiated;

    public Vector3 Position 
    {
        get { return position; }
        set { position = value; }
    }

    public Quaternion Rotation
    {
        get { return rotation; }
        set { rotation = value; }
    }

    public string Type 
    {
        get { return type; }
        set { type = value; }
    }

    public bool Instantiated
    {
        get { return instantiated; }
        set { instantiated = value; }
    }

    public string Owner
    {
        get { return owner; }
    }

    public ProjectileEntity(string owner, Quaternion rotation, string type,bool instantiated)
    {
        this.owner = owner;
        this.rotation = rotation;
        this.type = type;
        this.instantiated = instantiated;
    }
}
