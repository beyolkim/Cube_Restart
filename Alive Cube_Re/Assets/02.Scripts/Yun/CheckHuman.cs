using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckHuman : MonoBehaviour
{
    private Animator human;
    private bool checkAni;
    public GameObject fadeAni;
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("HUMAN"))
        {
            //Debug.Log("와우! 멈춰라!");
            human = other.GetComponent<Animator>();
            human.SetTrigger("Move");
            checkAni = true;

        }
        if (other.CompareTag("HUMAN")&&checkAni == true)
        {
            fadeAni.GetComponent<Animator>().SetTrigger("FADEOUT");


        }
    }

    //IEnumerator chageIntro()
    //{
    //    yield return new WaitForSeconds(4.0f);
    //    SceneManager.LoadScene(0);
    //}
}
