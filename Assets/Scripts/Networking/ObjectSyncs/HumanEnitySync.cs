using UnityEngine;
using System.Collections;

public class HumanEnitySync : MonoBehaviour {

    public GameObject humanEntity;


	// Use this for initialization
	void Start () 
    {
	    
	}
	
	// Update is called once per frame
	void Update () 
    {
	
	}

    public void MoveTo(Vector3 position)
    {
        humanEntity.transform.position = position;
    }
}
