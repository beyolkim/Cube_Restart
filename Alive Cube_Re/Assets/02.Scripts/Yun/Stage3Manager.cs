using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage3Manager : MonoBehaviour
{
    public GameObject Attacks;
    public CubeMapWeakPoint[] WeakPointonoff = new CubeMapWeakPoint[3];
    void Start()
    {
        StartCoroutine(Start3stage());
    }

    IEnumerator Start3stage()
    {
        yield return new WaitForSeconds(3.0f);
        Attacks.SetActive(true);
        for(int i=0; i< WeakPointonoff.Length;i++)
        {
            WeakPointonoff[i].enabled = true;
        }

    }
    void Update()
    {
        
    }
}
