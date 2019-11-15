using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class TestDotwwen : MonoBehaviour
{
    // Start is called before the first frame update

    public Transform target;
    public float homingDelay = 1f;
    public float maxSpeed = 50f;

    void Start()
    {
        if (target == null)
        {
            target = this.transform;
        }
        //m_System = GetComponent<ParticleSystem>();
        //m_Particles = new ParticleSystem.Particle[m_System.maxParticles];// Before Unity 5.5
        //m_Particles = new ParticleSystem.Particle[m_System.main.maxParticles];// Since Unity 5.5
    }

    void LateUpdate()
    {
        //int numParticlesAlive = m_System.GetParticles(m_Particles);
        //float ted = Vector3.Distance(target.position, this.transform.position);// sqrMagnitude is faster than Distance/Magnitude because it skips square root.
        float ted = (target.position - this.transform.position).sqrMagnitude + 0.001f;
        Vector3 diff = target.position - this.transform.position;
        float diffsqrm = diff.sqrMagnitude;
        float face = Vector3.Dot(this.gameObject.GetComponent<Rigidbody>().velocity.normalized, diff.normalized);
        float f = Mathf.Abs((ted - diffsqrm) / ted) * ted * (face+1.001f);

        float t = 0;
        t += Time.deltaTime / (homingDelay + 0.0001f) * 100f;
        this.gameObject.GetComponent<Rigidbody>().velocity = Vector3.ClampMagnitude(Vector3.Slerp(this.gameObject.GetComponent<Rigidbody>().velocity, this.gameObject.GetComponent<Rigidbody>().velocity + diff * 1 * 0.01f * f, t), maxSpeed);
        //for (int i = 0; i < numParticlesAlive; i++)
        //{
        //}
        //m_System.SetParticles(m_Particles, numParticlesAlive);
    }















    //void Start()
    //{
    //    //Sequence mySequence = DOTween.Sequence();
    //    //mySequence.Append(transform.DOMove(Vector3.forward * 4, 5))
    //    //          .Join(transform.DORotate(new Vector3(90, 90, 90), 5.0f));
    //    //transform.DOMove(Vector3.forward * 4, 5);

    //    speed = 5;
    //    width = 4;
    //    height = 7;

    //    //DOPunchPosition(Vector3 punch, float duration, int vibrato, float elasticity, bool snapping)
    //}


    //void Update()
    //{
    //    timeCounter += Time.deltaTime * speed;
    //    float x = Mathf.Cos(timeCounter) * width;
    //    float y = Mathf.Sin(timeCounter) * height;
    //    float z = 5;
    //    transform.position = new Vector3(x, y, z) + Vector3.forward;
    //}
}
