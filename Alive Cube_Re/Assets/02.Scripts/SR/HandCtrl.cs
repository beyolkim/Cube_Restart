using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class HandCtrl : MonoBehaviour
{
    public SteamVR_Input_Sources hand = SteamVR_Input_Sources.Any;
    public SteamVR_Input_Sources rightHand = SteamVR_Input_Sources.RightHand;
    public SteamVR_Input_Sources leftHand = SteamVR_Input_Sources.LeftHand;
    
    public SteamVR_Action_Boolean trigger = SteamVR_Actions.default_InteractUI;
    public SteamVR_Action_Vibration haptic = SteamVR_Actions.default_Haptic;

    public GameObject[] sword;
    public GameObject sword_blade;
    public Transform sword_particle_pos;
    public GameObject sword_particle;

    private float[] swordValue = new float[10];
    private float[] gunValue = new float[4]; //gun 파트의 각각의 gunValue 선언
    private Material[] swordMat = new Material[10];
    private Material[] gunMat = new Material[4]; //public이 아닌 private로 선언할 때는 new배열로 선언 필요

    private bool swordOn = false;

    public GameObject[] guns;
    public GameObject gunPart;
    public Transform gun_particle_pos;
    public GameObject gun_particle; 

    private bool gunOn = false;
    private bool gunSpawn = false;
    
    private Animator anim;   
    
    private int swordGrab;
    private int gunGrab;
    private int gunShot;

    private AudioSource _audio;
    public AudioClip beamUp;
    public AudioClip gunSpawn_audio;
    public AudioClip swordSpawn;

    void Start()
    {        
        for(int i =0; i < 10; i ++)
        {
            swordValue[i] = 0.5f;
            swordMat[i] = sword[i].GetComponent<MeshRenderer>().material;
        }       


        for (int i = 0; i < 4; i++)
        {
            gunValue[i] = 0.5f; //gun의 5개 파트의 gunValue값을 각각 초기화
            gunMat[i] = guns[i].GetComponent<MeshRenderer>().material; //gun의 5개 파트의 gunMat값을 각각 초기화
        }
        //gunMat[0] = gun[0].GetComponent<MeshRenderer>().material;
              

        anim = GetComponent<Animator>();                
        swordGrab = Animator.StringToHash("sword");
        gunGrab = Animator.StringToHash("gun");
        gunShot = Animator.StringToHash("gunShot");

        _audio = GetComponent<AudioSource>();

        Debug.Log("씬 이름 : " + this.gameObject.scene.name + "!!!");
    }

    
    void Update()
    {
        if (this.gameObject.scene.name == "Stage1") 
        {
            if (!swordOn && trigger.GetStateDown(rightHand)) //Scene이 Sword일 때 검 생성
            {
                swordOn = true;
                anim.SetBool(swordGrab, true);                
                StartCoroutine(SwordSpawn());
            }
        }
        else if(this.gameObject.scene.name == "Go_Gun")
        {
            if(!gunSpawn && !gunOn && trigger.GetStateDown(rightHand)) //Scene이 Gun일 때 총 생성
            {
                gunSpawn = true; //총이 생성되는 중임을 true로 알림
                anim.SetBool(gunGrab, true);
                StartCoroutine(GunSpawn());
            }
            else if(gunOn && trigger.GetStateDown(rightHand)) //총이 생성되었으면 trigger을 누를때마다 격발
            {                
                anim.SetBool(gunShot, true);

            }
            else if (gunOn && trigger.GetStateUp(rightHand))
            {
                anim.SetBool(gunShot, false);                
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
            for(int i = 0; i < 10; i++)
            {
                swordValue[i] -= 0.017f;
                swordMat[i].SetFloat("_Dissolve", swordValue[i]);                
            }
            yield return new WaitForSeconds(0.008f);
        }
        sword[9].GetComponent<MeshCollider>().enabled = true;
        sword_blade.gameObject.SetActive(true);
        R_SwordCtrl.instance.right_swordOn = true;
    }

    IEnumerator GunSpawn()
    {
        yield return new WaitForSeconds(0.4f);
        _audio.PlayOneShot(beamUp);
        Quaternion rot = Quaternion.FromToRotation(Vector3.forward, gun_particle_pos.forward);
        GameObject _gun_particle = Instantiate(gun_particle, gun_particle_pos.position, rot);

        Destroy(_gun_particle, 1.5f);

        yield return new WaitForSeconds(0.4f);
        _audio.PlayOneShot(gunSpawn_audio);        
        while(gunValue[0] >= -1f)
        {
            for (int i = 0; i < 4; i++)
            {
                gunValue[i] -= 0.017f;            
                gunMat[i].SetFloat("_Dissolve", gunValue[i]);
            }
            
            yield return new WaitForSeconds(0.01f);
        }
        gunPart.gameObject.SetActive(true);

        gunOn = true; //총이 생성완료되었음을 true로 알림
    }

    //gunShot 애니메이션이 발동할 때마다 자동으로 GunFire Event 발생하여 실행
    void GunFire() 
    {
        GunCtrl.instance.Fire();
    }
 
}
