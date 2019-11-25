using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInit : MonoBehaviour
{
   
    void Start()
    {
        AttackController.cubeHp = 20;
        PlayerController.redMon_Kill = 0;
        PlayerController.playerHp = 20;
    }

}
