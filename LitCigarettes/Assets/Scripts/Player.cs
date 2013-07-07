using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	
	//Movement
		CharacterController controller;
		public Vector3 moveDirection;//private
		public int moveSpeed;
		public float ySpeed;//private
		public float gravitySpeed;
		//Jumping
			public int jumpSpeed;
			bool isJumping = false;
		//Crouching
			public float crouchSpeed;
			public float crouchMoveSpeed;
			public float crouchHeight;
			float controllerHeight;
			bool isCrouching = false;
	//Camera Control
		public int rotateSpeed;
		//Up/Down
			public float cameraV;//how much are we currently rotating (private)
			float cameraRotationV;
			public float minCameraV;
			public float maxCameraV;
		//Left/Right
			public float cameraH;	
	
	//GameObjects
		public GameObject model;
	
	// Use this for initialization
	void Start () 
	{
		controller = GetComponent<CharacterController>();
	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{
		Movement();
		MouseInput();
		if(Input.GetKey(KeyCode.R))
			Application.LoadLevel(Application.loadedLevel);
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
			PlayAnimation("Throw",1);
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
		if(isCrouching)
			moveDirection *= crouchMoveSpeed;
		else
        	moveDirection *= moveSpeed;	
		moveDirection = new Vector3(moveDirection.x,ySpeed,moveDirection.z);
		if(Input.GetAxis("Vertical") > 0 || Input.GetAxis("Horizontal") != 0)
		{
			if(!model.animation["Throw"].enabled && !isJumping)
			{
				if(isCrouching)
					PlayAnimation("Walk",.2f);
				else
					PlayAnimation("Walk",.4f);
			}
			else
				model.animation.Stop("Walk");
		}
		else
		{
			if(!model.animation["Throw"].enabled)
				PlayAnimation("Idle",1);
		}
		if(controller.isGrounded)
		{
			isJumping = false;
			Jump();
			Crouch();
		}
		else
		{
			if(ySpeed > -9.8)
				ySpeed += gravitySpeed;
		}
	}
	
	void Jump()
	{
		if(Input.GetButton("Jump"))
		{
			//isCrouching = false;
			isJumping = true;
			ySpeed = 0;
			ySpeed += jumpSpeed;
		}
		else
			moveDirection.y = 0;
	}
	
	void Crouch()
	{
		if(Input.GetKey(KeyCode.LeftShift))
		{
			isCrouching = true;
			if(controller.height > crouchHeight)
				controller.height -= crouchSpeed*Time.deltaTime;
		}
		if(!Input.GetKey(KeyCode.LeftShift) && isCrouching)
		{
			ySpeed = 0;
			ySpeed += jumpSpeed/1.7f;
			controller.height = 1.7f;
			isCrouching = false;
		}
	}
	
	void PlayAnimation(string name,float speed)
	{
		model.animation[name].speed = speed;
		model.animation.Play(name);
	}
}
