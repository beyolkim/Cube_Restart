using UnityEngine;
using System.Collections;

public class MoveInSquares : MonoBehaviour {


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

		float angle=Random.Range(-90,90);

		if( Mathf.Abs(angle-90) < Mathf.Abs(angle+90) )
		{
			angle=90;
		}
		else
		{
			angle=-90;
		}

		transform.rotation*=Quaternion.Euler(0,angle,0);

		Vector3 newOffset=distance*transform.forward;
		newPoint=transform.position+newOffset;


	}
}