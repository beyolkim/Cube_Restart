using UnityEngine;
using System.Collections;

public class MoveInHexagon : MonoBehaviour {

	// Use this for initialization
	public float distance=10f;
	public float divisor;

	public Vector3 newPoint,dirVector;

	void Start () 
	{
		newPoint=transform.position+distance*transform.forward;
	}
	
	// Update is called once per frame
	void Update () 
	{
		Vector3 position =transform.position;
		dirVector=newPoint-position;

		if( dirVector.magnitude > 1e-4)
		{
			transform.position+=distance/divisor*transform.forward;
		}
		else
		{
			giveNewPoint();
		}
	}


	void giveNewPoint()
	{

		float angle=Random.Range(-60,60);

		if( Mathf.Abs(angle-60) < Mathf.Abs(angle+60) )
		{
			angle=60;
		}
		else
		{
			angle=-60;
		}

		transform.rotation*=Quaternion.Euler(0,angle,0);

		Vector3 newOffset=distance*transform.forward;
		newPoint=transform.position+newOffset;


	}
}
