using UnityEngine;
using System.Collections;

public class ProjectileEntitySync : MonoBehaviour {
    public GameObject prefab;
    private ProjectileEntity entity;

    private float speed = 1;
    private bool expended = false;
    private RaycastHit hit;
    private float range = 1.5f;
    private float expireTimer = 20;

    public ProjectileEntity Entity
    {
        get { return entity; }
        set { entity = value; }
    }

    // Use this for initialization
    void Start()
    {
        transform.position = entity.Position;
        StartCoroutine(expire());
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);

        if (Physics.Raycast(transform.position, transform.up, out hit, range) &&
            !expended)
        {
            if (hit.transform.tag == "Floor")
            {
                expended = true;
                //Instantiate(blasterExplosion, hit.point, Quaternion.identity);
                transform.renderer.enabled = false;
                transform.light.enabled = false;
            }

        }
    }

    IEnumerator expire()
    {
        yield return new WaitForSeconds(expireTimer);
        Destroy(transform.gameObject);
    }
    
}
