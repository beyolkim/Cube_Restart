using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class LeftHandCtrl : MonoBehaviour
{
    public static LeftHandCtrl instance = null;
    public SteamVR_Input_Sources lefthand = SteamVR_Input_Sources.LeftHand;
    public SteamVR_Action_Boolean trigger = SteamVR_Actions.default_InteractUI;

    public GameObject sword;
    public Transform sword_particle_pos;
    public GameObject sword_particle;
    private float swordValue = 0.5f;
    private Material swordMat;
    private bool swordOn = false;
    private int swordGrab;
    public GameObject[] shield;
    private float[] shieldValue = new float[3];
    private float[] _shieldValue = new float[3];
    private Material[] shieldMat = new Material[6];
    public Transform shield_particle_pos;
    public GameObject shield_particle;

    private Animator anim;
    private int cubeHit;
    private int shieldGrab;
    private bool shieldOn = false;

    public AudioClip shieldSpawn;
    public AudioClip swordSpawn;
    public AudioClip beamUp;
    private AudioSource _audio;

    void Start()
    {
        instance = this;
        anim = GetComponent<Animator>();
        // cubeHit = Animator.StringToHash("cubeHit");
        shieldGrab = Animator.StringToHash("shield");
        swordGrab = Animator.StringToHash("sword");

        swordMat = sword.GetComponent<MeshRenderer>().material;
        _audio = GetComponent<AudioSource>();
        

        for(int i = 0; i<3; i++)
        {
            shieldValue[i] = 0.5f;
            _shieldValue[i] = 3f;
            shieldMat[i] = shield[i].GetComponent<MeshRenderer>().materials[0];
            shieldMat[i+3] = shield[i].GetComponent<MeshRenderer>().materials[1];

        }

    }

    void Update()
    {
        if (this.gameObject.scene.name == "Yun_Test")
        {
            if (!swordOn && trigger.GetStateDown(lefthand)) //Scene이 Sword일 때 검 생성
            {
                swordOn = true;
                anim.SetBool(swordGrab, true);                
                StartCoroutine(SwordSpawn());
            }
        }

        else if (this.gameObject.scene.name == "Go_Gun")
        {
            if (!shieldOn && trigger.GetStateDown(lefthand))
            {
                shieldOn = true;
                anim.SetBool(shieldGrab, true);
                StartCoroutine(ShieldSpawn());
            }            
        }
    }
    
    IEnumerator SwordSpawn()
    {
        yield return new WaitForSeconds(0.4f); //손이 돌아가고 Spawn 파티클 나오도록
        _audio.PlayOneShot(beamUp);
        Quaternion rot = Quaternion.FromToRotation(Vector3.forward, sword_particle_pos.forward);
        GameObject _sword_particle = Instantiate(sword_particle, sword_particle_pos.position, rot);
        
        Destroy(_sword_particle, 1.5f);

        yield return new WaitForSeconds(0.4f); //Spawn 파티클 나오고 칼 생성되도록
        _audio.PlayOneShot(swordSpawn);
        while(swordValue >= -0.9f)
        {
            swordValue -= 0.015f;
            swordMat.SetFloat("_Dissolve", swordValue);
            yield return new WaitForSeconds(0.015f);
        }
        sword.GetComponent<MeshCollider>().enabled = true;
    }

    IEnumerator ShieldSpawn()
    {
        yield return new WaitForSeconds(0.4f);
        _audio.PlayOneShot(beamUp);
        Quaternion rot = Quaternion.FromToRotation(Vector3.forward, shield_particle_pos.forward);
        GameObject _shield_particle = Instantiate(shield_particle, shield_particle_pos.position, rot);

        Destroy(_shield_particle, 1.5f);

        yield return new WaitForSeconds(0.4f);
        _audio.PlayOneShot(shieldSpawn);
        while (shieldValue[0] >= -0.9f)
        {
            for(int i =0; i < 3; i++)
            {
                shieldValue[i] -= 0.015f;
                _shieldValue[i] -= 0.05f;                
                shieldMat[i].SetFloat("_Dissolve", shieldValue[i]);
                shieldMat[i+3].SetFloat("_Dissolves", _shieldValue[i]);
            }            
            yield return new WaitForSeconds(0.02f);
        }

        for(int i = 0; i < 3; i++)
        {
            shield[i].GetComponent<MeshCollider>().enabled = true;
        }
        
        ShieldCtrl.instance.shieldOn = true;
    }

    // private void OnCollisionEnter(Collision coll)
    // {
    //     if (coll.transform.CompareTag("CUBE"))
    //     {
    //         Debug.Log("왼손!!");
    //         anim.SetTrigger(cubeHit);
    //     }
    // }

}
