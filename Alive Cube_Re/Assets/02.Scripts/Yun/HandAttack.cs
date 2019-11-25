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
    //파티클 색깔
    float gravity = 1;
    float deathColor = 0;
    int particleRate = 1000;
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
        }else if(AttackController.cubeHp <= 10 && AttackController.cubeHp >0)
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
            //하얀색 파티클
            if (AttackController.cubeHp > 15 )
            {
                transform.DOLocalRotate(new Vector3(240, 0, 180), 3.0f).SetEase(Ease.InFlash).SetLoops(2, LoopType.Yoyo);
                yield return new WaitForSeconds(1.5f);
                Boss_Audio.instance.BossAudio();
                yield return new WaitForSeconds(8.5f);
            }

            //파란색 파티클
            if (AttackController.cubeHp <= 15 && AttackController.cubeHp > 10)
            {
                transform.DOLocalRotate(new Vector3(240, 0, 180), 2.0f).SetEase(Ease.InFlash).SetLoops(4, LoopType.Yoyo);
                yield return new WaitForSeconds(1.5f);
                Boss_Audio.instance.BossAudio();
                yield return new WaitForSeconds(6.5f);

            }
            //빨간색 파티클
            if (AttackController.cubeHp <= 10)
            {
                transform.DOLocalRotate(new Vector3(240, 0, 180), 1.5f).SetEase(Ease.InFlash).SetLoops(4, LoopType.Yoyo);
                yield return new WaitForSeconds(1.5f);
                Boss_Audio.instance.BossAudio();
                yield return new WaitForSeconds(6.5f);

            }


        }

        if (PlayerController.playerHp <= 0) // 플레이어 hp가 0 이면 핸드 죽는 애니메이션 없이 넘어가기
        {
            yield break;
        }
        transform.DOLocalRotate(new Vector3(300, 0, 180), 2.5f).SetEase(Ease.InFlash);


        StartCoroutine(DeathHand());


    }

    IEnumerator DeathHand()
    {
        
        //Debug.Log("죽음카운트시작");
        yield return new WaitForSeconds(2.5f);
        handEffect.SetInt("SubParticle", 5);
        Boss_Audio.instance.BossDeath();
        while (deathColor <3)
        {
            deathColor += 0.01f;
            //thirdColor = -0.01f;
            handEffect.SetFloat("ThirdColor", 3.0f-deathColor);
            handEffect.SetFloat("DeathColor", deathColor);
            handEffect.SetFloat("GravityX", gravity);
            yield return new WaitForSeconds(0.01f);
            //Debug.Log("끝" + deathColor);
        }
        while (particleRate  > 0)
        {
            particleRate = -1;
            gravity -= 0.01f;

            handEffect.SetFloat("ParticleRate", particleRate);
            yield return new WaitForSeconds(0.01f);

        }

       // Debug.Log("끝");

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

