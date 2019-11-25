using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Experimental.VFX;

public class HandAttack : MonoBehaviour
{
    // Start is called before the first frame update
    public static bool stage3 = true;
    public VisualEffect handEffect;
    void Start()
    {
        //handAttack = transform.DOLocalRotate(new Vector3(240, 0, 180), 3.0f).SetEase(Ease.InFlash).SetLoops(2,LoopType.Yoyo);

        StartCoroutine(HandAttackRepeat());
    }

    private void Update()
    {
        if(AttackController.cubeHp <= 15 && AttackController.cubeHp > 10)
        {
            handEffect.SetFloat("FirstColor", 0f);
            handEffect.SetFloat("SecondColor", 3f);
            handEffect.SetFloat("ThirdColor", 0f);
        }else if(AttackController.cubeHp < 10)
        {
            handEffect.SetFloat("FirstColor", 0f);
            handEffect.SetFloat("SecondColor", 0f);
            handEffect.SetFloat("ThirdColor", 3f);
        }
        
    }

    IEnumerator HandAttackRepeat()
    {
        while (PlayerController.playerHp > 0 && AttackController.cubeHp >0)
        {

            //transform.DOLocalRotate(new Vector3(240, 0, 180), 3.0f).SetEase(Ease.InFlash).SetLoops(2, LoopType.Yoyo);
            //yield return new WaitForSeconds(2.5f);
            //Boss_Audio.instance.BossAudio();
            //yield return new WaitForSeconds(7.5f);


            if (AttackController.cubeHp <= 20 && AttackController.cubeHp > 15 )
            {
                transform.DOLocalRotate(new Vector3(240, 0, 180), 3.0f).SetEase(Ease.InFlash).SetLoops(2, LoopType.Yoyo);
                yield return new WaitForSeconds(1.5f);
                Boss_Audio.instance.BossAudio();
                yield return new WaitForSeconds(8.5f);
            }


            if (AttackController.cubeHp <= 15 && AttackController.cubeHp > 10)
            {
                transform.DOLocalRotate(new Vector3(240, 0, 180), 2.0f).SetEase(Ease.InFlash).SetLoops(4, LoopType.Yoyo);
                yield return new WaitForSeconds(1.5f);
                Boss_Audio.instance.BossAudio();
                yield return new WaitForSeconds(6.5f);

            }

            if (AttackController.cubeHp <= 10)
            {
                transform.DOLocalRotate(new Vector3(240, 0, 180), 1.5f).SetEase(Ease.InFlash).SetLoops(4, LoopType.Yoyo);
                yield return new WaitForSeconds(1.5f);
                Boss_Audio.instance.BossAudio();
                yield return new WaitForSeconds(6.5f);

            }

        }

    }
    public void ShieldCollsion()
    {
        transform.DOLocalRotate(new Vector3(50, 0, 180), 2.0f).SetEase(Ease.InFlash);

    }


    public IEnumerator HandVFX(GameObject _coll)
    {
        float force = 100f;
        handEffect.SetFloat("Force", force);
        _coll.GetComponent<MeshCollider>().enabled = false;
        yield return new WaitForSeconds(2.0f);
        handEffect.SetFloat("Force", 0);
        _coll.GetComponent<MeshCollider>().enabled = true;

    }

}

