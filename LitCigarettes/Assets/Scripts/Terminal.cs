using UnityEngine;
using System.Collections;

public class Terminal : MonoBehaviour {
	
	public Color[] glowColor;
	public AudioClip[] sounds;
	bool soundPlaying;
	public Texture2D[] activated;
	public Texture2D[] deactivated;
	public Texture2D[] hud;
	public float maxDistance;//how close does the player have to be to activate the terminal
	bool isActive = false;//on or off
	
	//Object refs
		GameObject hudPlane;
		public GameObject player;
		GameObject screenGlow;
		public GameObject[] target;
	
	// Use this for initialization
	void Start () 
	{
		hudPlane = GameObject.Find("HUD");
		screenGlow = GameObject.Find("Screenglow");
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(Vector3.Distance(player.transform.position,transform.position) < maxDistance)
		{
			hudPlane.SetActive(true);
			if(Input.GetKey(KeyCode.E))
			{
				if(!soundPlaying)
				{
					if(isActive)
						StartCoroutine(Deactivate(6.5f));
					else
						StartCoroutine(Activate(3.5f));
					soundPlaying=true;
				}
			}
		}
		else
			hudPlane.SetActive(false);
	}
	
	void MessageTargets(string function)
	{
		for(int i = 0;i<target.Length;i++)
			target[i].SendMessage(function);
	}
	
	IEnumerator Activate(float waitTime)
	{
		PlaySound(0,.3f);
		yield return new WaitForSeconds(waitTime);
		renderer.material.SetTexture("_MainTex",activated[0]);
		renderer.material.SetTexture("_Detail",activated[1]);
		hudPlane.renderer.material.SetTexture("_MainTex",hud[0]);
		screenGlow.light.color = glowColor[0];
		MessageTargets("Activate");
		isActive = true;
		soundPlaying=false;
	}
	
	IEnumerator Deactivate(float waitTime)
	{
		PlaySound(1,.05f);
		yield return new WaitForSeconds(waitTime);
		renderer.material.SetTexture("_MainTex",deactivated[0]);
		renderer.material.SetTexture("_Detail",deactivated[1]);
		hudPlane.renderer.material.SetTexture("_MainTex",hud[1]);
		screenGlow.light.color = glowColor[1];
		MessageTargets("Deactivate");
		isActive = false;
		soundPlaying=false;
	}
	
	void PlaySound(int index, float volume)
	{
		audio.volume = volume;
		audio.clip = sounds[index];
		audio.Play();
	}
}
