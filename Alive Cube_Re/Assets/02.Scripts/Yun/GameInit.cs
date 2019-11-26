using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInit : MonoBehaviour
{
   
    void Start()
    {
        AttackController.cubeHp = 20;
        SwordCutter.stage1_Count = 0;
        //AttackController.stage1_Count = 0;
        PlayerController.redMon_Kill = 0;
        PlayerController.playerHp = 20;
    }

}
