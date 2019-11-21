using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AliveCube_UI : MonoBehaviour
{
    public static AliveCube_UI instance = null;

    private Animator anim;
    private int aliveCube_UI;

    void Start()
    {
        instance = this;
        anim = GetComponent<Animator>();
        aliveCube_UI = Animator.StringToHash("AliveCube");
    }


    //void Update()
    //{
    //    if(Input.GetMouseButtonDown(0))
    //    {
    //        anim.SetBool(aliveCube_UI, true);
    //    }
    //}

    public void AliveCube_UI_Open()
    {
        anim.SetBool(aliveCube_UI, true);
    }
}
