using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class BoxCtrl : MonoBehaviour
{
    public SteamVR_Input_Sources hand = SteamVR_Input_Sources.Any;
    public SteamVR_Input_Sources righHhand = SteamVR_Input_Sources.RightHand;
    public SteamVR_Input_Sources leftHhand = SteamVR_Input_Sources.LeftHand;

    public SteamVR_Action_Boolean trigger = SteamVR_Actions.default_InteractUI;
    public SteamVR_Action_Vibration haptic = SteamVR_Actions.default_Haptic;

    private int box;
    private int box_G;
    private int box_Shield;
    private Animator anim;

    void Start()
    {
        box = Animator.StringToHash("boxRotate");
        box_G = Animator.StringToHash("G_boxRotate");
        box_Shield = Animator.StringToHash("boxRotate_Shield");
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (this.gameObject.scene.name == "Yun_Test")
        {
            if (trigger.GetStateDown(hand))
            {
                anim.SetBool(box, true);
            }
        }

        if (this.gameObject.scene.name == "Go_Gun")
        {
            if (trigger.GetStateDown(righHhand))
            {
                anim.SetBool(box_G, true);
            }
            if (trigger.GetStateDown(leftHhand))
            {
                anim.SetBool(box_Shield, true);
            }
        }

        //else if (trigger.GetStateUp(hand))
        //{
        //    anim.SetBool(box, false);
        //}
    }
}
