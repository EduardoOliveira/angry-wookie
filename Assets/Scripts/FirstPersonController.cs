using UnityEngine;
using System.Collections;

public class FirstPersonController : MonoBehaviour {
	
	public float movementSpeed = 5.0f;
	public float mouseSensitivity = 2.0f;
	
	public float upDownRange = 60.0f;
	float verticalRotation = 0.0f;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		//Rotation
		float rotX = Input.GetAxis("Mouse X")*mouseSensitivity;
		transform.Rotate(0, rotX, 0);
		
		verticalRotation -= Input.GetAxis("Mouse Y")*mouseSensitivity;
		verticalRotation = Mathf.Clamp(verticalRotation, -upDownRange, upDownRange);
		Camera.main.transform.localRotation = Quaternion.Euler(verticalRotation, 0, 0);
		
		//Movement
		float forwardSpeed = Input.GetAxis("Vertical") * movementSpeed;
		float sidewaysSpeed = Input.GetAxis("Horizontal") * movementSpeed;
		
		Vector3 speed = new Vector3(sidewaysSpeed, 0, forwardSpeed);
		
		speed = transform.rotation * speed;
		
		CharacterController charCtrl = GetComponent<CharacterController>();
		
		charCtrl.SimpleMove(speed);
		
	}
}
