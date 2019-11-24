using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class HandAttack : MonoBehaviour
{
    // Start is called before the first frame update
    public static bool stage3 = true;
    
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
            yield return new WaitForSeconds(2.5f);
            Boss_Audio.instance.BossAudio();
            yield return new WaitForSeconds(7.5f);
        }

    }
    public void ShieldCollsion()
    {
        transform.DOLocalRotate(new Vector3(50, 0, 180), 3.0f).SetEase(Ease.InFlash);

    }

}
