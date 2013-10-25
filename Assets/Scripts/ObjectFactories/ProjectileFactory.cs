using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ProjectileFactory : MonoBehaviour {

    private static LinkedList<ProjectileEntity> projectiles = new LinkedList<ProjectileEntity>();
    public GameObject prefab;

	// Use this for initialization
	void Start () {
        ProjectileEvent.onShootingSlaveSimple += OnShootingSlaveSimple;
	}
	
	// Update is called once per frame
	void Update () {
        foreach (ProjectileEntity entity in projectiles)
        {
            if (!entity.Instantiated) 
            {
                HumanEntity humanEntity = HumanEntityFactory.GetEntity(entity.Owner);
                humanEntity.Rotation = entity.Rotation;
                entity.Position = humanEntity.Position;

                ((ProjectileEntitySync)((GameObject)
                    GameObject.Instantiate(prefab)).GetComponent("ProjectileEntitySync")
                    ).Entity = entity;
                entity.Instantiated = true;
                /*
            Instantiate(blaster,
                position,
                Quaternion.Euler(
                    cameraHeadTransform.eulerAngles.x + 90,
                    transform.eulerAngles.y,
                    0
                ));*/
            }
            
        }
	}

    void OnShootingSlaveSimple(Message msg)
    {
        projectiles.AddLast(new ProjectileEntity(msg.getNextString(),msg.getNextQuartenion(),msg.getNextString(),false));
    }

    public static void CreateProjectile(HumanEntity humanEntity,string type)
    {
        projectiles.AddLast(new ProjectileEntity(humanEntity.ID, humanEntity.Rotation, type, false));
    }
}
