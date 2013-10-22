using UnityEngine;
using System.Collections;

public class HumanEntity {

    private string name;
    private Vector3 position;
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

    public HumanEntity(string name, Vector3 position)
    {
        this.name = name;
        this.position = position;
    }


    public string ID { get { return name; } set { name = value; } }
}
