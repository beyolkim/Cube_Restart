using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCubeController : MonoBehaviour
{
    public int front_Number;
    public int left_Number;
    public int right_Number;
    public int back_Number;
    public int top_Number;
    public int bottom_Number;




    private WaitForSeconds delayAttack;
    public CubeController[] CubeCheck = new CubeController[6];
    private bool stage_check_01;

    void Start()
    {
        delayAttack = new WaitForSeconds(1.0f);
        //StartCoroutine(AttackNumber());
    }

    //IEnumerator AttackNumber()
    //{
    //    yield return delayAttack;

    //    CubeCheck[0].gameObject.SetActive(true);

    //    yield return delayAttack;
    //    CubeCheck[1].gameObject.SetActive(true);


    //    yield return delayAttack;
    //    CubeCheck[2].gameObject.SetActive(true);


    //    yield return delayAttack;
    //    CubeCheck[3].enabled = true;


    //    yield return delayAttack;
    //    CubeCheck[4].enabled = true;


    //    yield return delayAttack;
    //    CubeCheck[5].enabled = true;


    //}

    
    void Update()
    {
        
        CubeCheck[0].FrontNum = front_Number;
        CubeCheck[1].LeftNum = left_Number;
        CubeCheck[2].RightNum = right_Number;
        CubeCheck[3].BackNum = back_Number;
        CubeCheck[4].TopNum = top_Number;
        CubeCheck[5].BottomNum = bottom_Number;

    }
}
