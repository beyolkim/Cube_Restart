using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestRayCast : MonoBehaviour
{
    private Ray ray;
    private RaycastHit hit;

    // Update is called once per frame
    void Update()
    {
        ray = new Ray(transform.position, transform.forward);
        Debug.DrawRay(ray.origin, ray.direction * 10.0f, Color.green);

        if (Physics.Raycast(ray, out hit, 15.0f))
        {
            if(hit.collider.CompareTag("ENEMY"))
            {
                hit.transform.GetComponent<Animator>().SetTrigger("Strafe Right");
                Debug.Log("빨갱이 잡았냐@@@@@@@@@");
            }
        }
    }
}
