using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	
	Vector3 moveDirection;
	public int moveSpeed;
	
	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{
		MovementInput();
		CameraControl();
		transform.Translate(moveDirection*Time.deltaTime);
	}
	
	void CameraControl()
	{
		
	}
	
	void MovementInput()
	{
		#region WASD
			if(Input.GetKey(KeyCode.W))
				transform.Translate(new Vector3(0,0,moveSpeed)*Time.deltaTime);
			
			if(Input.GetKey(KeyCode.S))
				transform.Translate(new Vector3(0,0,-moveSpeed*.8f)*Time.deltaTime);
			
			if(Input.GetKey(KeyCode.A))
				transform.Translate(new Vector3(-moveSpeed*.7f,0,0)*Time.deltaTime);	
			
			if(Input.GetKey(KeyCode.D))
				transform.Translate(new Vector3(moveSpeed*.7f,0,0)*Time.deltaTime);
		#endregion
	}
}
