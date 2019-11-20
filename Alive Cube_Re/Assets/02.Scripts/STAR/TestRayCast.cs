using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TestRayCast : MonoBehaviour
{
    private Ray ray;
    private RaycastHit hit;
    private bool flag;
    RedMonCtrl redMonCtrl;

    private void Start()
    {
        redMonCtrl = GameObject.Find("Stg2Mon(Red)").GetComponent<RedMonCtrl>();
        Debug.Log(redMonCtrl);
        ray = new Ray(transform.position, transform.forward);
        flag = true;
    }
    void Update()
    {
        Debug.DrawRay(ray.origin, ray.direction * 10.0f, Color.green);

        if (Physics.Raycast(ray, out hit, 10.0f) && hit.collider.CompareTag("REDMON"))
        {
            Debug.Log(hit.collider.name);
            //Idle상태일 때만 회피 가능, 공격 또는 회피 도중 피격은 피할 수 없음
            if (transform.position.x > -4.5 && transform.position.x <= -0.1 && flag == true)
            {
                redMonCtrl.StateStrafeLeft();
            }

            else if (transform.position.x >= -8.9 && transform.position.x <= -4.5 && flag == true)
            {
                redMonCtrl.StateStrafeRight();
            }
        }
    }
}
