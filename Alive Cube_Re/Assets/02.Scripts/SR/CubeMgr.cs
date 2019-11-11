using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeMgr : MonoBehaviour
{
    
    private GameObject testCube;    
        
    void Start()
    {
        testCube = Resources.Load<GameObject>("TestCube");        

        StartCoroutine(SpawnCube());
    }

    
    void Update()
    {
        //Vector3 pos = new Vector3(Random.Range(-1.5f, 2), Random.Range(0.5f, 4), 8);                

        //if (Input.GetMouseButtonDown(0))
        //{            
        //    Instantiate(testCube, pos, Quaternion.identity);

        //}        
    }
    IEnumerator SpawnCube()
    {
        while (true)
        {
            //Vector3 pos = new Vector3(Random.Range(-1f, 1.5f), Random.Range(2.5f, 4), 8);
            GameObject go = Instantiate(testCube);

            Vector3 pos = transform.position;
            pos.x += Random.Range(-1f, 1f);
            go.transform.position = pos;

            yield return new WaitForSeconds(1f);
        }

    }
}
