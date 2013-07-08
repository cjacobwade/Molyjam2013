using UnityEngine;
using System.Collections;

public class Spike : MonoBehaviour {
	
	public AudioClip[] sounds;
	public GameObject tailLight;
	public float brightTime;//how long till the lights start to dim
	public float dimRate;
	public float colorRate;
	bool isDimming = false;
	
	// Use this for initialization
	void Start () 
	{
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(isDimming)
		{
			tailLight.light.intensity -= dimRate/2*Time.deltaTime;
			tailLight.light.range -= dimRate*Time.deltaTime;
			renderer.material.color -= new Color(colorRate,colorRate,colorRate,colorRate)*Time.deltaTime;
		}
//		if(tailLight.light.intensity <= 0 && tailLight.light.intensity <=0)
//			renderer.material.color = new Color(0,0,0,0);
	}
	
	void OnCollisionEnter()
	{
		PlaySound(0,1);
		StartCoroutine(Dimmer(brightTime));
	}
	
	IEnumerator Dimmer(float waitTime)
	{
		rigidbody.isKinematic = true;
		yield return new WaitForSeconds(waitTime);
		isDimming = true;
	}
	
	void PlaySound(int index, float volume)
	{
		audio.volume = volume;
		audio.clip = sounds[index];
		audio.Play();
	}
}
