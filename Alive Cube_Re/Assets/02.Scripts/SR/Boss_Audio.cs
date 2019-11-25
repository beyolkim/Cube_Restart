using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Audio : MonoBehaviour
{
    public static Boss_Audio instance = null;

    private AudioSource _audio;
    public AudioClip[] bossAudio;
    public AudioClip bossDeath;
    void Start()
    {
        instance = this;
        _audio = GetComponent<AudioSource>();
    }
    
    //void Update()
    //{
        
    //}
    public void BossDeath()
    {
        _audio.PlayOneShot(bossDeath);

    }
    public void BossAudio()
    {
        _audio.PlayOneShot(bossAudio[Random.Range(0, bossAudio.Length)]);
    }
}
