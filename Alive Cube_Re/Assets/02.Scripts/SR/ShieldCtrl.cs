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

    void Start()
    {
        instance = this;
        anim = GetComponent<Animator>();
        shieldOpen = Animator.StringToHash("shieldOpen");
        
    }
    void Update()
    {
        if(shieldOn && trigger.GetStateDown(lefthand))
        {
            anim.SetBool(shieldOpen, true);            
        }
    }
    
}
