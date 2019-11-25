using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using UnityEngine.Experimental.VFX;

public class ShieldCtrl : MonoBehaviour
{
    public static ShieldCtrl instance = null;
    public SteamVR_Input_Sources lefthand = SteamVR_Input_Sources.LeftHand;
    public SteamVR_Action_Boolean trigger = SteamVR_Actions.default_InteractUI;

    private int shieldOpen;
    private Animator anim;
    public bool shieldOn = false;
    private bool shieldAudio = false;

    public GameObject _weaponUI;
    public GameObject shieldHit_Particle;


    private AudioSource _audio;
    public AudioClip open;
    public AudioClip[] shieldHit;

   
    //실드 파티클
    public GameObject shieldparticle;
    void Start()
    {
        instance = this;
        anim = GetComponent<Animator>();
        shieldOpen = Animator.StringToHash("shieldOpen");
        _audio = GetComponent<AudioSource>();
        
    }
    void Update()
    {
        if(!shieldAudio && shieldOn && trigger.GetStateDown(lefthand))
        {
            _weaponUI.gameObject.SetActive(false);
            shieldAudio = true;
            anim.SetBool(shieldOpen, true);
            _audio.PlayOneShot(open);
        }
    }

    private void OnTriggerEnter(Collider other)

    {
        if (other.transform.CompareTag("LASER"))
        {
            _audio.PlayOneShot(shieldHit[Random.Range(0, shieldHit.Length)]);
            Debug.Log("방패에 맞았음");
        }

        if (other.CompareTag("HANDATTACK"))
        {
            //Debug.Log("손을 막았다");
            _audio.PlayOneShot(shieldHit[Random.Range(0, shieldHit.Length)]);
            other.gameObject.GetComponentInParent<HandAttack>().ShieldCollsion();
            shieldparticle.transform.position = this.gameObject.transform.position + Vector3.forward*0.5f;
            if (AttackController.cubeHp <= 15 && AttackController.cubeHp > 10)  //파티클 파란색으로 바꾸기
            {
                shieldparticle.GetComponent<VisualEffect>().SetFloat("FirstColor", 0f);
                shieldparticle.GetComponent<VisualEffect>().SetFloat("SecondColor", 3f);
                shieldparticle.GetComponent<VisualEffect>().SetFloat("ThirdColor", 0f);
            }
            else if (AttackController.cubeHp <= 10)  //파티클 빨강색으로 바꾸기
            {
                shieldparticle.GetComponent<VisualEffect>().SetFloat("FirstColor", 0f);
                shieldparticle.GetComponent<VisualEffect>().SetFloat("SecondColor", 0f);
                shieldparticle.GetComponent<VisualEffect>().SetFloat("ThirdColor", 3f);
            }

            shieldparticle.GetComponent<VisualEffect>().SetInt("ParticleCount", 150); // 파티클 발생
            shieldparticle.GetComponent<VisualEffect>().SendEvent("OnPlay");
            StartCoroutine(other.gameObject.GetComponentInParent<HandAttack>().HandVFX(other.gameObject));          
            
        }

    }
    public void ShieldHit_Audio()
    {
        _audio.PlayOneShot(shieldHit[Random.Range(0, shieldHit.Length)]);
    }

}
