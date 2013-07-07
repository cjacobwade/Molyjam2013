using UnityEngine;
using System.Collections;

public class Lamp : MonoBehaviour {
	
	GameObject bulb;
	GameObject glow;
	public Material lightOn;
	public Material lightOff;
	public int index;
	bool isFlicker = false;
	// Use this for initialization
	void Start () 
	{
		
		glow = GameObject.Find("lamp" + index +"/light");
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(glow.light.enabled)
		{
			if(!isFlicker)
				StartCoroutine(Flicker());
		}
	}
	
	IEnumerator Flicker()
	{
		isFlicker = true;
		yield return new WaitForSeconds(Random.Range(1,2));
		glow.light.intensity = Random.Range(.1f,.4f);
		StartCoroutine(Flicker());
		isFlicker = false;
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
