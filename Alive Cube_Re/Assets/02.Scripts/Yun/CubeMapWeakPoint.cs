using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class CubeMapWeakPoint : MonoBehaviour
{
    //큐브 약점 오브젝트
    private List<GameObject> WeakPoints = new List<GameObject>();

    //임시저장소들
    private Transform[] Temp_WeakPoint;
    private List<int> tempNum = new List<int>();


    //큐브약점포인트 색깔
    private Material [] WeakPointMat;
    private Color nowColor;

    private Material warringmat;

    //큐브약점포인트 한면에 나타나는 횟수
    public int weakPointCount;

    

    private int rannum;
    void Start()
    {
        warringmat = Resources.Load("WarningCube3rd") as Material;
        Temp_WeakPoint = this.GetComponentsInChildren<Transform>();
        for (int i = 0; i < Temp_WeakPoint.Length; i++)
        {
            if (Temp_WeakPoint[i].CompareTag("WEAKPOINT"))
            {
                WeakPoints.Add(Temp_WeakPoint[i].gameObject);
            }
        }
        //WeakPointMat = WeakPoints[0].GetComponent<MeshRenderer>().material;
        //nowColor = WeakPointMat.GetVector("_WallColor");
        StartCoroutine(ChageMat());
    }

    IEnumerator ChageMat()
    {
        while(AttackController.cubeHp >0)
        {
            for (int i=0; i< weakPointCount; i++)
            {
                rannum = Random.Range(0, WeakPoints.Count - 1);
                tempNum.Add(rannum);
                WeakPointMat = WeakPoints[rannum].GetComponent<MeshRenderer>().materials;

                WeakPointMat[1].SetVector("_VectorAlpha", new Vector2(0, 1));
                //chagneMat = warringmat;
                //chagneMat.SetVector("_WallColor", Color.red * 5f);
                
                WeakPoints[rannum].GetComponent<WallCubeController>().setCheck = true;
            }
            yield return new WaitForSeconds(3.0f);
            for (int i=0; i< weakPointCount; i++)
            {
                WeakPointMat = WeakPoints[tempNum[i]].GetComponent<MeshRenderer>().materials;
                WeakPointMat[1].SetVector("_VectorAlpha", new Vector2(0, 0));
                WeakPoints[tempNum[i]].GetComponent<WallCubeController>().setCheck = false;
            }
            tempNum.Clear();
        }

    }

}
