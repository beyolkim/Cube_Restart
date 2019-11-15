using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

//보라색 몬스터 : 고정 위치에서 좌우로 레이저 발사 
public class RedMonCtrl : MonoBehaviour
{
    public enum State
    {
        IDLE,
        STRAFE,
        ATTACK,
        DIE
    }

    //상태를 저장할 변수
    public State state = State.IDLE;
    //사망 여부를 판단할 변수
    public bool isDie = false;
    private WaitForSeconds ws;

    //레드 몬스터 시간차 공격
    public float delayAnimation;

    //레이저 
    private Transform targetTr;
    private GameObject redLaser;

    private Animator animator;
    private TestRayCast testRayCast;

    //오디오 
    private AudioSource audioSource;
    public AudioClip[] audioClip;

    //애니메이터 파라미터의 문자열을 해시값으로 추출 
    private readonly int h_Idle = Animator.StringToHash("Idle");
    private readonly int h_StrafeLeft = Animator.StringToHash("Strafe Left");
    private readonly int h_StrafeRight = Animator.StringToHash("Strafe Right");
    private readonly int h_Attack = Animator.StringToHash("Attack");
    private readonly int h_TakeDamage = Animator.StringToHash("Take Damage");
    private readonly int h_FastAttack = Animator.StringToHash("Fast Attack");
    private readonly int h_Die = Animator.StringToHash("Die");

    //실행 제어 변수
    bool flag = true;

    void Awake()
    {
        animator = GetComponent<Animator>();
        targetTr = GameObject.FindWithTag("Player").transform;
        testRayCast = GameObject.FindWithTag("Player").GetComponent<TestRayCast>();
        redLaser = transform.GetChild(0).transform.GetChild(0).gameObject;
        ws = new WaitForSeconds(0.3f);
        audioSource = GetComponent<AudioSource>();
        
    }

    private void OnEnable()
    {
        
        StartCoroutine(CheckState());
        StartCoroutine(Action());

    }
    private void Start()
    {
        
        audioSource.PlayOneShot(audioClip[0]);
    }

    IEnumerator CheckState()
    {
        while (!isDie)
        {
            //if (state == State.DIE)
            //    yield break;

            //레드몬스터가 레이에 닿았을 때 상태
            
                //state = State.STRAFE;
                //Debug.Log("@@@@@@@@@@@@@@맞았니");

            
            //else if (state != State.STRAFE && state != State.ATTACK)
            //{
            //    state = State.IDLE;
            //}
            //else
            //{
            //    state = State.ATTACK;
            //}

            yield return ws;
        }
    }

    IEnumerator Action()
    {
        while (!isDie)
        {
            yield return ws;

            switch (state)
            {
                case State.IDLE:
                    StateIdle();
                    break;
                case State.STRAFE:
                    StateStrafe();
                    break;
                case State.ATTACK:
                    StateAttack();
                    break;
                case State.DIE:
                    StateDie();
                    break;
            }
        }
        Debug.Log(state);
    }

    void StateIdle()
    {
        ////Attack모드 해제, Idle 시작
        //animator.SetBool(h_Attack, false);
        //redLaser.SetActive(false);
        animator.SetTrigger(h_Idle);
        //레드몬스터가 AttckMode로 방향 전환
        Vector3 attackMode = new Vector3(-90, 0, 0);
        transform.DORotate(attackMode, 1.5f);
    }
    void StateAttack()
    {
        //플레이어를 향해 Spin + 레이저 발사
        animator.SetBool(h_Attack, true);
        redLaser.SetActive(true);
        redLaser.transform.DOLookAt(targetTr.position, 1.0f, AxisConstraint.None);
    }

    void StateStrafe()
    {
        redLaser.SetActive(false);

        Ray ray = testRayCast.Ray;
        RaycastHit hit = testRayCast.Hit;

        if (Physics.Raycast(ray, out hit, 10.0f) && hit.collider.CompareTag("REDMON"))
        {
            Debug.Log(hit.collider.name);

            if (hit.transform.position.x > -4.5 && hit.transform.position.x <= -0.1 && flag == true)
            {
                Debug.Log("몇번이나 invoke를 실행했니");

                //레드몬스터의 위치가 플레이어 기준, 오른쪽 벽과 더 가까울 때 왼쪽으로 회피한다
                flag = false;
                hit.transform.GetComponentInParent<Animator>().SetTrigger(h_StrafeLeft);

                if (flag == true)
                {
                    //Invoke 한번만 호출하기 위해 flag 사용
                    Invoke("StrafeLeft", 2.7f);
                }
                //hit.transform.GetComponentInParent<Animator>().ResetTrigger(h_StrafeLeft);
            }

            else if (hit.transform.position.x >= -8.9 && hit.transform.position.x <= -4.5)
            {
                //레드몬스터의 위치가 플레이어 기준, 왼쪽 벽과 더 가까울 때 오른쪽으로 회피한다
                hit.transform.GetComponentInParent<Animator>().SetTrigger(h_StrafeRight);
                //transform.localPosition = transform.localPosition + Vector3.right * 4.0f;
                Invoke("Straferight", 2.0f);

            }

        }
    }
    void StateDie()
    {
        redLaser.SetActive(false);
        animator.SetTrigger(h_Die);
        // + 사운드
    }

    void StrafeLeft()
    {
        transform.Translate(Vector3.left * 4.0f);
    }

    void Straferight()
    {
        transform.localPosition = transform.localPosition + Vector3.right * 4.0f;
    }
}

