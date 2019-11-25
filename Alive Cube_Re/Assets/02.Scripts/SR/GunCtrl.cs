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
    public float fireRate = 0.25f;
    private bool fireAllowed = true;
    public bool gunReady = false;
    private bool flag;
    public AudioClip[] audioClip;
    private AudioSource _audio;

    private Ray ray;
    private RaycastHit hit;

    public Material lineMat;
    public Transform rayPos;
    public float range = 30f;
    private LineRenderer line;

    public bool stage2UICheck = false;

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
            _audio.Play();
            //Invoke("CreateLine", 2.0f);
        }
        //Stage2의 UI를 확인해야 Ray가 나가도록
        else if (stage2UICheck && gunReady && RedMonCtrl.monReady) //Gun의 Line이 생성되었다면 Ray를 발사
        {
            ray = new Ray(firePos.position, transform.forward);

            if (Physics.Raycast(ray, out hit, 30.0f) && hit.collider.CompareTag("ENEMY"))
            {
                var hitRedMonCtrl = hit.collider.transform.parent.GetComponent<RedMonCtrl>();
                Transform monPos = hit.collider.transform.parent;

                //Debug.Log("RayCast에 맞은 레드몬은 : " + hit.collider.transform.parent.name);

                if (hitRedMonCtrl.state == RedMonCtrl.State.IDLE)
                {
                    //Debug.Log(hitRedMonCtrl.state);

                    //Idle상태일 때만 회피 가능, 공격 또는 회피 도중 피격은 피할 수 없음
                    if (monPos.position.x >= 0 && monPos.position.x <= 4.5)
                    {
                        hitRedMonCtrl.StateStrafeLeft();
                        //Debug.Log("레드몬이 왼쪽으로 피했습니다");
                    }

                    else if (monPos.position.x >= -4.5 && monPos.position.x < 0)
                    {
                        hitRedMonCtrl.StateStrafeRight();
                        //Debug.Log("레드몬이 오른쪽으로 피했습니다");
                    }
                }
            }
        }
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
}
