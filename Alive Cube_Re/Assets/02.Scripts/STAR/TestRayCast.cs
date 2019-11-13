using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TestRayCast : MonoBehaviour
{
    private Ray ray;
    private RaycastHit hit;

    public Ray Ray
    {
        get
        {
            return ray;
        }
        set
        {
            ray = value;
        }
    }
    public RaycastHit Hit
    {
        get
        {
            return hit;
        }
        set
        {
            hit = value;
        }
    }
    void Update()
    {
        ray = new Ray(transform.position, transform.forward);
        Debug.DrawRay(ray.origin, ray.direction * 10.0f, Color.green);
    }
}
