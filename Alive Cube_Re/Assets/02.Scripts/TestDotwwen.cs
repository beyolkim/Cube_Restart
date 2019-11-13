using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class TestDotwwen : MonoBehaviour
{
    // Start is called before the first frame update
    float timeCounter = 0;

    float speed;
    float width;
    float height;

    void Start()
    {
        //Sequence mySequence = DOTween.Sequence();
        //mySequence.Append(transform.DOMove(Vector3.forward * 4, 5))
        //          .Join(transform.DORotate(new Vector3(90, 90, 90), 5.0f));
        //transform.DOMove(Vector3.forward * 4, 5);

        speed = 5;
        width = 4;
        height = 7;

        //DOPunchPosition(Vector3 punch, float duration, int vibrato, float elasticity, bool snapping)
    }
    

    void Update()
    {
        timeCounter += Time.deltaTime * speed;
        float x = Mathf.Cos(timeCounter) * width;
        float y = Mathf.Sin(timeCounter) * height;
        float z = 5;
        transform.position = new Vector3(x, y, z) + Vector3.forward;
    }
}
