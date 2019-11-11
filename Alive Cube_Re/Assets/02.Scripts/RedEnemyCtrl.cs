using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

//보라색 몬스터 : 고정 위치에서 좌우로 레이저 발사 
public class RedEnemyCtrl : MonoBehaviour
{
    //공격모드일 때 각도
    public Vector3 AttackAngle;

    public Animator animator;
    private GameObject RedLaser;
    private TestRayCast testRayCast;

    //애니메이터 파라미터의 문자열을 해시값으로 추출 
    private readonly int strafeRight = Animator.StringToHash("Strafe Right");
    private readonly int strafeLeft = Animator.StringToHash("Strafe Left");
    private readonly int isAttack = Animator.StringToHash("Is Attack");
    private readonly int TakeDamage = Animator.StringToHash("Take Damage");
    private readonly int Die = Animator.StringToHash("Die");

    void Awake()
    {
        animator = GetComponent<Animator>();
        RedLaser = transform.GetChild(0).gameObject.transform.GetChild(0).gameObject;
    }
    private void Start()
    {
        Invoke("AttackAction", 0.0f);
    }
    private void AttackAction()
    {
        //스핀모드에서 플레이어를 향해 레이저 빔
        //animator.SetTrigger()
        //animator.SetTrigger(IsStrafe);
        //PurpleLaser.SetActive(true);
    }
}