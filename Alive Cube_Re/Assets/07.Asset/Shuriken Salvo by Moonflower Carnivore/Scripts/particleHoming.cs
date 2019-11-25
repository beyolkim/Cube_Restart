//using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[ExecuteInEditMode]
public class particleHoming : MonoBehaviour {
	[Tooltip("Target object. If this parameter is undefined it will assume the attached object itself which creates self chasing particle effect.")]
	public Transform[] target;
	[Tooltip("How fast the particle is guided to the closest target.")]
	public float speed = 10f;
	[Tooltip("Cap the maximum speed to prevent particle from being flung too far from the missed target.")]
	public float maxSpeed = 50f;
	[Tooltip("How long in the projectile begins being guided towards the target. Higher delay and high particle start speed requires greater distance between attacker and target to avoid uncontrolled orbitting around the target.")]
	public float homingDelay = 1f;
	ParticleSystem m_System;
	ParticleSystem.Particle[] m_Particles;

    public GameObject shieldHit_Particle;
    public List<ParticleCollisionEvent> collisionEvents;

    void Start() {
		//if (target == null) {
		//	target = this.transform;
		//}
		m_System = GetComponent<ParticleSystem>();
		//m_Particles = new ParticleSystem.Particle[m_System.maxParticles];// Before Unity 5.5
		m_Particles = new ParticleSystem.Particle[m_System.main.maxParticles];// Since Unity 5.5
	}
	
	void LateUpdate() {
		int numParticlesAlive = m_System.GetParticles(m_Particles);
		//float ted = Vector3.Distance(target.position, this.transform.position);// sqrMagnitude is faster than Distance/Magnitude because it skips square root.
		float ted = (target[Random.Range(0, target.Length)].position - this.transform.position).sqrMagnitude + 0.001f;
		for (int i = 0; i < numParticlesAlive; i++) {
			Vector3 diff = target[Random.Range(0, target.Length)].position - m_Particles[i].position;
			float diffsqrm = diff.sqrMagnitude;
			float face = Vector3.Dot(m_Particles[i].velocity.normalized, diff.normalized);
			float f = Mathf.Abs((ted - diffsqrm)/ted) * ted * (face + 1.001f);
				
			float t=0;
			t += Time.deltaTime / (homingDelay + 0.0001f) * 100f;
			m_Particles[i].velocity = Vector3.ClampMagnitude(Vector3.Slerp(m_Particles[i].velocity, m_Particles[i].velocity + diff * speed * 0.01f * f, t), maxSpeed);
        }
		m_System.SetParticles(m_Particles, numParticlesAlive);
	}

    private void OnParticleCollision(GameObject other)
    {  
        //int collCount = m_System.GetSafeCollisionEventSize();
        collisionEvents = new List<ParticleCollisionEvent>();
        int eventCount = m_System.GetCollisionEvents(other, collisionEvents);

        for (int i = 0; i < eventCount; i++)
        {
            Vector3 collisionHitRot = collisionEvents[i].normal;
            Vector3 collisionHitPos = collisionEvents[i].intersection;

            Quaternion HitRot = Quaternion.LookRotation(Vector3.forward, collisionHitRot);
            GameObject _shieldHit_Particle = Instantiate(shieldHit_Particle, collisionHitPos, HitRot);
            Destroy(_shieldHit_Particle, 1.5f);

            if(other.gameObject.CompareTag("SHIELD"))
            {
                ShieldCtrl.instance.ShieldHit_Audio();
            }
        }
    }
}