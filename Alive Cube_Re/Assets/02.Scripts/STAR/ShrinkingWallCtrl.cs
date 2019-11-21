using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShrinkingWallCtrl : MonoBehaviour
{
    private GameObject frontWall;
    private GameObject rightWall;
    private GameObject leftWall;
    public CameraShake cameraShake;

    //지진 효과
    private void Start()
    {
        frontWall = transform.GetChild(0).gameObject;
        rightWall = transform.GetChild(2).gameObject;
        leftWall = transform.GetChild(3).gameObject;
        //cameraShake = GameObject.FindWithTag("Player").transform.parent.GetComponent<CameraShake>();
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            cameraShake.shake = 1;
            Shirinking();
        }
    }

    void Shirinking()
    {
        MoveFrontWall();
        MoveRightWall();
        MoveLeftWall();
    }

    void MoveFrontWall()
    {
        Hashtable ht = new Hashtable();
        ht.Add("z", -0.5f);
        ht.Add("time", 2.0f);
        ht.Add("easetype", iTween.EaseType.easeOutQuart);

        iTween.MoveBy(frontWall, ht);
    }

    void MoveRightWall()
    {
        Hashtable ht = new Hashtable();
        ht.Add("x", 0.5f);
        ht.Add("time", 2.0f);
        ht.Add("easetype", iTween.EaseType.easeOutQuart);

        iTween.MoveBy(rightWall, ht);
    }

    void MoveLeftWall()
    {
        Hashtable ht = new Hashtable();
        ht.Add("x", -0.5f);
        ht.Add("time", 2.0f);
        ht.Add("easetype", iTween.EaseType.easeOutQuart);

        iTween.MoveBy(leftWall, ht);
    }
}
