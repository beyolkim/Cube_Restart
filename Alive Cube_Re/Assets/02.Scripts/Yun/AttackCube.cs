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
    private AudioClip[] cubeHead_Audio = new AudioClip[3];

    public float cubeDir;

    private Material cubeMats;
    private float cubeAlphaValue;

    public Transform[] targetTr;

    
    public float minDistance = 0.25f;

    public float speed = 1;
    public float rotationspeed = 50;

    public int beginSize;

    public GameObject headprefab;
    public GameObject bodyprefab;

    Vector3 disTarget;
    private float dis;
    private Transform firstTr;
    private Transform curBodypart;
    private Transform prevBodypart;

    public bool check_attack;
    private bool check_Dotween;

    public bool targetCheck;
   
    void Start()
    {

        firstTr = this.gameObject.transform;
        AddBodyPart();
        // 머리를 제외한 꼬리부분 생성
        //for (int i = 0; i < beginSize; i++)
        //{
        //    //BodyParts[i+1].localScale = BodyParts[i + 1].localScale - ((Vector3.one)*(i+1)*0.05f);
        //}

        //disTarget = targetTr.position - firstTr.position;
        
        cubeHead_Audio[0] = Resources.Load<AudioClip>("Whispering1");
        cubeHead_Audio[1] = Resources.Load<AudioClip>("Whispering2");
        cubeHead_Audio[2] = Resources.Load<AudioClip>("Whispering3");

    }

    void FixedUpdate()
    {
        //if ((BodyParts[0].localPosition - firstTr.position).z < cubeDir)
        if (targetCheck == true) // 타켓에 도착시 
        {
            Invoke("delayCheck", 3.0f);
            
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
        for (int i = 0; i < beginSize; i++)
        {
            CubeAlpha(i, 1.0f);
            
            BodyParts[i].gameObject.GetComponent<BoxCollider>().enabled = true;
            BodyParts[i].gameObject.transform.position = firstTr.position;
        }
        targetCheck = false;
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
            waitpoint[3] = targetTr[Random.Range(0, targetTr.Length)].position;

            Tween attackSnake = BodyParts[0].DOPath(waitpoint, 3.0f, PathType.CatmullRom, PathMode.Full3D, 10, Color.red).SetLookAt(waitpoint[3]);

            //난이도 조절구간
            if (PlayerController.instance.attackAllow)
            {
                attackSnake.SetEase(Ease.InFlash);                
                PlayerController.instance.curTime = 0;
            }


            //첫번째 머리 오디오 추가
            attackSound = BodyParts[0].gameObject.AddComponent<AudioSource>();            
            attackSound.loop = true;
            attackSound.spatialize = true;
            attackSound.priority = 10;
            attackSound.volume = 1;
            attackSound.spatialBlend = 1;
            attackSound.dopplerLevel = 3;
            attackSound.minDistance = 3;
            attackSound.maxDistance = 50;
            attackSound.rolloffMode = AudioRolloffMode.Logarithmic;
            attackSound.clip = cubeHead_Audio[Random.Range(0, cubeHead_Audio.Length)];
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
          

            if (targetCheck == true)
            {
                
                curBodypart.position = Vector3.Slerp(curBodypart.position, newpos + disTarget.normalized * (-1.0f), T);
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
        Transform newpart;
        for (int i=0; i<beginSize; i++)
        {

            if (i ==0)
            {
                newpart = (Instantiate(headprefab,firstTr.position,firstTr.rotation) as GameObject).transform;
            }
            else
            {
                newpart = (Instantiate(bodyprefab, BodyParts[BodyParts.Count - 1].position, BodyParts[BodyParts.Count -1].rotation) as GameObject).transform;

            }
            newpart.SetParent(transform);
            BodyParts.Add(newpart);
        }

        
    }

    public void ResetCube()
    {
        BodyParts.Clear();
        for (int i=0; i<beginSize;i++)
        {
            Destroy(this.gameObject.transform.GetChild(i).gameObject);

        }
    }
       
}
