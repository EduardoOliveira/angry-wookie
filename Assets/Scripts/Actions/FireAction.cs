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
