using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class LeftHandCtrl : MonoBehaviour
{
    public static LeftHandCtrl instance = null;
    public SteamVR_Input_Sources lefthand = SteamVR_Input_Sources.LeftHand;
    public SteamVR_Action_Boolean trigger = SteamVR_Actions.default_InteractUI;
    public GameObject weaponUI;
    public GameObject _weaponUI;

    public GameObject[] sword;
    public GameObject sword_blade;
    public Transform sword_particle_pos;
    public GameObject sword_particle;
    private float[] swordValue = new float[10];
    private Material[] swordMat = new Material[10];
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
        _audio = GetComponent<AudioSource>();

        for (int i = 0; i < 10; i++)
        {
            swordValue[i] = 0.5f;
            swordMat[i] = sword[i].GetComponent<MeshRenderer>().material;
        }

        for (int i = 0; i<3; i++)
        {
            shieldValue[i] = 0.5f;
            _shieldValue[i] = 3f;
            shieldMat[i] = shield[i].GetComponent<MeshRenderer>().materials[0];
            shieldMat[i+3] = shield[i].GetComponent<MeshRenderer>().materials[1];

        }

    }

    void Update()
    {
        if (this.gameObject.scene.name == "Stage1")
        {
            if (!swordOn && trigger.GetStateDown(lefthand)) //Scene이 Sword일 때 검 생성
            {
                swordOn = true;
                anim.SetBool(swordGrab, true);                
                StartCoroutine(SwordSpawn());
                weaponUI.gameObject.SetActive(false);
                _weaponUI.gameObject.SetActive(true);
            }
        }

        else if (this.gameObject.scene.name == "Stage2" || this.gameObject.scene.name == "Stage2_Copy" || this.gameObject.scene.name == "Stage3")
        {
            if (!shieldOn && trigger.GetStateDown(lefthand))
            {
                shieldOn = true;
                anim.SetBool(shieldGrab, true);
                StartCoroutine(ShieldSpawn());
                weaponUI.gameObject.SetActive(false);
                _weaponUI.gameObject.SetActive(true);
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
        while (swordValue[0] >= -1f)
        {
            for (int i = 0; i < 10; i++)
            {
                swordValue[i] -= 0.017f;
                swordMat[i].SetFloat("_Dissolve", swordValue[i]);
            }
            yield return new WaitForSeconds(0.008f);
        }

        sword[9].GetComponent<MeshCollider>().enabled = true;
        sword_blade.gameObject.SetActive(true);
        L_SwordCtrl.instance.left_swordOn = true;
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
            shield[i].GetComponent<BoxCollider>().enabled = true;
        }
        
        ShieldCtrl.instance.shieldOn = true;
    }

    public IEnumerator ShieldDisapper()
    {
        _audio.PlayOneShot(shieldSpawn);
        while (shieldValue[0] <= 0.5f)
        {
            for (int i = 0; i < 3; i++)
            {
                shieldValue[i] += 0.015f;
                _shieldValue[i] += 0.05f;
                shieldMat[i].SetFloat("_Dissolve", shieldValue[i]);
                shieldMat[i + 3].SetFloat("_Dissolves", _shieldValue[i]);
            }
            yield return new WaitForSeconds(0.02f);
        }

        for (int i = 0; i < 3; i++)
        {
            shield[i].GetComponent<BoxCollider>().enabled = false;
        }
        ShieldCtrl.instance.shieldOn = false;
        anim.SetBool(shieldGrab, false);
    }
    public IEnumerator L_SwordDisapper()
    {
        _audio.PlayOneShot(swordSpawn);
        sword_blade.gameObject.SetActive(false);
        while (swordValue[0] <= 0.5f)
        {
            for (int i = 0; i < 10; i++)
            {
                swordValue[i] += 0.017f;
                swordMat[i].SetFloat("_Dissolve", swordValue[i]);
            }
            yield return new WaitForSeconds(0.008f);
        }
        anim.SetBool(swordGrab, false);
        L_SwordCtrl.instance.left_swordOn = false;
    }

}
