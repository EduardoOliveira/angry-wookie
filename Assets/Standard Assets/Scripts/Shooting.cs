using UnityEngine;
using System.Collections;

class Shooting : MonoBehaviour {
    public int ClipSize = 20;

    public bool SingleShot = false;
	public double FireRate = 0.0;
    public bool hasScope = false;
    public ParticleEmitter hitParticles = null;
    public ParticleEmitter muzzleFlash = null;
    public GameObject bulletTextures = null;
	public Texture2D crosshairTex = null;
	public string ZoomIn = null;
	public string ZoomOut = null;
	
	private int BulletsLeft = 20; 
	private Rect position = new Rect(0,0,0,0);
	private bool scoped = false;
	private bool aiming = false;
    private double nextFireTime = 0.0;
    
    
    
    void Start () 
    {
	Screen.showCursor = false;
		
    // We don't want to emit particles all the time, only when we hit something.
    if (hitParticles)
    hitParticles.emit = false;
    
    if(muzzleFlash)
   	muzzleFlash.emit = false;
    }
     
    void Recoil()
    {
    
    }
    
    void ZoomInFunction(GameObject parent)
    {
       	parent.animation.Play(ZoomIn);
    	parent.animation[ZoomIn].layer = 4;
    }
    
    void ZoomOutFunction(GameObject parent)
    {
    	parent.animation.Play(ZoomOut);
    	parent.animation[ZoomOut].layer = 4;
    }
    
    void CreateCrossHair(GameObject parent)
    {
    	for(int i = 0; i < parent.transform.childCount; i++)
   		{
   			Transform item = parent.transform.GetChild(i);
   			if(item.GetComponent<MeshRenderer>()!=null)
   				item.GetComponent<MeshRenderer>().enabled = false;
   		}
    	position = new Rect(0, 0, Screen.width, Screen.height);
    	scoped = !scoped;
    }
    
    void RemoveCrossHair(GameObject parent)
    {
    	for(int i = 0; i < parent.transform.childCount; i++)
   		{
   			Transform item = parent.transform.GetChild(i);
   			if(item.GetComponent<MeshRenderer>()!=null)
   				item.GetComponent<MeshRenderer>().enabled = true;
   		}
    	position = new Rect(0, 0, Screen.width, Screen.height);
    	scoped = !scoped;
    }
    
    void Aim()
    {
	    GameObject parent = gameObject.transform.parent.gameObject;
    	if(!aiming)
    	{
			ZoomInFunction(parent);
			if(hasScope)
				CreateCrossHair(parent);
    	}
    	else
    	{
			ZoomOutFunction(parent);
			if(hasScope)
				RemoveCrossHair(parent);
    	}
		aiming = !aiming;
    }
     
    void FireOneShot () {
		
		
    if(CanShoot()){
	    nextFireTime = Time.time + FireRate;
	    BulletsLeft--;
	    if(muzzleFlash&&!scoped){
	    muzzleFlash.Emit();
	    }
	    Vector3 direction = transform.TransformDirection(Vector3.forward);
	    RaycastHit hit = new RaycastHit();
			
	    // Did we hit anything?
	    if (Physics.Raycast (transform.position, direction, out hit)) {
		    
				// Apply a force to the rigidbody we hit
		    if (hit.rigidbody)
		    hit.rigidbody.AddForceAtPosition(direction, hit.point);
		    
		    Instantiate(bulletTextures, hit.point, Quaternion.FromToRotation(Vector3.up, hit.normal));
		    // Place the particle system for spawing out of place where we hit the surface!
		    // And spawn a couple of particles
		    if (hitParticles) {
		    hitParticles.transform.position = hit.point;
		    hitParticles.transform.rotation = Quaternion.FromToRotation(Vector3.up, hit.normal);
		    hitParticles.Emit();
		    }
	     
	   }
	}

    }
	
    
    bool CanShoot(){
    	return BulletsLeft>0 && Time.time > nextFireTime; 
    }
    
    void Reload(){
    	BulletsLeft = ClipSize;
    	
    }
     
    void OnGUI(){
    	if(scoped)
 		{
			GUI.DrawTexture(position, crosshairTex);
		}
    }
     
    void Update (){
    if(SingleShot){
	    if(Input.GetMouseButtonDown(0)){
    		FireOneShot();
    	}
    }else{
    	if(Input.GetMouseButton(0)){
    		FireOneShot();
    	}
    }
    if(Input.GetKeyDown("r")){
    	Reload();
    }
    
    if(Input.GetMouseButtonDown(1)){
    	Aim();
    }
    
    }
    
    
    
    
}
