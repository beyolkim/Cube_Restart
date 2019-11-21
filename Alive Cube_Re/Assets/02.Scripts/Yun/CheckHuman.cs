using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckHuman : MonoBehaviour
{
    private Animator human;
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("HUMAN"))
        {
            Debug.Log("와우! 멈춰라!");
            human = other.GetComponent<Animator>();
            human.SetTrigger("Move");
        }
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
