using UnityEngine;
using System.Collections;
using Valve.VR;

[RequireComponent (typeof(Rigidbody))]
public class SwordCutter : MonoBehaviour {

    public static SwordCutter instance = null;

    public SteamVR_Input_Sources hand = SteamVR_Input_Sources.Any;
    public SteamVR_Input_Sources righHhand = SteamVR_Input_Sources.RightHand;
    public SteamVR_Input_Sources leftHhand = SteamVR_Input_Sources.LeftHand;

    public SteamVR_Action_Vibration haptic = SteamVR_Actions.default_Haptic;
	public Material capMaterial;
        
    private Vector3 curPos;
    private Vector3 prePos;
    private Vector3 dist;
    private float power;
    
    public Transform hitPos;    
    public GameObject hit;

    private AudioSource _audio;
    public AudioClip[] swordHit;

    private int check_fallStone;

    public static int stage1_Count = 0;

    //빈오브젝트 생성
    public GameObject redGameObject;
    public GameObject purpleGameObject;

    private void Start()
    {
        instance = this;
        //hit = Resources.Load<GameObject>("SwordSlash");
        _audio = GetComponent<AudioSource>();
 
    }

    private void Update()
    {
        SwordPower();
        
    }

    private void OnCollisionEnter(Collision coll)
    {
        GameObject victim = coll.collider.gameObject;

        if(coll.gameObject.CompareTag("UI") && power >= 0.8f)
        {
            _audio.PlayOneShot(swordHit[Random.Range(0, swordHit.Length)]);
            GameObject _hit = Instantiate(hit, hitPos.position, hitPos.rotation);
            haptic.Execute(0f, 0.15f, 80, 2f, hand);
            Destroy(_hit, 1.5f);

            GameObject[] pieces = BLINDED_AM_ME.MeshCut.Cut(victim, transform.position, transform.right, capMaterial);
            coll.gameObject.SetActive(false);
            StartCoroutine(AttackController.instance.AttackTurn());
        }
        
        if (coll.gameObject.transform.CompareTag("CUBE") && power >= 0.5f)
        {
            check_fallStone += 1;
            ContactPoint contact = coll.contacts[0];
            Vector3 _normal = contact.normal;

            _audio.PlayOneShot(swordHit[Random.Range(0, swordHit.Length)]);
            GameObject _hit = Instantiate(hit, hitPos.position, hitPos.rotation);
            haptic.Execute(0f, 0.15f, 80, 2f, hand);
            Destroy(_hit, 1.5f);

            GameObject[] pieces = BLINDED_AM_ME.MeshCut.Cut(victim, transform.position, transform.right, capMaterial);
            //Debug.Log(power);

            //큐브 되돌아가기
            victim.GetComponentInParent<AttackCube>().check_attack = true;
            //AttackController.scoreCount++;

            if (check_fallStone > 2)
            { 
                if (stage1_Count <7)
                {
                    GameObject fragment = Instantiate(Resources.Load("RedPiece") as GameObject);
                    //fragment.transform.parent = redGameObject.transform;
                    fragment.transform.position = victim.transform.position;
                    fragment.GetComponent<Rigidbody>().useGravity = true;
                    fragment.GetComponent<BoxCollider>().enabled = true;
                    check_fallStone = 0;
                    stage1_Count += 1;
                    StartCoroutine(UnCheckGravity(fragment));
                }
                else
                {
                    GameObject fragment = Instantiate(Resources.Load("PurplePiece") as GameObject);
                    //fragment.transform.parent = purpleGameObject.transform;
                    fragment.transform.position = victim.transform.position;
                    fragment.GetComponent<Rigidbody>().useGravity = true;
                    fragment.GetComponent<BoxCollider>().enabled = true;
                    check_fallStone = 0;
                    stage1_Count += 1;
                    StartCoroutine(UnCheckGravity(fragment));
                }
               
            }

            //if(coll.gameObject == victim.GetComponentInParent<AttackCube>().BodyParts[2])
            //{
            //}

            if (!pieces[1].GetComponent<Rigidbody>())
            {
                pieces[1].AddComponent<Rigidbody>();
                pieces[1].AddComponent<BoxCollider>();

            }
            //Destroy(pieces[1], 4);            
        }
    }
    void SwordPower()
    {
        curPos = transform.position;
        dist = (curPos - prePos) / Time.deltaTime;
        prePos = curPos;

        power = dist.magnitude;
    }

    IEnumerator UnCheckGravity(GameObject _fragment)
    {
        yield return new WaitForSeconds(3.0f);
        _fragment.GetComponent<Rigidbody>().isKinematic = true;
    }

    //IEnumerator LaserOff()
    //{
    //    yield return new WaitForSeconds(0.5f);
    //    laserOff = true;
    //    Debug.Log(laserOff);
    //}

}
