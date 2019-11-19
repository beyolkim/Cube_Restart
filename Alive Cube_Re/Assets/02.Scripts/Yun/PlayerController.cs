using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance = null;

    public float curTime = 0;
    private float attackTime = 10;
    public bool attackAllow = false;

    private bool playerHit = false;
    public GameObject hitEffect;

    private AudioSource _audio;
    public AudioClip intro_audio;
    public AudioClip mapMaking_audio;
    public AudioClip mapMakingEnd_audio;
    public AudioClip countDown_audio;
    public AudioClip hit_audio;
    public AudioClip gameOver_audio;

    public static int coint = 0;

    public bool playerDie = false;   

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

        Debug.Log(AttackController.playerHp);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("CUBE"))
        {
            
            Debug.Log("맞았다!!!");
            _audio.PlayOneShot(hit_audio);
            AttackController.playerHp--;
            if(!playerHit)
            {
                StartCoroutine(HitEffect());
            }            
            Debug.Log("PlayerHP : " + AttackController.playerHp);
            
        }
    }
    

    void Update()
    {
        AttackDifficulty();

        if(!playerDie && AttackController.playerHp <= 0)
        {            
            Debug.Log("Game Over");
            _audio.PlayOneShot(gameOver_audio);
            playerDie = true;
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
    IEnumerator HitEffect()
    {
        playerHit = true;
        this.gameObject.GetComponent<CapsuleCollider>().enabled = false;
        hitEffect.SetActive(true);
        
        yield return new WaitForSeconds(2.5f); //Player가 맞으면 2.5초간 무적
        hitEffect.SetActive(false);
        this.gameObject.GetComponent<CapsuleCollider>().enabled = true;
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
}
