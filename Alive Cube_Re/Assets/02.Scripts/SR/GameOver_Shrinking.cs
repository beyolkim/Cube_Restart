using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver_Shrinking : MonoBehaviour
{
    public static GameOver_Shrinking instance = null;

    private GameObject frontWall;
    private GameObject rightWall;
    private GameObject leftWall;
    public CameraShake cameraShake;

    //지진 효과
    private void Start()
    {
        instance = this;
        frontWall = transform.GetChild(0).gameObject;
        rightWall = transform.GetChild(2).gameObject;
        leftWall = transform.GetChild(3).gameObject;
        //cameraShake = GameObject.FindWithTag("Player").transform.parent.GetComponent<CameraShake>();
    }

    //void Update()
    //{
    //    if (Input.GetMouseButtonDown(0))
    //    {
    //        cameraShake.shake = 4;
    //        Shirinking();
    //    }
    //}

    void Shirinking()
    {
        MoveFrontWall();
        MoveRightWall();
        MoveLeftWall();
    }

    void MoveFrontWall()
    {
        Hashtable ht = new Hashtable();
        ht.Add("z", -8.5f);
        ht.Add("time", 9.3f);
        ht.Add("easetype", iTween.EaseType.easeOutQuart);

        iTween.MoveBy(frontWall, ht);
    }

    void MoveRightWall()
    {
        Hashtable ht = new Hashtable();
        ht.Add("x", 4.8f);
        ht.Add("time", 9.3f);
        ht.Add("easetype", iTween.EaseType.easeOutQuart);

        iTween.MoveBy(rightWall, ht);
    }

    void MoveLeftWall()
    {
        Hashtable ht = new Hashtable();
        ht.Add("x", -4.8f);
        ht.Add("time", 9.3f);
        ht.Add("easetype", iTween.EaseType.easeOutQuart);

        iTween.MoveBy(leftWall, ht);
    }
    public void GameOver()
    {
        cameraShake.shake = 4;
        Shirinking();
    }
}
