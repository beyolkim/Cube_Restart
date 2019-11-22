using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextCtrl : MonoBehaviour
{
    private Text text;

    void Start()
    {
        text = GetComponent<Text>();
    }

    
    void Update()
    {
        text.text = "Your HP : " + AttackController.playerHp;
    }

    void Test()
    {
        transform.Translate(new Vector3(Random.Range(-4, 4), Random.Range(3, 10), Random.Range(3, 9)));
    }
}
