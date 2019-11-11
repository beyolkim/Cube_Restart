using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackController : MonoBehaviour
{
    
    public static int scoreCount;
    public static int playerHp;
    public static AttackController instance;

    private int turnRandomNum;
    private WaitForSeconds turncheckDelay = new WaitForSeconds(0.5f);
    public List<GameObject> TurnCheck = new List<GameObject>();

    private void Awake()
    {
        instance = this;
        playerHp = 3;
    }
    private void Start()
    {
        StartCoroutine(AttackTurn());
    }


    IEnumerator AttackTurn()
    {
        while (scoreCount != 10)
        {
            turnRandomNum = Random.Range(0, 3);
            TurnCheck[turnRandomNum].GetComponent<SnakeCubeController>().check_AllAttack = !TurnCheck[turnRandomNum].GetComponent<SnakeCubeController>().check_AllAttack;
            yield return turncheckDelay;
            TurnCheck[turnRandomNum].GetComponent<SnakeCubeController>().check_AllAttack = !TurnCheck[turnRandomNum].GetComponent<SnakeCubeController>().check_AllAttack;
            yield return new WaitForSeconds(3.0f);
        }


    }



}
