using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class TestDotwwen : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        transform.DOSpiral(3, null, SpiralMode.ExpandThenContract, 0.5f, 5);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
