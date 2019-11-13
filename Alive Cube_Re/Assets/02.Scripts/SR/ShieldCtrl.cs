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
            shieldAudio = true;
            anim.SetBool(shieldOpen, true);
            _audio.PlayOneShot(open);
        }
    }

    private void OnCollisionEnter(Collision coll)
    {
        if(coll.transform.CompareTag("CUBE"))
        {
            _audio.PlayOneShot(shieldHit[Random.Range(0, shieldHit.Length)]);
        }
    }
    
}
