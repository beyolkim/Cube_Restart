using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitSound : MonoBehaviour
{
    private AudioSource audioSource;
    private AudioClip audioClip;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();

    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("SHIELD"))
        {
            audioSource.Play();
            Debug.Log(other.gameObject);
        }

    }
}
