using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class AttackCube : MonoBehaviour
{

    public List<Transform> BodyParts = new List<Transform>();

    //스파이럴움직임
    Vector3[] waitpoint = new Vector3[4];

    //오디오 부분
    private AudioSource attackSound;
    private AudioClip cubeHead_Audio;

    public float cubeDir;

    private Material cubeMats;
    private float cubeAlphaValue;

    public Transform targetTr;

    
    public float minDistance = 0.25f;

    public float speed = 1;
    public float rotationspeed = 50;

    public int beginSize;

    public GameObject bodyprefab;

    Vector3 disTarget;
    private float dis;
    private Transform firstTr;
    private Transform curBodypart;
    private Transform prevBodypart;

    public bool check_attack;
    private bool check_Dotween;
    


    void Start()
    {

       
        for (int i = 0; i < beginSize - 1; i++)
        {
            AddBodyPart();
        }

        firstTr = this.gameObject.transform;
        disTarget = targetTr.position - firstTr.position;

        cubeHead_Audio = Resources.Load<AudioClip>("bonus_energy_004");
        
    }

    void FixedUpdate()
    {
        
        if ((BodyParts[0].localPosition - firstTr.position).z < cubeDir)
        {
            Invoke("delayCheck", 4.0f);
        }
        if (check_attack==false)
        {

            Move();
        }
        else
        {
            ReverseMove();

        }
    }
    void delayCheck()
    {
        check_attack = true;
    }
    public void ReverseMove()
    {
        Sequence mySequence = DOTween.Sequence();

        float curspeed = speed;
        if(check_Dotween == true)
        {
           mySequence.Append(BodyParts[0].DOMove(firstTr.position, 1.0f))
                     .Join(BodyParts[0].DOLocalMoveZ(15.0f, 1.0f));
            //BodyParts[0].DOLocalMoveZ(15.0f, 1.0f);
            //BodyParts[0].DOMove(firstTr.position, 1.0f);
            //BodyParts[0].DOLocalMoveZ(15.0f, 2.0f);
            //오디오 뒤로 갈 경우 AudioSource 끄거나 오디오 사운드 바꾸기?
            attackSound.enabled = false;
            check_Dotween = !check_Dotween;
        }
        SnakeMove(speed);

    }
    //큐브 매터리얼 알파값 조절
    private void CubeAlpha(int _number, float _cubeAlphaValue)
    {
        cubeMats = BodyParts[_number].gameObject.GetComponent<MeshRenderer>().material;
        BodyParts[_number].gameObject.GetComponent<BoxCollider>().enabled = false;
        cubeAlphaValue = _cubeAlphaValue;
        cubeMats.SetFloat("_CubeAlpha", cubeAlphaValue);
    }
    //초기화 부분
    public IEnumerator initCube()
    {
        for (int i = 0; i < BodyParts.Count; i++)
        {
            CubeAlpha(i, 0.0f);
            BodyParts[i].gameObject.GetComponent<BoxCollider>().enabled = false;
            
            //BodyParts[i].gameObject.SetActive(false);
            yield return new WaitForSeconds(0.1f);

        }
        this.gameObject.SetActive(false);
        
        ResetCube();
        AddBodyPart();
        //cubeDir = 0;
        for (int i = 0; i < BodyParts.Count; i++)
        {
            CubeAlpha(i, 1.0f);
            
            BodyParts[i].gameObject.GetComponent<BoxCollider>().enabled = true;
            BodyParts[i].gameObject.transform.position = firstTr.position;
        }
    }

    
    public IEnumerator CubeEexplosion()
    {
        for (int i=0; i<BodyParts.Count; i++)
        {

            BodyParts[i].gameObject.transform.GetChild(0).gameObject.SetActive(true);
            CubeAlpha(i, 0.0f);
            if(i==0)
            {
                GameObject fragment = Instantiate(Resources.Load("PurplePiece") as GameObject);
                fragment.transform.position = BodyParts[0].position;
            }
            yield return new WaitForSeconds(0.1f);

        }
        this.gameObject.SetActive(false);

        ResetCube();
        AddBodyPart();

        for (int i = 0; i < BodyParts.Count; i++)
        {
            BodyParts[i].gameObject.transform.GetChild(0).gameObject.SetActive(false);
            CubeAlpha(i, 1.0f);

            BodyParts[i].gameObject.GetComponent<BoxCollider>().enabled = true;
            BodyParts[i].gameObject.transform.position = firstTr.position;
        }
    }
    public void Move()
    {
        
        float curspeed = speed;
        //Sequence mySequence = DOTween.Sequence();


        if (check_Dotween == false)
        {

            waitpoint[0] = BodyParts[0].transform.position + Vector3.forward * -2.0f;
            waitpoint[1] = BodyParts[0].transform.localPosition + new Vector3(Random.Range(-3.0f, 3.0f), Random.Range(2.0f, 8.0f), Random.Range(3.0f, 7.0f));
            waitpoint[2] = BodyParts[0].transform.localPosition + new Vector3(Random.Range(-3.0f, 3.0f), Random.Range(2.0f, 8.0f), Random.Range(3.0f, 7.0f));
            waitpoint[3] = targetTr.position;

            BodyParts[0].DOPath(waitpoint, 4.0f, PathType.CatmullRom, PathMode.Full3D, 10, Color.red).SetLookAt(targetTr).SetEase(Ease.InFlash);


            //mySequence.Append(BodyParts[0].DOMove(targetTr.position, 2.0f))
            //          .Join(BodyParts[0].DOLookAt(targetTr.eulerAngles, 1.0f, AxisConstraint.None));


            //첫번째 머리 오디오 추가
            attackSound = BodyParts[0].gameObject.AddComponent<AudioSource>();            
            attackSound.loop = true;
            attackSound.spatialize = true;
            attackSound.priority = 10;
            attackSound.spatialBlend = 1;
            attackSound.dopplerLevel = 3;
            attackSound.minDistance = 2;
            attackSound.maxDistance = 50;
            attackSound.rolloffMode = AudioRolloffMode.Logarithmic;
            attackSound.clip = cubeHead_Audio;
            attackSound.Play();




            check_Dotween = !check_Dotween;
            
            
        }
        SnakeMove(speed);
             
    }

    private void SnakeMove(float _curspeed)
    {
        for (int i = 1; i < BodyParts.Count; i++)
        {
            curBodypart = BodyParts[i];
            prevBodypart = BodyParts[i - 1];

            dis = Vector3.Distance(prevBodypart.position, curBodypart.position);

            Vector3 newpos = prevBodypart.position;

            //newpos.y = BodyParts[0].position.y;

            float T = Time.deltaTime * dis / minDistance * _curspeed;
            if (T > 0.5f)
            {
                T = 0.5f;
            }
          

            if (BodyParts[0].position ==targetTr.position)
            {
                
                curBodypart.position = Vector3.Slerp(curBodypart.position, newpos + disTarget.normalized * (-0.5f), T);
                curBodypart.rotation = Quaternion.Slerp(curBodypart.rotation, prevBodypart.rotation, T);
            }
            else
            {
                curBodypart.position = Vector3.Slerp(curBodypart.position, newpos , T);
                curBodypart.rotation = Quaternion.Slerp(curBodypart.rotation, prevBodypart.rotation, T);

            }

        }
    }

    public void AddBodyPart()
    {
        Transform newpart = (Instantiate(bodyprefab, BodyParts[BodyParts.Count - 1].position, BodyParts[BodyParts.Count -1].rotation) as GameObject).transform;
        newpart.SetParent(transform);

        BodyParts.Add(newpart);
        
    }

    public void ResetCube()
    {
        BodyParts.RemoveAt(0);
        Destroy(this.gameObject.transform.GetChild(0).gameObject);
        
    }




}
