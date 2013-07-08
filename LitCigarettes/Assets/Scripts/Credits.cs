using UnityEngine;
using System.Collections;

public class Credits : MonoBehaviour {

	public float scrollSpeed;
	public GameObject view;
	bool isScroll = false;
	
	// Use this for initialization
	void Start () 
	{
		StartCoroutine(Scroll(3));
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(isScroll)
			view.transform.Translate(0,-scrollSpeed*Time.deltaTime,0);
		
		if(Input.anyKey)
			Application.LoadLevel("tutorial");
	}
	
	IEnumerator Scroll(float waitTime)
	{
		print ("go");
		yield return new WaitForSeconds (waitTime);
		isScroll = true;
	}
}
