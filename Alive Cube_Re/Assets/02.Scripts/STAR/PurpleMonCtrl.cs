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
    public Vector3 AttackAngle;

    private Animator animator;
    private GameObject PurpleLaser;

    //애니메이터 파라미터의 문자열을 해시값으로 추출 
    private readonly int IsStrafe = Animator.StringToHash("Is Strafe");
    private readonly int TakeDamage = Animator.StringToHash("Take Damage");
    private readonly int Die = Animator.StringToHash("Die");

    void Awake()
    {
        animator = GetComponent<Animator>();
        PurpleLaser = transform.GetChild(0).gameObject.transform.GetChild(0).gameObject;
    }
    private void Start()
    {
        Invoke("AttackAction", delayAnimation);
    }

    private void AttackAction()
    {
        //앞쪽으로 AttackMode만큼 기울어지며 좌우 레이저 빔 
        .......................................















































































        ......................................












































            .













































        .































        transform.DORotate(AttackAngle, 1.0f);
        animator.SetTrigger(IsStrafe);
        PurpleLaser.SetActive(true);
    }
}