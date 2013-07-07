using UnityEngine;
using System.Collections;

public class Rotation : MonoBehaviour {

	public float rotateSpeed;
	bool isActive = false;
	
	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(isActive)
			transform.Rotate(rotateSpeed,0,0);
	}
	
	void Activate()
	{
		isActive = true;
	}
	
	void Deactivate()
	{
		isActive = false;
	}
}
