using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

//보라색 몬스터 : 고정 위치에서 좌우로 레이저 발사 
public class RedMonCtrl : MonoBehaviour
{
    public static RedMonCtrl instance = null;

    public Vector3 attackAngle;
     
    private Transform targetTr;

    //파티클 미사일
    private GameObject homming;

    private ParticleSystem hommingparticle;

    //오디오 
    private AudioSource audioSource;
    public AudioClip[] audioClip;

    private WaitForSeconds ws;
    private GameObject rigHub;
    private GameObject deadParticle;
    public enum State
    {
        IDLE,
        STRAFE,
        ATTACK,
        FASTATTACK,
        TAKEDAMAGE,
        DIE
    }
    public State state;

    public int R_MonHP = 4;
    private bool isDie = false;
    private bool strafingFlag = true;

    //애니메이터 파라미터의 문자열을 해시값으로 추출 
    public Animator animator;
    private readonly int h_Idle = Animator.StringToHash("Idle");
    private readonly int h_StrafeLeft = Animator.StringToHash("Strafe Left");
    private readonly int h_StrafeRight = Animator.StringToHash("Strafe Right");
    private readonly int h_Attack = Animator.StringToHash("Attack");
    private readonly int h_TakeDamage = Animator.StringToHash("Take Damage");
    private readonly int h_FastAttack = Animator.StringToHash("Fast Attack");
    private readonly int h_Die = Animator.StringToHash("Die");
    private int gameOver_Idle = Animator.StringToHash("GameOver_Idle");
    public static bool monReady = false;


    private void Awake()
    {
        instance = this;
        animator = GetComponent<Animator>();
        homming = transform.GetChild(0).transform.GetChild(0).gameObject;
        targetTr = GameObject.FindWithTag("Player").transform;
        ws = new WaitForSeconds(0.3f);
        audioSource = GetComponent<AudioSource>();
        deadParticle = transform.GetChild(1).gameObject;
        rigHub = transform.GetChild(0).gameObject;

    }

    private void OnEnable()
    {
        StartCoroutine(CheckState());
        Invoke("StateSpawn", 0.25f);
    }
    private void Start()
    {
        //RedMonCtrl 시작과 동시에 Spwan Sound 재생
        audioSource.PlayOneShot(audioClip[0]);
        hommingparticle = homming.GetComponent<ParticleSystem>();

    }

