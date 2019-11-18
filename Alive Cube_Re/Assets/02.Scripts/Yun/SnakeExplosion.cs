using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeExplosion : MonoBehaviour
{

    private AttackCube dissolvedCube;
    private Transform parentTr;
    private GameObject parentObj;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.CompareTag("SWORD"))
        {
            AttackController.scoreCount++;
            Debug.Log(AttackController.scoreCount);
            StartCoroutine(dissolvedCube.CubeEexplosion());
        }

        if (collision.collider.CompareTag("CHECKINIT"))
        {
            StartCoroutine(dissolvedCube.initCube());
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("TARGET"))
        {
            parentObj.GetComponent<AttackCube>().targetCheck = true;
        }

    }
    void Start()
    {
        parentTr = this.transform.parent.transform;
        parentObj = parentTr.gameObject;
        dissolvedCube = parentObj.GetComponentInParent<AttackCube>();
    }

}
