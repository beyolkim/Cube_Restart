using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class Test : MonoBehaviour
{
    public SteamVR_Input_Sources hand = SteamVR_Input_Sources.Any;
    public SteamVR_Action_Boolean trigger = SteamVR_Actions.default_InteractUI;
    public SteamVR_Action_Boolean grab = SteamVR_Actions.default_GrabGrip;

    private bool fadeCtrl = true;

    void Start()
    {

    }

    void Update()
    {
        if (fadeCtrl && Input.GetMouseButtonDown(0))
        {
            SteamVR_Fade.Start(Color.black, 0.5f);
            fadeCtrl = false;
            Debug.Log("페이드 아웃!");
        }
        else if(!fadeCtrl && Input.GetMouseButtonDown(0))
        {
            SteamVR_Fade.Start(Color.clear, 0.5f);
            fadeCtrl = true;
            Debug.Log("페이드 인!");
        }
    }
}
