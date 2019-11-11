using UnityEngine;
using System.Collections;

public class AutoResize: MonoBehaviour {

	public float speed;
	public float maxSize;
	public float initialSize=0.03f;


	// Use this for initialization
	void Start () 
	{
		transform.localScale=new Vector3(initialSize,0,0);
	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{
		float localScale=transform.localScale.magnitude;

		if(localScale<maxSize)
		{

			transform.localScale+=speed*new Vector3(0,1,0);
		}


	}
}
