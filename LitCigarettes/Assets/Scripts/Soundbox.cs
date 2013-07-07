using UnityEngine;
using System.Collections;

public class Soundbox : MonoBehaviour {
	
	public AudioClip[] sounds;
	public AudioSource sounder;
	bool soundPlayed = false;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.tag=="Player")
		{
			if(!soundPlayed)
			{
				PlaySound(0,1f);
				soundPlayed = true;
			}
		}
	}
	
	void PlaySound(int index, float volume)
	{
		sounder.volume = volume;
		sounder.clip = sounds[index];
		sounder.Play();
	}
}
