using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EndingSound : MonoBehaviour
{
    private AudioSource _audio;
    public AudioClip open;
    void Start()
    {
        _audio = GetComponent<AudioSource>();


    }

    // Update is called once per frame
    void FootWalk()
    {
        _audio.PlayOneShot(open);
    }
}
