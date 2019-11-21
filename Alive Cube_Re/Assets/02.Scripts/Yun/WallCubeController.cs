using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class WallCubeController : MonoBehaviour
{
    //큐브 약점포인트 드러날 경우 판정
    public bool setCheck;
    void Start()
    {
        transform.localPosition = transform.localPosition + Vector3.up * Random.Range(-0.1f, 0.1f);
        transform.DOLocalMoveY(Random.Range(-0.5f, 0.5f), 3.0f).SetLoops(-1,LoopType.Yoyo).SetEase(Ease.Linear);

    }

    void LateUpdate()
    {
        
    }

}
