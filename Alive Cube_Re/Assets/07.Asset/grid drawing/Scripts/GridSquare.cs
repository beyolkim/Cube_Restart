using UnityEngine;
using System.Collections;

public class GridSquare : MonoBehaviour {

	// Use this for initialization
	public GameObject line;
	public float indx=1;
	public float squareSize;
	public float maxNumberOfLines;
	public float elapsed,increment;
	public float timeBetweenLines;

	void Start () 
	{
		elapsed=0;
	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{

		elapsed+=increment;
	

		//if(line.transform.localScale.magnitude<=indx*squareSize && indx<=maxNumberOfLines)
		if( indx<=maxNumberOfLines && elapsed>=timeBetweenLines)
		{
			    GameObject g1=	GameObject.Instantiate(line, new Vector3(indx*squareSize,0,0), Quaternion.Euler(90,0,0)) as GameObject;
			    g1.transform.parent=transform;
				GameObject g2= GameObject.Instantiate(line, new Vector3(-indx*squareSize,0,0), Quaternion.Euler(90,0,0)) as GameObject;
				g2.transform.parent=transform;
				GameObject g3=GameObject.Instantiate(line, new Vector3(0,0,indx*squareSize), Quaternion.Euler(90,90,0))as GameObject;
				g3.transform.parent=transform;
				GameObject g4=GameObject.Instantiate(line, new Vector3(0,0,-indx*squareSize), Quaternion.Euler(90,90,0))as GameObject;
				g4.transform.parent=transform;

				elapsed=0;
				indx+=1;
		}


	}
}
