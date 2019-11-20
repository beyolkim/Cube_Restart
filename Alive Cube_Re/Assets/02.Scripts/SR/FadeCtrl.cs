using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeCtrl : MonoBehaviour
{
    public static FadeCtrl instance = null;

    private Animator anim;
    private int fadeIn;
    private int fadeOut;

    
    void Start()
    {
        instance = this;
        anim = GetComponent<Animator>();
        fadeIn = Animator.StringToHash("fadeIn");
        fadeOut = Animator.StringToHash("fadeOut");

        if(this.gameObject.scene.name == "Stage1")
        {
            Debug.Log("ㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡ");
            anim.SetBool(fadeIn, false);
        }
    }

    
    void Update()
    {
        //if (Input.GetMouseButtonDown(0))
        //{
        //    Fade();
        //    Debug.Log("페이드!");
        //}
    }
    public void FadeOut()
    {
        Debug.Log("페이드 아웃!");
        anim.SetBool(fadeOut, true);
    }
}
