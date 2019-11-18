using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class R_SwordCtrl : MonoBehaviour
{
    public static R_SwordCtrl instance = null;
    public SteamVR_Input_Sources righthand = SteamVR_Input_Sources.RightHand;
    public SteamVR_Action_Boolean trigger = SteamVR_Actions.default_InteractUI;

    private int swordOpen;
    private Animator anim;
    public bool right_swordOn = false;
    private bool swordAudio = false;

    private AudioSource _audio;
    public AudioClip open;

    void Start()
    {
        instance = this;
        anim = GetComponent<Animator>();
        _audio = GetComponent<AudioSource>();
        swordOpen = Animator.StringToHash("swordOpen");
    }

    void Update()
    {
        if (!swordAudio && right_swordOn && trigger.GetStateDown(righthand))
        {
            swordAudio = true;
            anim.SetBool(swordOpen, true);
            _audio.PlayOneShot(open);
        }
    }    
}