    IEnumerator CheckState()
    {
        //레드 몬스터 사망 전까지 도는 무한루프
        while (!isDie)
        {
            if (state == State.DIE) yield break;

            if (animator.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
                state = State.IDLE;
            else if (animator.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
                state = State.ATTACK;
            else if (animator.GetCurrentAnimatorStateInfo(0).IsName("Strafe Left"))
                state = State.STRAFE;
            else if (animator.GetCurrentAnimatorStateInfo(0).IsName("Strafe Right"))
                state = State.STRAFE;
            else if (animator.GetCurrentAnimatorStateInfo(0).IsName("Take Damage"))
                state = State.TAKEDAMAGE;
            else if (animator.GetCurrentAnimatorStateInfo(0).IsName("Fast Attack"))
            {
                state = State.FASTATTACK;
                StateFastAttack();
            }
            else if (animator.GetCurrentAnimatorStateInfo(0).IsName("Die"))
            {
                state = State.DIE;
                StateDie();
                isDie = true;
            }

            //Debug.Log("레드몬 상태 : " + state);
            //Debug.Log("레드몬 HP : " + R_MonHP);

            yield return ws;
        }
    }

    void StateSpawn()
    {
        StartCoroutine(RotAnim());
        animator.SetTrigger(h_Idle);
        animator.SetFloat("AnimSpeed", Random.Range(1.0f, 1.5f));
        animator.SetFloat("AnimOffset", Random.Range(0.0f, 1.0f));
        monReady = true;
    }

    IEnumerator RotAnim()
    {
        transform.DORotate(attackAngle, 2.0f);
        yield return new WaitForSeconds(2.0f);
    }

    void StateIdle() //Idle : 레이 닿으면 피하고 피격시 공격모드
    {
        hommingparticle.emissionRate = 0.0f;


        //hommingparticle.Stop();


        //homming.SetActive(false);  //Idle재실행 때 레이저 끄기
        animator.ResetTrigger(h_Attack);
        animator.SetTrigger(h_Idle);
        animator.SetFloat("AnimSpeed", Random.Range(1.0f, 1.5f));
        animator.SetFloat("AnimOffset", Random.Range(0.0f, 1.0f));
        transform.DORotate(attackAngle, 0.0f);
    }

    public void StateStrafeLeft() //Strafe - 레이닿으면 왼쪽으로 피하고 즉시 공격모드
    {

        if (state == State.STRAFE || state == State.IDLE)
        {
            if (strafingFlag == true)
            {
                //GunCtrl.instance.strafing = true;
                //Debug.Log("StateStrafeLeft가 실행되었습니다");
                transform.DORotate(attackAngle, 0.0f);
                hommingparticle.emissionRate = 0.0f;

               //hommingparticle.Stop();

                //homming.SetActive(false);
                animator.SetTrigger(h_StrafeLeft);
                StartCoroutine(MoveLeft());
            }
        }
    }
    public void StateStrafeRight() //Strafe - 레이닿으면 오른쪽으로 피하고 즉시 공격모드
    {

        if (state == State.STRAFE || state == State.IDLE)
        {
            if (strafingFlag == true)
            {
                //GunCtrl.instance.strafing = true;
                //Debug.Log("StateStrafeRight가 실행되었습니다");
                transform.DORotate(attackAngle, 0.0f);

                hommingparticle.emissionRate = 0.0f;

                //hommingparticle.Stop();

                //homming.SetActive(false);
                animator.SetTrigger(h_StrafeRight);
                StartCoroutine(MoveRight());
            }
        }
    }

    void StateAttack() //Attack - 회전 + 레이저, 레이 못 피함, 이후 Idle로 돌아감
    {

        if (state == State.ATTACK || state == State.STRAFE)
        {
            //Debug.Log("StateAttack이 실행되었습니다");
            transform.DORotate(attackAngle, 0.0f);
            animator.ResetTrigger(h_StrafeRight);
            animator.ResetTrigger(h_StrafeRight);
            animator.SetTrigger(h_Attack);

            hommingparticle.emissionRate = 2.0f;

        }
    }

    void StateFastAttack() //FastAttack - Take Damage 이후 실행, 죽을때까지 멈추지 않음
    {
        //Debug.Log("StateAttack이 실행되었습니다");

        transform.DORotate(attackAngle, 0.0f);



        hommingparticle.emissionRate = 3.0f;

    }

    public void StateTakeDamage()
    {
        //Debug.Log("StateTakeDamage가 실행되었습니다");

        transform.DORotate(attackAngle, 0.0f);
        animator.SetTrigger("Take Damage");
        R_MonHP--;
    }

    void StateDie() //Die - 죽는 애니메이션, 몸체 끄기, 죽는 파티클
    {
        //Debug.Log("StateAttack이 실행되었습니다");

        if (this.gameObject != null)
        {
            rigHub.SetActive(false);
            deadParticle.SetActive(true);
            StartCoroutine(ParticleDelay());
        }
    }

    IEnumerator ParticleDelay()
    {
        yield return new WaitForSeconds(1.5f);
        Destroy(this.gameObject);
        PlayerController.redMon_Kill++;
        Debug.Log("레드몬 죽은 마리수 : " + PlayerController.redMon_Kill);

    }
    //Strafe 이동 코루틴
    IEnumerator MoveLeft()
    {
        float yy = Random.Range(-0.01f, 0.01f);
        float zz = Random.Range(-0.001f, 0.01f);

        strafingFlag = false;
        for (int i = 0; i < 100; i++)
        {
            transform.Translate(transform.right * -0.04f);

            yield return null;
            if (isDie)
            {

                break;
            }
        }

        strafingFlag = true;
    }
    IEnumerator MoveRight()
    {
        float yy = Random.Range(-0.01f, 0.01f);
        float zz = Random.Range(-0.001f, 0.01f);
        strafingFlag = false;

        for (int i = 0; i < 100; i++)
        {
            transform.Translate(transform.right * 0.04f);

            yield return null;
            if (isDie)
            {
                break;
            }
        }

        strafingFlag = true;
    }


}

