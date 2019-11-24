using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class ShieldCtrl : MonoBehaviour
{
    public static ShieldCtrl instance = null;
    public SteamVR_Input_Sources lefthand = SteamVR_Input_Sources.LeftHand;
    public SteamVR_Action_Boolean trigger = SteamVR_Actions.default_InteractUI;

    private int shieldOpen;
    private Animator anim;
    public bool shieldOn = false;
    private bool shieldAudio = false;

    private GameObject laser;

    public GameObject _weaponUI;
    public GameObject shieldHit_Particle;


    private AudioSource _audio;
    public AudioClip open;
    public AudioClip[] shieldHit;

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
    private void OnParticleCollision(Collision coll)
    {
        if (coll.gameObject.CompareTag("HOMMING"))
        {
            Debug.Log("방패로 막기!");
            ContactPoint contact = coll.contacts[0];
            Vector3 _normal = -contact.normal;

            GameObject _shieldHit_Particle = Instantiate(shieldHit_Particle, contact.point, Quaternion.LookRotation(_normal));
            Destroy(_shieldHit_Particle, 1);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("LASER"))
        {
            laser = other.gameObject;
            _audio.PlayOneShot(shieldHit[Random.Range(0, shieldHit.Length)]);
            laser.GetComponent<SphereCollider>().enabled = false;
            StartCoroutine(LaserCollider());
            Debug.Log("방패에 맞았음");
        }

        if (other.CompareTag("HANDATTACK"))
        {
            Debug.Log("손을 막았다");
            other.gameObject.GetComponentInParent<HandAttack>().ShieldCollsion();
            
            StartCoroutine(other.gameObject.GetComponentInParent<HandAttack>().HandVFX(other.gameObject));          
            
        }

    }
    
    

      
IEnumerator LaserCollider()
    {
        yield return new WaitForSeconds(1.5f);
        laser.GetComponent<SphereCollider>().enabled = true;
        Debug.Log("콜라이더 켜기");
        
    }
}
