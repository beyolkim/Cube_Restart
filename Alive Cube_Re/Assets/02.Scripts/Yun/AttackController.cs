using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackController : MonoBehaviour
{
    
    public static int scoreCount;
    public static int playerHp = 6;
    public static AttackController instance;

    private int turnRandomNum01;
    private int turnRandomNum02;
    private WaitForSeconds turncheckDelay = new WaitForSeconds(0.5f);
    public List<GameObject> TurnCheck = new List<GameObject>();

    private void Awake()
    {
        instance = this;        
    }
    private void Start()
    {
        StartCoroutine(AttackTurn());
    }


    IEnumerator AttackTurn()
    {
        //PlayerController.instance.CountDown_Audio();

        //yield return new WaitForSeconds(13f);

        while (playerHp > 0)
        {
            turnRandomNum01 = Random.Range(0, 3);
            turnRandomNum02 = Random.Range(0, 3);
            while(turnRandomNum01 == turnRandomNum02)
            { 
                turnRandomNum02 = Random.Range(0, 3);
            }
            TurnCheck[turnRandomNum01].GetComponent<SnakeCubeController>().check_AllAttack = !TurnCheck[turnRandomNum01].GetComponent<SnakeCubeController>().check_AllAttack;
            TurnCheck[turnRandomNum02].GetComponent<SnakeCubeController>().check_AllAttack = !TurnCheck[turnRandomNum02].GetComponent<SnakeCubeController>().check_AllAttack;
            yield return turncheckDelay;
            TurnCheck[turnRandomNum01].GetComponent<SnakeCubeController>().check_AllAttack = !TurnCheck[turnRandomNum01].GetComponent<SnakeCubeController>().check_AllAttack;
            TurnCheck[turnRandomNum02].GetComponent<SnakeCubeController>().check_AllAttack = !TurnCheck[turnRandomNum02].GetComponent<SnakeCubeController>().check_AllAttack;
            yield return new WaitForSeconds(1.0f);
        }


    }



}
