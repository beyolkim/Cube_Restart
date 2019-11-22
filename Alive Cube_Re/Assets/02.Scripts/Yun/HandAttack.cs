using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class HandAttack : MonoBehaviour
{
    // Start is called before the first frame update
    Tween handAttack;
    void Start()
    {
        handAttack = transform.DOLocalRotate(new Vector3(240, 0, 180), 3.0f).SetEase(Ease.InFlash);
    }

    
    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            transform.DOLocalRotate(new Vector3(45, 0, 180), 3.0f).SetEase(Ease.OutFlash);

        }
        else if(Input.GetMouseButtonDown(1))
        {
            transform.DOLocalRotate(new Vector3(240, 0, 180), 3.0f).SetEase(Ease.InFlash);
            

        }
    }
}
