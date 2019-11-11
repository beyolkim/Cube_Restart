using UnityEngine;
using System.Collections;

public enum Type{
    hexagons,squares
 	}

public class TypeSelector : MonoBehaviour {

	public Type current;
	public int numberOfSeeds;
	public GameObject hexagonPrefab, squarePrefab;
	public Vector3 startingPoint;

	void Start () 
	{

		int nbChildren=transform.childCount;

		if(current==0)
		{
		 	for(int jj=1;jj<=numberOfSeeds;jj++)
		 	{
		 		GameObject g=Instantiate(hexagonPrefab,startingPoint,Quaternion.Euler(0,0,0)) as GameObject;
		 		g.transform.parent=transform;
		 	}	
		}
		else
		{
			for(int jj=1;jj<=numberOfSeeds;jj++)
		 	{
				GameObject g=Instantiate(squarePrefab,startingPoint,Quaternion.Euler(0,0,0)) as GameObject;
				g.transform.parent=transform;
		 	}	
		}


		
	}

}
