﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance = null;

    public float curTime = 0;
    private float attackTime = 5;
    public static int playerHp = 30;

    public Slider hpSlider;
    public static int redMon_Kill = 0;
    public bool attackAllow = false;

    private bool playerHit = false;
    public GameObject hitEffect;

    public AudioSource _audio;
    public AudioSource stage2_audio;
    public AudioClip intro_audio;
    public AudioClip mapMaking_audio;
    public AudioClip mapMakingEnd_audio;
    public AudioClip countDown_audio;
    public AudioClip hit_audio;
    public AudioClip gameOver_audio;
    public AudioClip earthquake_audio;
    public AudioClip[] ending_audio;
    public AudioClip[] stageClear_audio;

    public GameObject warningZone;
    public GameObject attackCube;

    public static int coint = 0;
    public bool playerDie = false;
    private bool stageClear_Check = false;

    //죽었을 경우 조각들 파편 파괴(스테이지 클리어에만 파괴되지 않음)
    public GameObject redObjs;
    public GameObject purbpleObjs;
    public GameObject hpUI;
    public GameObject gameOverUI;
    public GameObject stageClearUI;
    public GameObject stage1UI;


    // 3stage 죽었을 경우 사라질 공격
    public GameObject stage3Attack;
    private void OnEnable()
    {
        IntroAnimation.IntroAudio += MapMaking_Audio;
    }
    private void OnDisable()
    {
        IntroAnimation.IntroAudio -= MapMaking_Audio;
    }

    void Start()
    {
        instance = this;

        _audio = GetComponent<AudioSource>();
        _audio.clip = intro_audio;
        _audio.Play();

        Debug.Log(playerHp);

        if (this.gameObject.scene.name == "Stage1")
        {
            StartCoroutine(MapSpawn());
        }
        else if(this.gameObject.scene.name == "Stage2")
        {
            hpSlider.value = playerHp;
        }
        else if(this.gameObject.scene.name == "Stage3")
        {
            hpSlider.value = playerHp;
        }
        else if(this.gameObject.scene.name == "End&Reset")
        {
            StartCoroutine(EndingAudio());

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("CUBE") || other.CompareTag("LASER") 
            || other.CompareTag("THORN") || other.CompareTag("HANDATTACK"))
        {
            Debug.Log("맞았다!!!");
            _audio.PlayOneShot(hit_audio);
            playerHp--;
            hpSlider.value = playerHp;
            if (!playerHit)
            {
                StartCoroutine(HitEffect(this.gameObject));
            }
            Debug.Log("PlayerHP : " + playerHp);

        }
    }

    void Update()
    {
        AttackDifficulty();

        if (this.gameObject.scene.name == "Stage2")
        {
            StartCoroutine(Stage2PlayerDie());
            StartCoroutine(Stage2Clear());
        }
        else if (this.gameObject.scene.name == "Stage3")
        {
            StartCoroutine(Stage3PlayerDie());
        }

    }

    public void MapMaking_Audio() //Intro 씬에서 Intro큐브를 trigger하면 audiosource가 있는 그 큐브의 위치가 바뀌어서 어쩔 수 없음
    {
        _audio.Stop();
        _audio.PlayOneShot(mapMaking_audio);

    }
    public void _IntroAudio()
    {
        _audio.PlayOneShot(mapMakingEnd_audio);
    }

    public void CountDown_Audio()
    {
        _audio.PlayOneShot(countDown_audio);

    }
    public void Earthquake_Audio()
    {
        _audio.PlayOneShot(earthquake_audio);
    }


    public IEnumerator HitEffect(GameObject _target)
    {
        playerHit = true;
        _target.GetComponent<CapsuleCollider>().enabled = false;
        hitEffect.SetActive(true);

        yield return new WaitForSeconds(2.5f); //Player가 맞으면 2.5초간 무적
        hitEffect.SetActive(false);
        _target.GetComponent<CapsuleCollider>().enabled = true;
        playerHit = false;
    }

    void AttackDifficulty()
    {
        curTime += Time.deltaTime;
        //Debug.Log(curTime);
        if (curTime < attackTime)
        {
            attackAllow = false;
        }

        else
        {
            attackAllow = true;
        }
    }

    public void PlayerDie() //Stage1 사망시에만 발동
    {
        Debug.Log("Game Over");
        Destroy(purbpleObjs);
        Destroy(redObjs);
        _audio.PlayOneShot(gameOver_audio);
        //AttackController.playerHp = 6;
    }

    IEnumerator MapSpawn()
    {
        yield return new WaitForSeconds(4f);
        warningZone.gameObject.SetActive(true);
        attackCube.gameObject.SetActive(true);
        stage1UI.gameObject.SetActive(true);
    }

    IEnumerator Stage2PlayerDie()
    {
        if (!playerDie && playerHp <= 0)
        {
            playerDie = true;
            HandCtrl.instance.gunOn = false; //Gun의 총알 끄기
            GunCtrl.instance.gunReady = false; //Gun의 Ray 끄기
            PurpleMonCtrl.instance.StateDie(); //퍼플 몬스터의 공격 중지
            //RedMonCtrl.instance.animator.SetTrigger("Die");
            PieceCtrl.instance.GameOver_RedIdle();
            Debug.Log("Game Over");
            _audio.PlayOneShot(gameOver_audio);
            hpUI.gameObject.SetActive(false); //hp창 끄기
            gameOverUI.gameObject.SetActive(true);
            yield return new WaitForSeconds(1.5f);
            StartCoroutine(HandCtrl.instance.GunDisapper()); //Gun 사라지게
            StartCoroutine(LeftHandCtrl.instance.ShieldDisapper()); //Shield 사라지게
            yield return new WaitForSeconds(2.5f);
            Earthquake_Audio(); //벽 수축 Audio
            yield return new WaitForSeconds(2.5f);
            GameOver_Shrinking.instance.GameOver(); //벽 수축 애니메이션
            yield return new WaitForSeconds(8.5f);
            SceneManager.LoadScene(4);
        }
    }

    IEnumerator Stage3PlayerDie()
    {
        if (!playerDie && playerHp <= 0)
        {
            playerDie = true;
            HandCtrl.instance.gunOn = false; //Gun의 총알 끄기
            GunCtrl.instance.gunReady = false; //Gun의 Ray 끄기
            Debug.Log("Game Over");
            _audio.PlayOneShot(gameOver_audio);
            hpUI.gameObject.SetActive(false); //hp창 끄기
            gameOverUI.gameObject.SetActive(true);
            yield return new WaitForSeconds(1.5f);
            stage3Attack.SetActive(false);
            StartCoroutine(HandCtrl.instance.GunDisapper()); //Gun 사라지게
            StartCoroutine(LeftHandCtrl.instance.ShieldDisapper()); //Shield 사라지게
            yield return new WaitForSeconds(2.5f);
            Earthquake_Audio(); //벽 수축 Audio
            yield return new WaitForSeconds(2.5f);
            GameOver_Shrinking.instance.GameOver(); //벽 수축 애니메이션
            yield return new WaitForSeconds(8.5f);
            SceneManager.LoadScene(4);
        }
    }



    IEnumerator Stage2Clear()
    {
        if (!stageClear_Check && redMon_Kill >= 7)
        {
            stageClear_Check = true;
            Debug.Log("Stage2 Clear!");
            stage2_audio.Stop();
            _audio.PlayOneShot(stageClear_audio[0]);
            PurpleMonCtrl.instance.StateDie(); //퍼플 몬스터의 공격을 중지하도록
            stageClearUI.gameObject.SetActive(true); //Stage1 Clear UI 표시
            hpUI.gameObject.SetActive(false); //hp창 끄기
            yield return new WaitForSeconds(3f);
            FadeCtrl.instance.FadeOut();
            yield return new WaitForSeconds(4);
            SceneManager.LoadScene(3); //Stage2 클리어 -> Stage3 씬 전환

        }
    }
    public IEnumerator Stage3Clear()
    {
        _audio.PlayOneShot(stageClear_audio[1]);
        stageClearUI.gameObject.SetActive(true); //Stage Clear UI 표시
        hpUI.gameObject.SetActive(false);
        yield return new WaitForSeconds(3f);
        FadeCtrl.instance.FadeOut();
        yield return new WaitForSeconds(4);
        SceneManager.LoadScene(0); //Stage3 클리어 -> Intro 씬 전환
    }

    IEnumerator EndingAudio()
    {
        while (true)
        {
            _audio.PlayOneShot(ending_audio[Random.Range(0, ending_audio.Length)]);
            yield return new WaitForSeconds(3);
        }
    }
}
