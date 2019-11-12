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

    public GameObject sword;
    public Transform sword_particle_pos;
    public GameObject sword_particle;

    private float swordValue = 0.5f;
    private float[] gunValue = new float[4]; //gun 파트의 각각의 gunValue 선언
    private Material swordMat;
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

    void Start()
    {        
        swordMat = sword.GetComponent<MeshRenderer>().material;


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

        Debug.Log("씬 이름 : " + sword.scene.name + "!!!");
    }

    
    void Update()
    {
        if (this.gameObject.scene.name == "Go_Sword") 
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

    //private void OnTriggerEnter(Collider coll)
    //{        
    //    if(coll.gameObject.CompareTag("CUBE"))
    //    {
    //        Debug.Log("CUBE!");
    //        anim.SetTrigger(cubeHit);
    //        GameObject target = coll.gameObject;            
    //        haptic.Execute(0.2f, 0.4f, 10f, 5f, hand);
    //        Destroy(target);
    //    }
    //}

    IEnumerator SwordSpawn()
    {
        yield return new WaitForSeconds(0.4f); //손이 돌아가고 Spawn 파티클 나오도록
        Quaternion rot = Quaternion.FromToRotation(Vector3.forward, sword_particle_pos.forward);
        GameObject _sword_particle = Instantiate(sword_particle, sword_particle_pos.position, rot);
        
        Destroy(_sword_particle, 1.5f);

        yield return new WaitForSeconds(0.4f); //Spawn 파티클 나오고 칼 생성되도록

        while(swordValue >= -0.9f)
        {
            swordValue -= 0.015f;
            swordMat.SetFloat("_Dissolve", swordValue);
            yield return new WaitForSeconds(0.015f);
        }
        sword.GetComponent<MeshCollider>().enabled = true;
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
        while(gunValue[0] >= -0.9f)
        {
            for (int i = 0; i < 4; i++)
            {
                gunValue[i] -= 0.015f;            
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
