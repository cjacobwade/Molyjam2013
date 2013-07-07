using UnityEngine;
using System.Collections;

public class Lamp : MonoBehaviour {
	
	GameObject bulb;
	GameObject glow;
	public Material lightOn;
	public Material lightOff;
	public int index;
	// Use this for initialization
	void Start () 
	{
		glow = GameObject.Find("lamp" + index +"/light");
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}
	
	void Activate()
	{
		//bulb.renderer.material = lightOn;
		glow.light.enabled = true;
	}
	
	void Deactivate()
	{
		//bulb.renderer.material = lightOff;
		glow.light.enabled = false;
	}
}
