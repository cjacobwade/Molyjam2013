using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour {
	
	public AudioClip[] sounds;
	public float moveSpeed;
	bool isMoving = false;
	bool isOpen = false;
	
	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(isMoving)
		{
			if(isOpen)
				transform.Translate(moveSpeed*Time.deltaTime,0,0);
			else
				transform.Translate(-moveSpeed*Time.deltaTime,0,0);
		}
	}
	
	void Activate()
	{
		StartCoroutine(Open(4f));
	}
	
	void Deactivate()
	{
		StartCoroutine(Close(4f));
	}
	
	IEnumerator Open(float waitTime)
	{
		isOpen = false;
		isMoving = true;
		PlaySound(0, .9f);
		yield return new WaitForSeconds(waitTime);
		isMoving = false;
	}
	
	IEnumerator Close(float waitTime)
	{
		isOpen = true;
		isMoving = true;
		PlaySound(0, .9f);
		yield return new WaitForSeconds(waitTime);
		isMoving = false;
	}
	
	void PlaySound(int index, float volume)
	{
		audio.volume = volume;
		audio.clip = sounds[index];
		audio.Play();
	}
}
