using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AttackController : MonoBehaviour
{
    
    public static int scoreCount;
    public static int playerHp = 6;
    public static int stage1_Count = 0;
    public GameObject hpUI;
    public static AttackController instance;

    private int turnRandomNum01;
    private int turnRandomNum02;
    private WaitForSeconds turncheckDelay = new WaitForSeconds(0.5f);
    public List<GameObject> TurnCheck = new List<GameObject>();
    public AudioSource stage1_audio;

    //카운트다운
    private GameObject frist_CountObj;
    private Transform[] CountDown;
    public List<GameObject> countDownObj = new List<GameObject>();
   
    private void Awake()
    {
        instance = this;        
    }
    private void Start()
    {
        frist_CountObj = transform.GetChild(0).gameObject;
        CountDown = frist_CountObj.GetComponentsInChildren<Transform>();
        

        for (int i=0; i<CountDown.Length; i++)
        {
            if(CountDown[i].CompareTag("COUNTDOWN"))
            {
                countDownObj.Add(CountDown[i].gameObject);
                
            }
            
        }
        for(int i=0;i<countDownObj.Count; i++)
        {
            countDownObj[i].SetActive(false);
        }


        StartCoroutine(AttackTurn());
    }


    IEnumerator AttackTurn()
    {

        PlayerController.instance.CountDown_Audio();
        //yield return new WaitForSeconds(1f);
        countDownObj[0].SetActive(true);
        yield return new WaitForSeconds(1.5f);
        countDownObj[0].SetActive(false);

        for (int i=1; i<countDownObj.Count-1; i++)
        {
            countDownObj[i].SetActive(true);
            yield return new WaitForSeconds(1f);
            countDownObj[i].SetActive(false);

        }

        countDownObj[10].SetActive(true);
        yield return new WaitForSeconds(2f);
        countDownObj[10].SetActive(false);
        hpUI.gameObject.SetActive(true);

        while (stage1_Count < 3) //Stage1에서 조각이 10개 생겨나기 전까지 공격 반복
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
        Debug.Log("Stage1 Clear!");
        stage1_audio.Stop();
        AttackCube.instance.check_attack = true; //나와있는 큐브들 모두 들어가도록
        yield return new WaitForSeconds(2f);
        FadeCtrl.instance.FadeOut();
        yield return new WaitForSeconds(4);
        SceneManager.LoadScene(2); //Stage1 클리어 -> Stage2 씬 전환
    }

}
