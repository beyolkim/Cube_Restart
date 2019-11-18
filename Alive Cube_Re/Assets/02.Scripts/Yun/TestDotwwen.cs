using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class TestDotwwen : MonoBehaviour
{
   
    void Start()
    {
        transform.localPosition = transform.localPosition + Vector3.up * Random.Range(-0.1f, 0.1f);
        transform.DOLocalMoveY(Random.Range(-0.5f, 0.5f), 3.0f).SetLoops(-1,LoopType.Yoyo).SetEase(Ease.Linear);

    }

    void LateUpdate()
    {
        
    }

}
