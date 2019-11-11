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

        StartCoroutine(MakeMap());
    }


    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            SceneManager.LoadScene("Intro");

        }
      

    }
    IEnumerator MakeMap()
    {
        GameObject nextObj = Resources.Load("MakeIntroCube") as GameObject;
        Vector3 left_Rot = new Vector3(0, 180.0f, 0);
        Vector3 right_dir = Vector3.zero;
        Vector3 left_dir = Vector3.zero;
        Vector3 left_Rot_Var = new Vector3(0, 180.0f, 270.0f);

        Vector3 right_dir_Var = new Vector3(0.5f, 0, 0.5f);
        //nowObj = Instantiate(nextObj);
        //nowObj.transform.position = tr.position;
        //nowObj.transform.rotation = Quaternion.Euler(left_Rot);
        //nowObj.transform.position = nowObj.transform.position + Vector3.left;


        //Debug.Log("Right_pos" + preTr_right.position + "Right_rot" + preTr_right.rotation);
        //Debug.Log("Left_pos" + preTr_left.position + "Left_rot" + preTr_left.rotation);
        //nextTr.position = tr.position + left_dir;



        for (int i = 0; i < 9; i++)
        {
            GameObject nowObj = Instantiate(nextObj);
            //nowObjs.Add(nowObj);

            if (i % 2 == 0)
            {
                nowObj.transform.position = preTr_right.position + right_dir;
                nowObj.transform.DOLocalRotate(Vector3.forward * -90.0f, 0.5f);
                right_dir = right_dir + Vector3.right;
            }
            else if (i % 2 == 1)
            {
                nowObj.transform.position = tr.position;
                nowObj.transform.rotation = Quaternion.Euler(left_Rot);
                nowObj.transform.position = nowObj.transform.position + Vector3.left + left_dir;

                nowObj.transform.DOLocalRotate(left_Rot_Var, 0.5f);
                //nowObj.transform.DORotate(Vector3.forward * 90.0f, 0.5f);
                left_dir = left_dir + Vector3.left;
            }
            //nowObj.transform.position = preTr_left.position + left_dir;
            yield return new WaitForSeconds(0.1f);
            
        }
      

    }

}
    
