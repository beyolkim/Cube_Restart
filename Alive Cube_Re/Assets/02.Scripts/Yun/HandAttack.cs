using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class HandAttack : MonoBehaviour
{
    // Start is called before the first frame update
    
    
    void Start()
    {
        //handAttack = transform.DOLocalRotate(new Vector3(240, 0, 180), 3.0f).SetEase(Ease.InFlash).SetLoops(2,LoopType.Yoyo);
        
       
        StartCoroutine(HandAttackRepeat());
    }


    IEnumerator HandAttackRepeat()
    {
        while(PlayerController.playerHp >0)
        {
            transform.DOLocalRotate(new Vector3(240, 0, 180), 3.0f).SetEase(Ease.InFlash).SetDelay(2).SetLoops(2,LoopType.Yoyo);
            yield return new WaitForSeconds(10.0f);
        }

    }

  
}
