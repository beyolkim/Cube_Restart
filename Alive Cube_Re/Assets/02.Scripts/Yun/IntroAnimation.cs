using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class IntroAnimation : MonoBehaviour
{
    //빈오브젝트 생성
    GameObject emptyGameObject;

    //메인큐브맵 오브젝트
    public GameObject maincubeMap;

    public Transform tr;    
    private Transform preTr_right;
    private Transform preTr_left;
    private Transform nextTr;
    private List<GameObject> nowObjs = new List<GameObject>();
    private bool mapMake = false;
    //인스턴스 큐브 오브젝트
    private GameObject nextObj;

    //큐브 매터리얼 정보
    private Material cubeLine;

    private GameObject tempObj; 

    private Animator anim;
    private int cubeTouch;
    private AudioSource _audio;
    public AudioClip cube_default_audio;
    public AudioClip mapStart_audio;
    public AudioClip mapMaking_audio;
    public AudioClip mapMakingEnd_audio;

    void Start()
    {
        preTr_right = tr;
        preTr_left = tr;
        anim = GetComponent<Animator>();
        cubeTouch = Animator.StringToHash("cubeTouch");
        _audio = GetComponent<AudioSource>();
        _audio.clip = cube_default_audio;
        _audio.loop = true;
        _audio.Play();
        //preTr_left.rotation = Quaternion.Euler(left_Rot);
        //preTr_left.position = preTr_left.position + Vector3.left;
        Debug.Log("Right_pos" + preTr_right.position + "Right_rot" + preTr_right.rotation);
        Debug.Log("Left_pos" + preTr_left.position + "Left_rot" + preTr_left.rotation);
        emptyGameObject = new GameObject("TEMP");
        //StartCoroutine(MakeMapLeftRight());
        //StartCoroutine(MakeMapTop());
        //StartCoroutine(MakeMapLeftWall(tr));
        //StartCoroutine(MakeMapRightWall(tr));
        nextObj = Resources.Load("MakeIntroCube") as GameObject;
        cubeLine = Resources.Load("CubeGridLines_Glow") as Material;

    }


    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            SceneManager.LoadScene("Intro");

        }
        
    }

    

    IEnumerator MakeMapTop()
    {
        //GameObject nextObj = Resources.Load("MakeIntroCube") as GameObject;
        Vector3 left_Rot = new Vector3(0, 180.0f, 0);
        Vector3 right_dir = Vector3.zero;
        Vector3 left_dir = Vector3.zero;
        Vector3 left_Rot_Var = new Vector3(0, 180.0f, 270.0f);
        Vector3 temp_dir = new Vector3(0, 10.0f, 0.0f);
        //Vector3 right_dir_Var = new Vector3(0.5f, 0, 0.5f);
        GameObject temp_Obj = new GameObject();
        yield return new WaitForSeconds(1.5f);
        for (int i = 0; i < 12; i++)
        {
            GameObject nowObj = Instantiate(nextObj);
            nowObj.transform.parent = emptyGameObject.transform;
            //nowObjs.Add(nowObj);
            if (i % 2 == 0)
            {

                nowObj.transform.position = preTr_right.position + right_dir + temp_dir;
                
                //nowObj.transform.Rotate(Vector3.forward * -90.0f);
                nowObj.transform.DORotate(Vector3.forward * -90.0f, 0.5f);

                right_dir = right_dir + Vector3.right;
                StartCoroutine(MakeMapForwardBack(nowObj.transform));

                

            }
            else if (i % 2 == 1)
            {
                nowObj.transform.position = tr.position + temp_dir;
                nowObj.transform.rotation = Quaternion.Euler(left_Rot);
                nowObj.transform.position = nowObj.transform.position + Vector3.left + left_dir;

                nowObj.transform.DORotate(left_Rot_Var, 0.5f);
                //nowObj.transform.DORotate(Vector3.forward * 90.0f, 0.5f);
                left_dir = left_dir + Vector3.left;
                StartCoroutine(MakeMapForwardBack(nowObj.transform));
             
            }
            yield return new WaitForSeconds(0.1f);
        }
    }




    IEnumerator MakeMapLeftRight()
    {
        //GameObject nextObj = Resources.Load("MakeIntroCube") as GameObject;
        Vector3 left_Rot = new Vector3(0, 180.0f, 0);
        Vector3 right_dir = Vector3.zero;
        Vector3 left_dir = Vector3.zero;
        Vector3 left_Rot_Var = new Vector3(0, 180.0f, 270.0f);
        Vector3 temp_dir = new Vector3(0, 10.0f, 0.0f);
        //Vector3 right_dir_Var = new Vector3(0.5f, 0, 0.5f);
        GameObject temp_Obj = new GameObject();

        for (int i = 0; i < 12; i++)
        {
            GameObject nowObj = Instantiate(nextObj);
            nowObj.transform.parent = emptyGameObject.transform;

            if (i % 2 == 0)
            {

                nowObj.transform.position = preTr_right.position + right_dir;
                temp_Obj.transform.position = preTr_right.position + right_dir + temp_dir;
                //nowObj.transform.Rotate(Vector3.forward * -90.0f);
                nowObj.transform.DORotate(Vector3.forward * -90.0f, 0.5f);
                
                right_dir = right_dir + Vector3.right;
                StartCoroutine(MakeMapForwardBack(nowObj.transform));
                
                StartCoroutine(MakeMapUp(nowObj.transform,11));
                StartCoroutine(MakeMapUp(nowObj.transform, 0));

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
                StartCoroutine(MakeMapUp(nowObj.transform,11));
                StartCoroutine(MakeMapUp(nowObj.transform,0));
            }
            yield return new WaitForSeconds(0.1f);
        }
    }

    IEnumerator MakeMapForwardBack(Transform _tr)
    {
        //GameObject nextObj = Resources.Load("MakeIntroCube") as GameObject;

        Vector3 forward_Rot = new Vector3(0.0f, 270.0f, 0.0f);
        Vector3 forward_dir_Var = new Vector3(-0.5f, 0, 0.5f);
        Vector3 forward_Rot_Var = new Vector3(0, 270.0f, 270.0f);
        Vector3 forward_dir = Vector3.zero;


        for (int i = 0; i < 12; i++)
        {
            GameObject nowObj = Instantiate(nextObj);
            nowObj.transform.parent = emptyGameObject.transform;

            nowObj.transform.position = _tr.position;
            nowObj.transform.rotation = Quaternion.Euler(forward_Rot);
            nowObj.transform.position = nowObj.transform.position + forward_dir_Var + forward_dir;
            nowObj.transform.DORotate(forward_Rot_Var, 0.5f);
            //nowObj.transform.DOPunchPosition(Vector3.up, 0.5f,3);
            forward_dir = forward_dir + Vector3.forward;


            yield return new WaitForSeconds(0.1f);

        }

    }

    IEnumerator MakeMapUp(Transform _tr, int _dirnum)
    {
        //GameObject nextObj = Resources.Load("MakeIntroCube") as GameObject;
        Vector3 up_Rot = new Vector3(0.0f, 0.0f, 180.0f);
        Vector3 up_dir_Var = new Vector3(1.0f, 1.0f, 0.0f);
        Vector3 up_Rot_Var = new Vector3(0.0f, 0.0f, 270.0f);
        Vector3 up_dir = Vector3.zero;

        yield return new WaitForSeconds(1.0f);

        for (int i = 0; i < 11; i++)
        {
            GameObject nowObj = Instantiate(nextObj);
            nowObj.transform.parent = emptyGameObject.transform;

            nowObj.transform.position = _tr.position + Vector3.forward * _dirnum + Vector3.left * 2;
            nowObj.transform.rotation = Quaternion.Euler(up_Rot);
            nowObj.transform.position = nowObj.transform.position + up_dir_Var + up_dir;
            nowObj.transform.DORotate(up_Rot_Var, 0.5f);

            up_dir = up_dir + Vector3.up;

            yield return new WaitForSeconds(0.1f);
        }
    }

    IEnumerator MakeMapLeftWall(Transform _tr)
    {
        //GameObject nextObj = Resources.Load("MakeIntroCube") as GameObject;
        Vector3 LeftWall_Rot = new Vector3(0.0f, 0.0f, 90.0f);
        Vector3 LeftWall_dir_Var = new Vector3(0.0f, -1.0f, 0.0f);
        Vector3 LeftWall_Rot_Var = new Vector3(0.0f, 0.0f, 0.0f);
        Vector3 LeftWall_dir = Vector3.zero;
        //GameObject tempObj = new GameObject();
        yield return new WaitForSeconds(1.0f);

        for (int i = 0; i < 12; i++)
        {
            GameObject nowObj = Instantiate(nextObj);
            nowObj.transform.parent = emptyGameObject.transform;

            //tempObj.transform.position = nowObj.transform.position;
            nowObj.transform.position = _tr.position + Vector3.right * 5;
            nowObj.transform.rotation = Quaternion.Euler(LeftWall_Rot);
            nowObj.transform.position = nowObj.transform.position + LeftWall_dir_Var + LeftWall_dir;
            nowObj.transform.DORotate(LeftWall_Rot_Var, 0.5f);
            //tempObj.transform.position = nowObj.transform.position + Vector3.left * 10;

            StartCoroutine(MakeMapForwardBack(nowObj.transform));


            //StartCoroutine(MakeMapForwardBack(tempObj.transform));

            LeftWall_dir = LeftWall_dir + Vector3.up;

            yield return new WaitForSeconds(0.1f);
        }

    }
    

    IEnumerator MakeMapRightWall(Transform _tr)
    {
        //GameObject nextObj = Resources.Load("MakeIntroCube") as GameObject;
        Vector3 RightWall_Rot = new Vector3(0.0f, 0.0f, 90.0f);
        Vector3 RightWall_dir_Var = new Vector3(0.0f, -1.0f, 0.0f);
        Vector3 RightWall_Rot_Var = new Vector3(0.0f, 0.0f, 0.0f);
        Vector3 RightWall_dir = Vector3.zero;
        //GameObject tempObj = new GameObject();
        yield return new WaitForSeconds(1.0f);

        for (int i = 0; i < 12; i++)
        {
            GameObject nowObj = Instantiate(nextObj);
            nowObj.transform.parent = emptyGameObject.transform;

            //tempObj.transform.position = nowObj.transform.position;
            nowObj.transform.position = _tr.position + Vector3.left * 6;
            nowObj.transform.rotation = Quaternion.Euler(RightWall_Rot);
            nowObj.transform.position = nowObj.transform.position + RightWall_dir_Var + RightWall_dir;
            nowObj.transform.DORotate(RightWall_Rot_Var, 0.5f);
            //tempObj.transform.position = nowObj.transform.position + Vector3.left * 10;

            StartCoroutine(MakeMapForwardBack(nowObj.transform));


            RightWall_dir = RightWall_dir + Vector3.up;

            yield return new WaitForSeconds(0.1f);
        }

    }

    private void OnTriggerEnter(Collider coll)
    {
        if(!mapMake)
        {
            Debug.Log("걸려 들어쓰");
            mapMake = true;
            StartCoroutine(MapMaking());            
        }        
    }

    IEnumerator MapMaking()
    {
        yield return new WaitForSeconds(0.4f);
        _audio.Stop();
        _audio.PlayOneShot(mapStart_audio);

        yield return new WaitForSeconds(0.5f);
        anim.SetBool(cubeTouch, true);


        yield return new WaitForSeconds(2.0f);
        
        tr.transform.position = tr.transform.position + Vector3.back*4.5f + Vector3.down*2.2f; // 큐브 사라진 후 위치 재조정(사람 뒤에서 부터 생성)

        _audio.PlayOneShot(mapMaking_audio);

        gameObject.GetComponent<MeshRenderer>().enabled = true;
        StartCoroutine(MakeMapLeftRight());
        StartCoroutine(MakeMapTop());
        StartCoroutine(MakeMapLeftWall(tr));
        StartCoroutine(MakeMapRightWall(tr));
                    
        yield return new WaitForSeconds(4.3f);
        PlayerController.instance.AudioCtrl();
        _audio.PlayOneShot(mapMakingEnd_audio);
        
        //큐브라인 생성
        cubeLine.DOVector(new Vector2(2, 2), "_WallCubeGrid", 2.5f).SetEase(Ease.OutQuad);

        yield return new WaitForSeconds(4);
        this.gameObject.SetActive(false);

        maincubeMap.SetActive(true);

        cubeLine.DOVector(new Vector2(1, 1), "_WallCubeGrid", 0.1f);
        Destroy(emptyGameObject);
    }

}


