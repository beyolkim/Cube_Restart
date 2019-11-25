using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class WallCubeController : MonoBehaviour
{
    public static WallCubeController instance = null;
    //큐브 약점포인트 드러날 경우 판정
    public bool setCheck;

    private AudioSource _audio;
    public AudioClip[] wallHit_audio;

    void Start()
    {
        instance = this;
        _audio = GetComponent<AudioSource>();

        if(this.CompareTag("THORNTARGET"))
        {
            transform.DOLocalMoveX(Random.Range(-0.5f, 0.5f), 3.0f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.Linear);
        }
        else
        {

            transform.localPosition = transform.localPosition + Vector3.up * Random.Range(-0.1f, 0.1f);
            transform.DOLocalMoveY(Random.Range(-0.5f, 0.5f), 3.0f).SetLoops(-1,LoopType.Yoyo).SetEase(Ease.Linear);
        }

    }

    //void LateUpdate()
    //{
        
    //}
    public void WallHit_Audio()
    {
        Debug.Log("피터지는소리" + wallHit_audio.Length);
        _audio.PlayOneShot(wallHit_audio[Random.Range(0, 1)]);
    }
}
