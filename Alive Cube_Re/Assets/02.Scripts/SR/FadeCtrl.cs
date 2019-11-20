using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeCtrl : MonoBehaviour
{
    public static FadeCtrl instance = null;

    private Animator anim;
    private int fadeCtrl;
    private bool fadeState = false;
    
    void Start()
    {
        instance = this;
        anim = GetComponent<Animator>();
        fadeCtrl = Animator.StringToHash("fadeCtrl");
    }

    
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Fade();
            Debug.Log("페이드!");
        }
    }
    public void Fade()
    {
        if (!fadeState)
        {
            Debug.Log("if " + fadeState);
            anim.SetBool(fadeCtrl, true);
            fadeState = true;
        }          
        else if(fadeState)
        {
            Debug.Log("else if " + fadeState);
            anim.SetBool(fadeCtrl, false);
            fadeState = false;
        }

    }
}
