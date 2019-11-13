using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class TestDotwwen : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Sequence mySequence = DOTween.Sequence();
        mySequence.Append(transform.DOMove(Vector3.forward * 5.0f, 3.0f))
                  .Join(transform.DORotate(new Vector3(90,90,90),5.0f));           
           

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
