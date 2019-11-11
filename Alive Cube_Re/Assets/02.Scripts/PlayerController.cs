using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    void Start()
    {
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
}
