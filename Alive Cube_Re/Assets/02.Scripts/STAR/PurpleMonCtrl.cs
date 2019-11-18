using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

//보라색 몬스터 : 고정 위치에서 좌우로 레이저 발사 
public class PurpleMonCtrl : MonoBehaviour
{
    //시간차 공격을 위한 시작시간 지연
    public float delayAnimation;
    //공격모드일 때 각도
    public Vector3 attackAngle;

    //오디오 
    private AudioSource audioSource;
    public AudioClip[] audioClip;
    public GameObject hitSound;

    private Animator animator;
    //애니메이터 파라미터의 문자열을 해시값으로 추출 
    private readonly int h_Strafe = Animator.StringToHash("Is Strafe");
    private readonly int h_TakeDamage = Animator.StringToHash("Take Damage");
    private readonly int h_Die = Animator.StringToHash("Die");

    private GameObject purpleLaser;
    private GameObject deadParticle;
    private GameObject rigHub;

    void Awake()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        
        purpleLaser = transform.GetChild(0).gameObject.transform.GetChild(0).gameObject;
        deadParticle = transform.GetChild(1).gameObject;
        rigHub = transform.GetChild(0).gameObject;
        //redMonBody = transform.GetChild(2).gameObject; //Tentacles 폴더
    }
    private void Start()
    {
        Invoke("StateAttack", delayAnimation);
    }

    private void StateAttack()
    {
        //앞쪽으로 AttackMode만큼 기울어지며 좌우 레이저 빔 
        transform.DORotate(attackAngle, 1.0f);
        animator.SetTrigger(h_Strafe);
        purpleLaser.SetActive(true);
    }

    private void StateDie()
    {
        rigHub.SetActive(false);
        deadParticle.SetActive(true);
    }
}