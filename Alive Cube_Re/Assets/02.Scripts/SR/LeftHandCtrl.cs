using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class LeftHandCtrl : MonoBehaviour
{
    public SteamVR_Input_Sources lefthand = SteamVR_Input_Sources.LeftHand;
    public SteamVR_Action_Boolean trigger = SteamVR_Actions.default_InteractUI;

    public GameObject sword;
    public Transform sword_particle_pos;
    public GameObject sword_particle;
    private float swordValue = 0.5f;
    private Material swordMat;
    private bool swordOn = false;
    private int swordGrab;

    private Animator anim;
    private int cubeHit;
    private int shieldGrab;

    void Start()
    {
        anim = GetComponent<Animator>();
        // cubeHit = Animator.StringToHash("cubeHit");
        shieldGrab = Animator.StringToHash("shield");
        swordGrab = Animator.StringToHash("sword");

        swordMat = sword.GetComponent<MeshRenderer>().material;
    }

    void Update()
    {
        if (this.gameObject.scene.name == "Go_Sword")
        {
            if (!swordOn && trigger.GetStateDown(lefthand)) //Scene이 Sword일 때 검 생성
            {
                swordOn = true;
                anim.SetBool(swordGrab, true);
                sword.GetComponent<MeshCollider>().enabled = true;
                StartCoroutine(SwordSpawn());
            }
        }

        else if (this.gameObject.scene.name == "Go_Gun")
        {
            if (trigger.GetStateDown(lefthand))
            {
                anim.SetBool(shieldGrab, true);
            }
        }
    }
    
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
