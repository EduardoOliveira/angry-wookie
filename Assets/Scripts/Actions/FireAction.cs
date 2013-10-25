using UnityEngine;
using System.Collections;

public class FireAction : MonoBehaviour {
    private float fireRate = 0.2f;
    private float nextFire = 0;
    private HumanEntity humanEntity;
	// Use this for initialization
	void Start () {

        this.humanEntity = ((PlayerSync)transform.root.GetComponentInChildren<PlayerSync>()).Entity;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButton("Fire1") && Time.time > nextFire)
        {
            MessageBuilder msg = new MessageBuilder(MessageType.SHOOTING_MASTER_SIMPLE);
            msg.Add(humanEntity.Position.x).Add(humanEntity.Position.y).Add(humanEntity.Position.z);
            msg.Add(humanEntity.Rotation.x).Add(humanEntity.Rotation.y).Add(humanEntity.Rotation.z).Add(humanEntity.Rotation.w);
            msg.Add("qwe");
            Debug.Log("123123");
            TCPHandler.getInstance().send(msg.Build());
            
            nextFire = Time.time + fireRate;

            Vector3 position = transform.TransformPoint(0, 0, 0.2f);

            ProjectileFactory.CreateProjectile(humanEntity, "qwe");

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
