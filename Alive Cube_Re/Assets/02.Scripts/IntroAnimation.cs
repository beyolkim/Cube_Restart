using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;
public class IntroAnimation : MonoBehaviour
{
    public Transform tr;
    private Transform preTr_right;
    private Transform preTr_left;
    private Transform nextTr;
    private List<GameObject> nowObjs = new List<GameObject>();
    void Start()
    {
        preTr_right = tr;
        preTr_left = tr;
        //preTr_left.rotation = Quaternion.Euler(left_Rot);
        //preTr_left.position = preTr_left.position + Vector3.left;
        Debug.Log("Right_pos" + preTr_right.position + "Right_rot" + preTr_right.rotation);
        Debug.Log("Left_pos" + preTr_left.position + "Left_rot" + preTr_left.rotation);

        StartCoroutine(MakeMapLeftRight());

    }


    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            SceneManager.LoadScene("Intro");

        }


    }



    IEnumerator MakeMapLeftRight()
    {
        GameObject nextObj = Resources.Load("MakeIntroCube") as GameObject;
        Vector3 left_Rot = new Vector3(0, 180.0f, 0);
        Vector3 right_dir = Vector3.zero;
        Vector3 left_dir = Vector3.zero;
        Vector3 left_Rot_Var = new Vector3(0, 180.0f, 270.0f);

        //Vector3 right_dir_Var = new Vector3(0.5f, 0, 0.5f);


        for (int i = 0; i < 10; i++)
        {
            GameObject nowObj = Instantiate(nextObj);
            //nowObjs.Add(nowObj);
            if (i % 2 == 0)
            {

                nowObj.transform.position = preTr_right.position + right_dir;
                //nowObj.transform.Rotate(Vector3.forward * -90.0f);
                nowObj.transform.DORotate(Vector3.forward * -90.0f, 0.5f);
                right_dir = right_dir + Vector3.right;
                StartCoroutine(MakeMapForwardBack(nowObj.transform));

            }
            else if (i % 2 == 1)
            {
                nowObj.transform.position = tr.position;
                nowObj.transform.rotation = Quaternion.Euler(left_Rot);
                nowObj.transform.position = nowObj.transform.position + Vector3.left + left_dir;

                nowObj.transform.DORotate(left_Rot_Var, 0.5f);
                //nowObj.transform.DORotate(Vector3.forward * 90.0f, 0.5f);
                left_dir = left_dir + Vector3.left;
                StartCoroutine(MakeMapForwardBack(nowObj.transform));

            }
            //nowObj.transform.position = preTr_left.position + left_dir;
            yield return new WaitForSeconds(0.1f);

        }


    }

    IEnumerator MakeMapForwardBack(Transform _tr)
    {
        GameObject nextObj = Resources.Load("MakeIntroCube") as GameObject;

        Vector3 forward_Rot = new Vector3(0.0f, 270.0f, 0.0f);
        Vector3 forward_dir_Var = new Vector3(-0.5f, 0, 0.5f);
        Vector3 forward_Rot_Var = new Vector3(0, 270.0f, 270.0f);
        Vector3 forward_dir = Vector3.zero;


        for (int i = 0; i < 11; i++)
        {
            GameObject nowObj = Instantiate(nextObj);

            nowObj.transform.position = _tr.position;
            nowObj.transform.rotation = Quaternion.Euler(forward_Rot);
            nowObj.transform.position = nowObj.transform.position + forward_dir_Var + forward_dir;
            nowObj.transform.DORotate(forward_Rot_Var, 0.5f);
            //nowObj.transform.DOPunchPosition(Vector3.up, 0.5f,3);
            forward_dir = forward_dir + Vector3.forward;


            yield return new WaitForSeconds(0.1f);
        }
    }

}

