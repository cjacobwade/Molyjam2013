using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	
	//Movement
		CharacterController controller;
		public Vector3 moveDirection;//private
		public int moveSpeed;
		public float ySpeed;//private
		public float gravitySpeed;
		bool isMoving = false;
		//Jumping
			public int jumpSpeed;
			public float maxJump;
			public float jumpTime;
			bool isJumping = false;
		//Crouching
			public float crouchSpeed;
			public float crouchMoveSpeed;
			public float crouchHeight;
			float controllerHeight;
			bool isCrouching = false;
		//Death
			bool brighten = false;
			public float deathHeight;
			public int deathRotateSpeed;
	
	//Camera Control
		public int rotateSpeed;
		public float shakeStrength;
		//Up/Down
			public float cameraV;//how much are we currently rotating (private)
			float cameraRotationV;
			public float minCameraV;
			public float maxCameraV;
		//Left/Right
			public float cameraH;	
	//Audio
		public AudioClip[] sounds;
	
	//GameObjects
		public GameObject view;
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
		view.transform.Rotate(new Vector3(0,Time.deltaTime*cameraH*rotateSpeed,0));
		view.transform.rotation = Quaternion.Euler(view.transform.rotation.eulerAngles.x,view.transform.rotation.eulerAngles.y,0);
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
			view.transform.Rotate(new Vector3(Time.deltaTime*cameraV*-rotateSpeed/*change this to positive for reversed axis*/,0,0));
		}
		if(cameraRotationV < minCameraV)//if lower than min rotation, correct
		{
			cameraRotationV += 1;
			view.transform.Rotate(new Vector3(Time.deltaTime*cameraV*-rotateSpeed,0,0));
		}
		if(cameraRotationV > maxCameraV)//if higher than max rotation, correct
		{
			cameraRotationV -= 1;
			view.transform.Rotate(new Vector3(Time.deltaTime*cameraV*-rotateSpeed,0,0));
		}	
	}
	
	void Movement()
	{
		view.transform.position = transform.position;
		//Horizontal and Vertical axis are controlled by wasd or arrows
		moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0,Input.GetAxis("Vertical"));
        moveDirection = view.transform.TransformDirection(new Vector3(moveDirection.x,transform.eulerAngles.y, moveDirection.z));
		if(isCrouching)
			moveDirection *= crouchMoveSpeed;
		else
        	moveDirection *= moveSpeed;	
		moveDirection = new Vector3(moveDirection.x,ySpeed,moveDirection.z);
		SoundControl();
		if(controller.isGrounded)
		{
			if(isJumping)
			{
				if(!audio.isPlaying)
					PlaySound(2,.7f);
			}
			jumpTime = 0;
			isJumping = false;
			Jump();
			Crouch();
		}
		else
		{
			Dying();
			if(ySpeed > -9.8)
				ySpeed += gravitySpeed;
		}
	}
	
	void SoundControl()
	{
		if(Input.GetAxis("Vertical") != 0 || Input.GetAxis("Horizontal") != 0)
		{
			if(!isJumping)
			{
				if(isCrouching)
				{
					if(!audio.isPlaying)
						PlaySound(1,.6f);
				}
				else
				{
					if(!audio.isPlaying)
						PlaySound(0,.6f);
				}
			}
		}
		else
			if(audio.clip.name != "Landing")
				audio.Stop();
	}
	
	void Dying()
	{
		if(jumpTime < maxJump)
			jumpTime += 1*Time.deltaTime;
		else
		{
			
			moveDirection *= 0;
			rotateSpeed = deathRotateSpeed;
			if(controller.height > deathHeight)
				controller.height -= crouchSpeed*Time.deltaTime;
			if(!brighten)
			{
				view.transform.Translate(Random.Range(-shakeStrength, shakeStrength)*3,Random.Range(-shakeStrength, shakeStrength)*2,0);
				StartCoroutine(Death(5));
				view.light.range += .2f;
				view.light.intensity += .1f;
			}
			
		}
	}
	
	IEnumerator Death(float waitTime)
	{
		yield return new WaitForSeconds(waitTime);
		brighten = true;
		yield return new WaitForSeconds(waitTime*.3f);
		Application.LoadLevel(Application.loadedLevel);
	}
	
	void Jump()
	{
		if(Input.GetButton("Jump"))
		{
			audio.Stop();
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
	
	void PlaySound(int index, float volume)
	{
		audio.volume = volume;
		audio.clip = sounds[index];
		audio.Play();
	}
}
