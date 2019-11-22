using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Stage3CubeAttack : MonoBehaviour
{
    private List<Transform> BottomAttacks = new List<Transform>();
    private Transform[] tempTr;

    void Start()
    {
        tempTr = this.GetComponentsInChildren<Transform>();
        for (int i =1; i<tempTr.Length; i++)
        {
            BottomAttacks.Add(tempTr[i]);
        }

        StartCoroutine(Attacks());

    }

    IEnumerator Attacks()
    {
        for (int i = 0; i < BottomAttacks.Count; i++)
        {

            BottomAttacks[i].DOMoveY(Random.Range(0.3f, 0.5f), 1.0f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InFlash);
            yield return new WaitForSeconds(0.1f);
        }
    }
   
}
