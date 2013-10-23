using UnityEngine;
using System.Collections;

public class HumanEntity {

    private string name;
    private Vector3 position;
    private Quaternion rotation;
    private bool isInstatiated;

    public Vector3 Position
    {
        get { return position; }
        set { position = value; }
    }
    public bool Instatiated
    {
        get { return isInstatiated; }
        set { isInstatiated = value; }
    }

    public HumanEntity(string name)
    {
        this.name = name;
    }

    public Quaternion Rotation { get { return rotation; } set { rotation = value; } }
    public string ID { get { return name; } set { name = value; } }
}
