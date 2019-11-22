using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

//보라색 몬스터 : 고정 위치에서 좌우로 레이저 발사 
public class RedMonCtrl : MonoBehaviour
{
    public Vector3 attackAngle;

    //레이저 
    private GameObject redLaser;
    private Transform targetTr;

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

    public int R_MonHP;
    private bool isDie = false;
    private bool strafingFlag = true;

    //애니메이터 파라미터의 문자열을 해시값으로 추출 
    private readonly int h_Idle = Animator.StringToHash("Idle");
    private readonly int h_StrafeLeft = Animator.StringToHash("Strafe Left");
    private readonly int h_StrafeRight = Animator.StringToHash("Strafe Right");
    private readonly int h_Attack = Animator.StringToHash("Attack");
    private readonly int h_TakeDamage = Animator.StringToHash("Take Damage");
    private readonly int h_FastAttack = Animator.StringToHash("Fast Attack");
    private readonly int h_Die = Animator.StringToHash("Die");
    public Animator animator;


    private void Awake()
    {
        animator = GetComponent<Animator>();
        redLaser = transform.GetChild(0).transform.GetChild(0).gameObject;
        targetTr = GameObject.FindWithTag("Player").transform;
        ws = new WaitForSeconds(0.3f);
        audioSource = GetComponent<AudioSource>();
        deadParticle = transform.GetChild(1).gameObject;
        rigHub = transform.GetChild(0).gameObject;
    }

    private void OnEnable()
    {
        StartCoroutine(CheckState());
    }
    private void Start()
    {
        //RedMonCtrl 시작과 동시에 Spwan Sound 재생
        audioSource.PlayOneShot(audioClip[0]);

        Invoke("StateIdle", 5.3f);
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

            Debug.Log("레드몬 상태 : " + state);
            Debug.Log("레드몬 HP : " + R_MonHP);

            yield return ws;
        }
    }
    IEnumerator RedMonAction()
    {
        while (!isDie)

            yield return ws;
    }

    void StateIdle() //Idle : 레이 닿으면 피하고 피격시 공격모드
    {
        if (state == State.IDLE || state == State.ATTACK)
        {
            redLaser.SetActive(false);  //Idle재실행 때 레이저 끄기
            animator.ResetTrigger(h_Attack);

            animator.SetTrigger(h_Idle);
            animator.SetFloat("AnimSpeed", Random.Range(1.0f, 1.5f));
            animator.SetFloat("AnimOffset", Random.Range(0.0f, 1.0f));
            transform.DORotate(attackAngle, 0.0f);
        }
    }
    public void StateStrafeLeft() //Strafe - 레이닿으면 왼쪽으로 피하고 즉시 공격모드
    {

        if (state == State.STRAFE || state == State.IDLE)
        {
            if (strafingFlag == true)
            {
                //GunCtrl.instance.strafing = true;
                Debug.Log("StateStrafeLeft가 실행되었습니다");
                transform.DORotate(attackAngle, 0.0f);
                redLaser.SetActive(false);
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
                Debug.Log("StateStrafeRight가 실행되었습니다");
                transform.DORotate(attackAngle, 0.0f);
                redLaser.SetActive(false);
                animator.SetTrigger(h_StrafeRight);
                StartCoroutine(MoveRight());
            }
        }
    }

    void StateAttack() //Attack - 회전 + 레이저, 레이 못 피함, 이후 Idle로 돌아감
    {

        if (state == State.ATTACK || state == State.STRAFE)
        {
        Debug.Log("StateAttack이 실행되었습니다");
            transform.DORotate(attackAngle, 0.0f);
            animator.ResetTrigger(h_StrafeRight);
            animator.ResetTrigger(h_StrafeRight);
            animator.SetTrigger(h_Attack);
            redLaser.transform.DOLookAt(targetTr.position, 0.5f, AxisConstraint.None);
            redLaser.SetActive(true);
        }
    }

    void StateFastAttack() //FastAttack - Take Damage 이후 실행, 죽을때까지 멈추지 않음
    {
        Debug.Log("StateAttack이 실행되었습니다");

        transform.DORotate(attackAngle, 0.0f);
        redLaser.transform.DOLookAt(targetTr.position, 0.5f, AxisConstraint.None);
        redLaser.SetActive(true);
    }

    public void StateTakeDamage()
    {
        Debug.Log("StateTakeDamage가 실행되었습니다");

        transform.DORotate(attackAngle, 0.0f);
        animator.SetTrigger("Take Damage");
        R_MonHP--;
    }

    void StateDie() //Die - 죽는 애니메이션, 몸체 끄기, 죽는 파티클
    {
        Debug.Log("StateAttack이 실행되었습니다");

        rigHub.SetActive(false);
        deadParticle.SetActive(true);
    }

    //Strafe 이동 코루틴
    IEnumerator MoveLeft()
    {
        float yy = Random.Range(-0.02f, 0.02f);
        float zz = Random.Range(-0.02f, 0.02f);

        strafingFlag = false;
        for (int i = 0; i < 120; i++)
        {
            transform.Translate(transform.right * -0.06f);
            transform.Translate(transform.up * yy);
            transform.Translate(transform.forward * zz);

            yield return null;
        }
        strafingFlag = true;
    }
    IEnumerator MoveRight()
    {
        float yy = Random.Range(-0.02f, 0.02f);
        float zz = Random.Range(-0.02f, 0.02f);
        strafingFlag = false;

        for (int i = 0; i < 120; i++)
        {
            transform.Translate(transform.right * 0.06f);
            transform.Translate(transform.up * yy);
            transform.Translate(transform.forward * zz);
            yield return null;
        }
        strafingFlag = true;

    }
}

