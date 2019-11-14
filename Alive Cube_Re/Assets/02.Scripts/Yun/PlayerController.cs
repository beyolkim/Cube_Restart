using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{

    public static PlayerController instance = null;

    private AudioSource _audio;
    public AudioClip intro_audio;
    public AudioClip countDown_audio;
    public AudioClip gameOver_audio;


    public static int coint = 0;

    public bool playerDie = false;

    void Start()
    {
        instance = this;

        _audio = GetComponent<AudioSource>();
        _audio.clip = intro_audio;
        _audio.Play();

        Debug.Log(AttackController.playerHp);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("CUBE"))
        {
            
            Debug.Log("맞았다!!!");
            AttackController.playerHp--;
            Debug.Log("PlayerHP : " + AttackController.playerHp);
            
        }
    }
    

    void Update()
    {
        if(!playerDie && AttackController.playerHp == 0)
        {            
            Debug.Log("Game Over");
            _audio.PlayOneShot(gameOver_audio);
            playerDie = true;
        }
    }

    public void AudioCtrl()
    {
        _audio.Stop();
    }

    public void CountDown_Audio()
    {
        _audio.PlayOneShot(countDown_audio);

    }
}
