using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class L_SwordCtrl : MonoBehaviour
{
    public static L_SwordCtrl instance = null;
    public SteamVR_Input_Sources lefthand = SteamVR_Input_Sources.LeftHand;
    public SteamVR_Action_Boolean trigger = SteamVR_Actions.default_InteractUI;

    private int swordOpen;
    private Animator anim;
    public bool left_swordOn = false;
    private bool swordAudio = false;
    public GameObject handle;
    public GameObject _weaponUI;

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
        if (!swordAudio && left_swordOn && trigger.GetStateDown(lefthand))
        {
            _weaponUI.gameObject.SetActive(false);
            swordAudio = true;
            anim.SetBool(swordOpen, true);
            _audio.PlayOneShot(open);
            this.gameObject.GetComponent<BoxCollider>().enabled = true;
            handle.gameObject.GetComponent<MeshCollider>().enabled = false;
        }
    }
}
