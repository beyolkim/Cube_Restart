using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    private Transform camTr;                //카메라를 자식으로 갖고있는 플레이어의 위치
    private Vector3 originalPos;            //플레이어에 있는 cam의 위치

    public float shake;
    public float shakeAmount = 0.05f;
    public float decreaseFactor = 1.0f;

    void Start()
    {
        camTr = transform.parent.transform;
        originalPos = camTr.localPosition;
    }
   
    void Update()
    {
        //카메라 흔들림
        if (shake > 0)
        {
            camTr.localPosition = originalPos + Random.insideUnitSphere * shakeAmount;
            shake -= Time.deltaTime * decreaseFactor;
        }
        else
        {
            shake = 0f;
            camTr.localPosition = originalPos;
        }
    }
}
