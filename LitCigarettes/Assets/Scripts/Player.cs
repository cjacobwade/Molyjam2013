using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	
	//Movement
	CharacterController controller;
	public int moveSpeed;
	public Vector3 moveDirection;
	public int jumpSpeed;
	public float gravitySpeed;
	public float ySpeed;
	//Camera Control
	public int rotateSpeed;
		//Up/Down
			public float cameraV;//how much are we currently rotating
			float cameraRotationV;
			public float minCameraV;
			public float maxCameraV;
		//Left/Right
			public float cameraH;
	
	
	
	
	

	
	
	// Use this for initialization
	void Start () 
	{
		controller = GetComponent<CharacterController>();
	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{
		//MovementInput();
		Movement();
		MouseInput();
		if (Input.GetKeyDown(KeyCode.Escape))
        	Screen.lockCursor = false;
		//transform.Translate(moveDirection*Time.deltaTime);
		controller.Move(moveDirection*Time.deltaTime);
	}
	
	void MouseInput()
	{
		LeftMouse();
		CameraHorizontal();
		CameraVertical();
	}
	
	void LeftMouse()
	{
		if(Input.GetMouseButtonDown(0))
		{
			Screen.lockCursor = true;
			Screen.showCursor = false;
		}
	}
	
	void CameraHorizontal()
	{
		cameraH = Input.GetAxis("Mouse X");
		transform.Rotate(new Vector3(0,Time.deltaTime*cameraH*rotateSpeed,0));
		transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x,transform.rotation.eulerAngles.y,0);
	}
	
	void CameraVertical()
	{
		if(Input.GetAxis("Mouse Y")>rotateSpeed)
			cameraV=rotateSpeed;
		else if(Input.GetAxis("Mouse Y")<-rotateSpeed)
			cameraV=-rotateSpeed;
		else
			cameraV = Input.GetAxis("Mouse Y");
		
		if(cameraRotationV >= minCameraV && cameraRotationV <= maxCameraV)
		{
			cameraRotationV += cameraV;
			transform.Rotate(new Vector3(Time.deltaTime*cameraV*-rotateSpeed/*change this to positive for reversed axis*/,0,0));
		}
		if(cameraRotationV < minCameraV)//if lower than min rotation, correct
		{
			cameraRotationV += 1;
			transform.Rotate(new Vector3(Time.deltaTime*cameraV*-rotateSpeed,0,0));
		}
		if(cameraRotationV > maxCameraV)//if higher than max rotation, correct
		{
			cameraRotationV -= 1;
			transform.Rotate(new Vector3(Time.deltaTime*cameraV*-rotateSpeed,0,0));
		}	
	}
	
	void Movement()
	{
		//Horizontal and Vertical axis are controlled by wasd or arrows
		moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0,Input.GetAxis("Vertical"));
        moveDirection = transform.TransformDirection(moveDirection);
        moveDirection *= moveSpeed;	
		moveDirection = new Vector3(moveDirection.x,ySpeed,moveDirection.z);
		if(controller.isGrounded)
		{
			if(Input.GetButton("Jump"))
			{
				ySpeed = 0;
				ySpeed += jumpSpeed;
			}
			else
				moveDirection.y = 0;
		}
		else
		{
			if(ySpeed > -9.8)
				ySpeed += gravitySpeed;
		}
	}
}
