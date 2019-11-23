using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallCheck : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    //void Update()
    //{
        
    //}
    private void OnCollisionEnter(Collision coll)
    {
        if(coll.gameObject.CompareTag("WALL"))
        {
            RedMonCtrl.wallHit = true;
            Debug.Log("벽이다~~~~~~~~~");
        }
    }
}
