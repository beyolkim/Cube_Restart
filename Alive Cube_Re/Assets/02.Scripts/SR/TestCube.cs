using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCube : MonoBehaviour
{
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = new Vector3(0, 0, -12f);
    }
    private void Update()
    {
        if(transform.position.y <-5)
        {
            Destroy(this.gameObject);
        }
    }

}
