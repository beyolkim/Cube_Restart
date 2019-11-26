using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckHuman : MonoBehaviour
{
    private Animator human;
    private bool checkAni = false;
    public GameObject fadeAni;

    private void OnTriggerEnter(Collider other)
    {
        if(!checkAni && other.CompareTag("HUMAN"))
        {
            Debug.Log("와우! 멈춰라!");
            human = other.GetComponent<Animator>();
            human.SetTrigger("Move");
            StartCoroutine(ChangeIntro());
            checkAni = true;

        }
        //else if (checkAni && other.CompareTag("HUMAN"))
        //{
        //    Debug.Log("ㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡ");
        //    FadeCtrl.instance.FadeOut();
        //    //fadeAni.GetComponent<Animator>().SetTrigger("FADEOUT");
        //}
    }

    IEnumerator ChangeIntro()
    {
        yield return new WaitForSeconds(13.0f);
        PlayerController.instance._audio.volume = 0.4f;
        FadeCtrl.instance.FadeOut();
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(0);
    }
}
