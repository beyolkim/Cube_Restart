using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

//보라색 몬스터 : 고정 위치에서 좌우로 레이저 발사 
public class RedMonCtrl : MonoBehaviour
{
    private enum State
    {
        IDLE,
        STRAFE,
        ATTACK,
        FASTATTACK,
        TAKEDAMAGE,
        DIE
    }

    private State state;
    //사망 여부 판단
    public bool isDie = false;

    //레이저 
    private TestRayCast testRayCast;
    private GameObject redLaser;
    private Transform targetTr;

    //오디오 
    private AudioSource audioSource;
    public AudioClip[] audioClip;

    private Animator animator;
    //애니메이터 파라미터의 문자열을 해시값으로 추출 
    private readonly int h_Idle = Animator.StringToHash("Idle");
    private readonly int h_StrafeLeft = Animator.StringToHash("Strafe Left");
    private readonly int h_StrafeRight = Animator.StringToHash("Strafe Right");
    private readonly int h_Attack = Animator.StringToHash("Attack");
    private readonly int h_TakeDamage = Animator.StringToHash("Take Damage");
    private readonly int h_FastAttack = Animator.StringToHash("Fast Attack");
    private readonly int h_Die = Animator.StringToHash("Die");

    private WaitForSeconds ws;

    private GameObject rigHub;
    private GameObject deadParticle;
    bool flag = true;          //실행 제어 변수


    void Awake()
    {
        animator = GetComponent<Animator>();
        testRayCast = GameObject.FindWithTag("Player").GetComponent<TestRayCast>();
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
    }

    IEnumerator CheckState()
    {
        //레드 몬스터 사망 전까지 도는 무한루프
        while (!isDie)
        {
            if (state == State.DIE) yield break;

            if (animator.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
            {
                state = State.IDLE;
                StateIdle();
            }

            else if (animator.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
                state = State.ATTACK;
            else if (animator.GetCurrentAnimatorStateInfo(0).IsName("Strafe Left"))
                state = State.STRAFE;
            else if (animator.GetCurrentAnimatorStateInfo(0).IsName("Strafe Right"))
                state = State.STRAFE;
            else if (animator.GetCurrentAnimatorStateInfo(0).IsName("Take Damage"))
                state = State.TAKEDAMAGE;
            else if (animator.GetCurrentAnimatorStateInfo(0).IsName("Fast Attack"))
                state = State.FASTATTACK;
            else if (animator.GetCurrentAnimatorStateInfo(0).IsName("Die"))
                state = State.DIE;

            Debug.Log(state);
            yield return ws;
        }
    }

    void StateIdle()
    {
        redLaser.SetActive(false);
        animator.SetTrigger(h_Idle);
        //레드몬스터가 AttckMode로 방향 전환
        Vector3 attackMode = new Vector3(-90, 0, 0);
        transform.DORotate(attackMode, 1.5f);
    }
    void StateAttack()
    {
        Vector3 attackMode = new Vector3(-90, 0, 0);
        transform.DORotate(attackMode, 0.5f);

        //플레이어를 향해 Spin + 레이저 발사
        redLaser.transform.DOLookAt(targetTr.position, 1f, AxisConstraint.None);
        animator.SetTrigger(h_Attack);
        redLaser.SetActive(true);
        // +sound, +playerAttackDamage
    }

    public void StateStrafeLeft()
    {
        Debug.Log("@@@@@@@@@@@@@@@@@@@@@");
        Vector3 attackMode = new Vector3(-90, 0, 0);
        transform.DORotate(attackMode, 0.5f);
        redLaser.SetActive(false);

        animator.SetTrigger(h_StrafeLeft);
        StartCoroutine(MoveLeft());
        flag = false;
    }

    public void StateStrafeRight()
    {
        Debug.Log("!!!!!!!!!!!!!!!!!!!!!!!!!!");

        Vector3 attackMode = new Vector3(-90, 0, 0);
        transform.DORotate(attackMode, 0.5f);
        redLaser.SetActive(false);
        animator.SetTrigger(h_StrafeRight);
        StartCoroutine(MoveRight());
        flag = false;
    }

    void StateDie()
    {
        animator.SetTrigger(h_Die);
        // + 사운드
        rigHub.SetActive(false);
        deadParticle.SetActive(true);
    }

    void StateTakeDamage()
    {
        animator.SetTrigger(h_TakeDamage);
    }
    //Strafe 이동 코루틴
    IEnumerator MoveLeft()
    {

        for (int i = 0; i < 120; i++)
        {
            transform.Translate(transform.right * -0.03f);
            yield return null;
        }
    }
    IEnumerator MoveRight()
    {
        for (int i = 0; i < 120; i++)
        {
            transform.Translate(transform.right * 0.03f);
            yield return null;
        }
    }
}

