using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class ThronSingeAttack : MonoBehaviour
{
    // Start is called before the first frame update
    //큐브 약점포인트 드러날 경우 판정
  
    public void SingleThronAttack()
    {
        transform.DOMoveY(Random.Range(0.3f, 0.5f), 0.8f).SetLoops(2, LoopType.Yoyo).SetEase(Ease.InFlash);
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
