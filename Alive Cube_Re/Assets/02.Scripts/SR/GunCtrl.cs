using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class GunCtrl : MonoBehaviour
{
    public static GunCtrl instance = null;

    public SteamVR_Input_Sources hand = SteamVR_Input_Sources.Any;
    public SteamVR_Action_Boolean trigger = SteamVR_Actions.default_InteractUI;
    public SteamVR_Action_Vibration haptic = SteamVR_Actions.default_Haptic;

    public GameObject fireParticle;
    public Transform firePos;
    public Light fireLight;
    public GameObject bullet;
    public Material lineMat;
    public Transform rayPos;

    public float fireRate = 0.25f;
    private bool fireAllowed = true;
    private bool gunReady = false;
    private Ray ray;
    private RaycastHit hit;
    public float range = 30f;
    private LineRenderer line;

    public AudioClip[] audioClip;
    private AudioSource _audio;

    void Start()
    {
        instance = this;
        fireLight.GetComponent<Light>().enabled = false;
        _audio = GetComponent<AudioSource>();
    }


    void Update()
    {
        if (!gunReady && trigger.GetStateDown(hand)) //Gun을 생성하면서 Line을 생성
        {
            gunReady = true;
            Invoke("CreateLine", 2f);
        }
        if (gunReady) //Gun의 Line이 생성되었다면 Ray를 발사
        {
            GunRay();
        }
        //if(gunReady && trigger.GetStateDown(hand))
        //{
        //    Fire();
        //}
    }
    public void Fire()
    {
        if (fireAllowed && fireParticle != null)
        {
            fireAllowed = false;
            _audio.PlayOneShot(audioClip[Random.Range(0, audioClip.Length)]);            
            haptic.Execute(0f, 0.1f, 80, 1f, hand);
            GameObject _fireParticle = Instantiate(fireParticle, firePos.position, firePos.rotation);
            StartCoroutine(FireLight());
            if (bullet != null)
            {
                GameObject _bullet = Instantiate(bullet, firePos.position, firePos.rotation);
            }

            Destroy(_fireParticle, 1.5f);
        }
    }
    IEnumerator FireLight()
    {
        fireLight.GetComponent<Light>().enabled = true;
        yield return new WaitForSeconds(fireRate);
        fireLight.GetComponent<Light>().enabled = false;
        fireAllowed = true;
    }
    void CreateLine()
    {
        line = this.gameObject.AddComponent<LineRenderer>();
        line.useWorldSpace = false;
        line.receiveShadows = false;
        line.positionCount = 2;
        line.SetPosition(0, rayPos.localPosition);
        line.SetPosition(1, new Vector3(0, 0, range));
        line.startWidth = 0.005f;
        line.endWidth = 0.005f;
        line.material = lineMat;
    }
    void GunRay()
    {
        ray = new Ray(transform.position, transform.forward);
        if (Physics.Raycast(ray, out hit, range))
        {
            if (hit.collider.CompareTag("CUBE"))
            {
                Debug.Log("큐브다!!");
            }
        }
    }
}
