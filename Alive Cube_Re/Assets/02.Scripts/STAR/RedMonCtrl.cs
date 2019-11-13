using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

//보라색 몬스터 : 고정 위치에서 좌우로 레이저 발사 
public class RedMonCtrl : MonoBehaviour
{
    //레드 몬스터 시간차 공격
    public float delayAnimation;
    
    //레이저 
    private Transform targetTr;
    private GameObject redLaser;

    private Animator animator;
    private TestRayCast testRayCast;

    void Awake()
    {
        animator = GetComponent<Animator>();
        targetTr = GameObject.FindWithTag("Player").transform;
        testRayCast = GameObject.FindWithTag("Player").GetComponent<TestRayCast>();
        redLaser = transform.GetChild(0).transform.GetChild(0).gameObject;
    }

    private void Start()
    {
        Invoke("Action", delayAnimation);
    }

    private void Update()
    {
        //플레이어를 향해 레이저 발사
        redLaser.transform.DOLookAt(targetTr.position, 1.0f, AxisConstraint.None);

        Ray ray = testRayCast.Ray;
        RaycastHit hit = testRayCast.Hit;

        if (Physics.Raycast(ray, out hit, 10.0f))
        {
            //레드몬스터의 위치가 플레이어 기준, 왼쪽 벽과 더 가까울 때 오른쪽으로 회피한다
            if (hit.collider.CompareTag("REDMON")
             && hit.transform.position.x >= -8.9
             && hit.transform.position.x <= -4.5)
            {
                hit.transform.GetComponentInParent<Animator>().SetTrigger("Strafe Right");

                Invoke("StrafeRight", 2.0f);
            }
            //레드몬스터의 위치가 플레이어 기준, 오른쪽 벽과 더 가까울 때 왼쪽으로 회피한다
            else if (hit.collider.CompareTag("REDMON")
                  && hit.transform.position.x > -4.5
                  && hit.transform.position.x <= -0.1)
            {
                hit.transform.GetComponentInParent<Animator>().SetTrigger("Strafe Left");
                //hit.transform.GetComponentInParent<Animator>().SetTrigger("Idle");
                Invoke("StrafeLeft", 1.0f);
            }
        }
    }

    void Action()
    {
        Vector3 attackMode = new Vector3(-90, 0, 0);
        
        transform.DORotate(attackMode, 1.0f);
        redLaser.SetActive(true);
    }

    //void StrafeRight()
    //{
    //    RaycastHit hit = testRayCast.Hit;
    //    hit.transform.parent.Translate(4, 0, 0);
    //}
    void StrafeLeft()
    {
       transform.localPosition = transform.localPosition + Vector3.left*4.0f;
    }

    
}