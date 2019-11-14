using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{

    public static PlayerController instance = null;

    private AudioSource _audio;
    public AudioClip intro_audio;   


    public static int coint = 0; 

    void Start()
    {
        instance = this;

        _audio = GetComponent<AudioSource>();
        _audio.clip = intro_audio;
        _audio.Play();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("CUBE"))
        {
            Debug.Log("맞았다!!!");
            AttackController.playerHp--;
        }
    }

    void Update()
    {
        if(AttackController.playerHp ==0)
        {
            //SceneManager.LoadScene("Yun");

        }
    }
    public void AudioCtrl()
    {
        _audio.Stop();
    }
}
