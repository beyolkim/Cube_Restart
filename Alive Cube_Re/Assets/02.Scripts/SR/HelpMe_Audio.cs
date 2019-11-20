using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelpMe_Audio : MonoBehaviour
{
    public static HelpMe_Audio instance = null;

    private Transform tr;
    public Transform targetTr;
    private AudioSource _audio;
    public AudioClip[] scaryVoice;
        
    void Start()
    {
        instance = this;

        _audio = GetComponent<AudioSource>();
        tr = GetComponent<Transform>();
        StartCoroutine(ScaryVoice());
    }
    private void Update()
    {
        tr.RotateAround(targetTr.position, Vector3.up, Time.deltaTime * 18);
    }

    IEnumerator ScaryVoice()
    {
        yield return new WaitForSeconds(2);
        _audio.PlayOneShot(scaryVoice[0]);

        yield return new WaitForSeconds(6);
        _audio.clip = scaryVoice[1];
        _audio.Play();
    }
    public void Audio_Volume()
    {
        _audio.volume = 0.3f;
    }
}
