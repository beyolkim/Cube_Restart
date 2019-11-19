using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeCubeController : MonoBehaviour
{
    public bool check_AllAttack;
    private bool double_Check;
    public enum CUBEDIR
    {
        Front = -15
      , Left = -4
      , Right = -9
    }

    public CUBEDIR attackDir = CUBEDIR.Front;
    public Transform warningCubeTr;


    public List<GameObject> warningCubeObj = new List<GameObject>();

    private Transform GameManagerTr;
    //private Transform[] cubeWallsTr;
    

    public List<GameObject> cubeWallsObj = new List<GameObject>();
    void Start()
    {

        GameManagerTr = this.gameObject.transform;
        CreateWarningCube();
        CreateCubeWalls();
        StartCoroutine(AllAttackCube());
    }

    void CreateWarningCube()
    {

        for (int i = 0; i < warningCubeTr.gameObject.GetComponentsInChildren<Transform>().Length; i++)
        {
            GameObject _testgames = warningCubeTr.gameObject.GetComponentsInChildren<Transform>()[i].gameObject;
            warningCubeObj.Add(_testgames);

        }
        warningCubeObj.RemoveAt(0);
        for (int i = 0; i < warningCubeObj.Count; i++)
        {
            warningCubeObj[i].SetActive(false);
        }

    }


    private void Update()
    {
        if (check_AllAttack == false && double_Check == true)
        {
            double_Check = !double_Check;
            StartCoroutine(AllAttackCube());

        }
    }


    void CreateCubeWalls()
    {
        
        for (int i=0; i< GameManagerTr.gameObject.GetComponentsInChildren<Transform>().Length; i++)
        {
            GameObject _testgames = GameManagerTr.gameObject.GetComponentsInChildren<Transform>()[i].gameObject;
            cubeWallsObj.Add(_testgames);
            if (!_testgames.CompareTag("SNAKEATTACK"))
            {
                cubeWallsObj.Remove(_testgames);
            }

        }
        for (int i=0; i<cubeWallsObj.Count;i++)
        {
            cubeWallsObj[i].SetActive(false);
        }

    }


    

    IEnumerator AllAttackCube()
    {
        while (!check_AllAttack)
        {


            for (int i = 0; i < 2; i++)
            {
                int random_Cube = Random.Range(0, 99);
                //int random_Cube = 0;
                while (((random_Cube)%10 == 0 && attackDir == CUBEDIR.Left) || ((random_Cube) % 9 == 0 && attackDir == CUBEDIR.Right))
                {

                    random_Cube = Random.Range(0, 99);
                    //random_Cube = 0;
                }

                warningCubeObj[random_Cube].SetActive(true);

                
                yield return new WaitForSeconds(2.0f);

                
                warningCubeObj[random_Cube].SetActive(false);
                cubeWallsObj[random_Cube].SetActive(true);
                switch (attackDir)
                {
                    case CUBEDIR.Front:
                        cubeWallsObj[random_Cube].GetComponent<AttackCube>().cubeDir = (float)CUBEDIR.Front;
                        break;
                    case CUBEDIR.Left:
                        cubeWallsObj[random_Cube].GetComponent<AttackCube>().cubeDir = (float)CUBEDIR.Left;
                        break;
                    case CUBEDIR.Right:
                        cubeWallsObj[random_Cube].GetComponent<AttackCube>().cubeDir = (float)CUBEDIR.Right;
                        break;

                }
                cubeWallsObj[random_Cube].GetComponent<AttackCube>().check_attack = false;
            }
            yield return new WaitForSeconds(5.0f);
        }
        double_Check = true;
        yield return null;
    }




}
