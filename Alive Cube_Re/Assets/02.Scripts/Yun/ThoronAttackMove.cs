using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class ThoronAttackMove : MonoBehaviour
{
    Vector3[] thronpoint = new Vector3[4];
    public Transform[] targetTr;
    private Transform firstTr;
    Tween attackSnake;

    bool checkcheck;
    void Start()
    {
        firstTr = this.transform;
        ThronMove();


    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("THORNTARGET") /*&& PlayerController.playerHp >0*/)
        {
            attackSnake.Pause();
            Vector3 FisrPr = new Vector3(Random.Range(-3.0f, 3.0f), -3, 10);
            StartCoroutine(AttackDelay());
            this.transform.position = FisrPr;
            //checkcheck = false;
        }
        if (other.CompareTag("THORN"))
        {
            other.GetComponent<ThronSingeAttack>().SingleThronAttack();
        }
    }


    IEnumerator AttackDelay()
    {
        yield return new WaitForSeconds(2.0f);
        ThronMove();
    }
    void ThronMove()
    {
        int rannum = Random.Range(0, 2);
        Vector3 pos = targetTr[rannum].position;
        thronpoint[0] = firstTr.position + Vector3.right * Random.Range(-2.5f, 2.5f) + Vector3.forward * Random.Range(0.0f, 2.0f) * -1;
        thronpoint[1] = firstTr.position + Vector3.right * Random.Range(-2.5f, 2.5f) + Vector3.forward * Random.Range(2.0f, 5.0f) * -1;
        thronpoint[2] = firstTr.position + Vector3.right * Random.Range(-2.5f, 2.5f) + Vector3.forward * Random.Range(5.0f, 7.0f) * -1;
        thronpoint[3] = pos;
        

        attackSnake = this.transform.DOPath(thronpoint, 3.0f, PathType.CatmullRom, PathMode.Full3D, 10, Color.red);
    }



}
