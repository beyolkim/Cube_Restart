using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class ThronSingeAttack : MonoBehaviour
{
    // Start is called before the first frame update
    //큐브 약점포인트 드러날 경우 판정
    private Transform firstPos;

    private void Start()
    {
        firstPos = this.gameObject.transform;
    }
    public void SingleThronAttack()
    {
        transform.DOLocalMoveY(Random.Range(0.7f, 0.8f), 0.8f).SetLoops(2, LoopType.Yoyo).SetEase(Ease.InFlash);
        firstPos.DOMove(firstPos.position, 0.7f);
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
