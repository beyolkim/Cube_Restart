using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeController : MonoBehaviour
{
    //큐브 공격 방향
    public enum CUBEDIR
    {
        Front = 180
      , Left = 90
      , Right = -90
      , Back = 360
      , Top
      , Bottom
    }

    //큐브 1회당 공격 횟수(MainCubeController에서 조작)
    private static int frontNum;
    private static int leftNum;
    private static int rightNum;
    private static int backNum;
    private static int topNum;
    private static int bottomNum;

    // 하위 큐브 오브젝트 및 위치값
    private Transform[] cubeWallsTr;
    private GameObject[] cubeWallsObj;

    private Transform GameManagerTr;

    //공격대상
    public GameObject playerObj;

    //Top & Bottom 시 체크
    public bool check_TopBottom;

    //큐브공격 
    public float attackSpeed;
    

    public CUBEDIR cubedirs = CUBEDIR.Front;
  
    public int FrontNum
    {
        get
        {
            return frontNum;
        }
        set
        {
            frontNum = value;
        }
    }
    public int LeftNum
    {
        get
        {
            return leftNum;
        }
        set
        {
            leftNum = value;
        }
    }
    public int RightNum
    {
        get
        {
            return rightNum;
        }
        set
        {
            rightNum = value;
        }
    }
    public int BackNum
    {
        get
        {
            return backNum;
        }
        set
        {
            backNum = value;
        }
    }
    public int TopNum
    {
        get
        {
            return topNum;
        }
        set
        {
            topNum = value;
        }
    }
    public int BottomNum
    {
        get
        {
            return bottomNum;
        }
        set
        {
            bottomNum = value;
        }
    }

   
    
    void Start()
    {
        GameManagerTr = this.gameObject.transform;
        cubeWallsObj = new GameObject[200];
        CreateCubeWalls();
        InvokeRepeating("Attack_replay", 1.0f, 5.0f);
    }

    void CreateCubeWalls()
    {
        // GetComponentsInChildren 사용시 자신의 오브젝트 값도 포함되기 때문에 오브젝트 변환시 맨 처음 값은 빼줌
        cubeWallsTr = GameManagerTr.gameObject.GetComponentsInChildren<Transform>();
    
        for (int i = 1; i < cubeWallsTr.Length; i++)
        {
            cubeWallsObj[i - 1] = cubeWallsTr[i].gameObject;
        
        }
    }

   
    // 공격횟수
    void Attack_replay()
    {
        switch (cubedirs)
        {
            case CUBEDIR.Front:
                for (int i = 0; i < frontNum; i++)
                    StartCoroutine(CubeMove());
                break;
            case CUBEDIR.Left:
                for (int i = 0; i < leftNum; i++)
                    StartCoroutine(CubeMove());
                break;
            case CUBEDIR.Right:
                for (int i = 0; i < rightNum; i++)
                    StartCoroutine(CubeMove());
                break;
            case CUBEDIR.Back:
                for (int i = 0; i < backNum; i++)
                    StartCoroutine(CubeMove());
                break;
            case CUBEDIR.Top:
                for (int i = 0; i < topNum; i++)
                    StartCoroutine(CubeMove());
                break;
            case CUBEDIR.Bottom:
                for (int i = 0; i < bottomNum; i++)
                    StartCoroutine(CubeMove());
                break;
        }
    }

    IEnumerator CubeMove()
    {
        // 랜덤 위치 및 랜덤 큐브값
        int random_Cube = Random.Range(0, 99);
        int random_Pos = Random.Range(4, 7);

        Vector3 testVec = playerObj.transform.position;
        // 큐브 공격 가이드 라인
        //Debug.Log(random_Cube + "가이드");
        if (random_Cube < 60 && check_TopBottom == false) // 상단 공격(60이하)
        {
            
            iTween.LookTo(cubeWallsObj[random_Cube + 100], iTween.Hash("looktarget", testVec,
                                                                 "time", 0.1f));
            iTween.MoveTo(cubeWallsObj[random_Cube + 100], iTween.Hash("position", testVec,
                                                                "time", 0.1f));
        }
        else
        {
            iTween.MoveBy(cubeWallsObj[random_Cube + 100], iTween.Hash("z", random_Pos * -1
                                           , "time", 0.1f
                                           ));
        }

        // 2초후 가이드 라인 없어짐
        yield return new WaitForSeconds(2.0f);

        if (random_Cube < 60 && check_TopBottom == false) // 상단 공격(60이하)
        {
            
            iTween.RotateTo(cubeWallsObj[random_Cube + 100], iTween.Hash("rotation", new Vector3(0.0f, (float)cubedirs, 0f),
                                                                    "time", 0.1f));
            iTween.MoveTo(cubeWallsObj[random_Cube + 100], iTween.Hash("position", cubeWallsTr[random_Cube+1],
                                                                    "time", 0.1f));
        }
        else
        {

            iTween.MoveBy(cubeWallsObj[random_Cube + 100], iTween.Hash("z", random_Pos,
                                                                      "time", 0.1f));
        }

        // 실제 큐브 공격

        //Debug.Log(random_Cube + "실제공격");
        if (random_Cube < 60 && check_TopBottom == false ) // 상단 공격(유도)
        {
            
            iTween.LookTo(cubeWallsObj[random_Cube], iTween.Hash("looktarget", testVec, 
                                                                 "time", attackSpeed));
            //cubeWallsObj[random_Cube].transform.position = cubeWallsObj[random_Cube].transform.position + (Vector3.zero * -50);

            //yield return new WaitForSeconds(5f);

            iTween.MoveTo(cubeWallsObj[random_Cube], iTween.Hash("position", testVec, 
                                                                "time", attackSpeed));
        }
        iTween.MoveBy(cubeWallsObj[random_Cube], iTween.Hash("z", random_Pos * -1,
                                                             "time", 1.0f));

        // 공격 되돌아 가기

        yield return new WaitForSeconds(2f);


        if (random_Cube < 60 && check_TopBottom == false)
        {
            switch (cubedirs)
            {
                case CUBEDIR.Front:
                    iTween.RotateTo(cubeWallsObj[random_Cube], iTween.Hash("rotation", new Vector3(0.0f, (float)cubedirs, 0f),
                                                                    "time", attackSpeed));
                    iTween.MoveTo(cubeWallsObj[random_Cube], iTween.Hash("position", cubeWallsTr[random_Cube+101],
                                                                            "time", attackSpeed));

                    break;
                case CUBEDIR.Left:
                    iTween.RotateTo(cubeWallsObj[random_Cube], iTween.Hash("rotation", new Vector3(0.0f, (float)cubedirs, 0f),
                                                                    "time", attackSpeed));
                    iTween.MoveTo(cubeWallsObj[random_Cube], iTween.Hash("position", cubeWallsTr[random_Cube+ 101],
                                                                            "time", attackSpeed));

                    break;
                case CUBEDIR.Right:
                    iTween.RotateTo(cubeWallsObj[random_Cube], iTween.Hash("rotation", new Vector3(0.0f, (float)cubedirs, 0f),
                                                                    "time", attackSpeed));
                    iTween.MoveTo(cubeWallsObj[random_Cube], iTween.Hash("position", cubeWallsTr[random_Cube+ 101],
                                                                            "time", attackSpeed));
                    break;
                case CUBEDIR.Back:
                    iTween.RotateTo(cubeWallsObj[random_Cube], iTween.Hash("rotation", new Vector3(0.0f, (float)cubedirs, 0f),
                                                                    "time", attackSpeed));
                    iTween.MoveTo(cubeWallsObj[random_Cube], iTween.Hash("position", cubeWallsTr[random_Cube+ 101],
                                                                            "time", attackSpeed));
                    break;

            }

        }
       
        iTween.MoveBy(cubeWallsObj[random_Cube], iTween.Hash("z", random_Pos,
                                                             "time", 1.0f));

    }
}
