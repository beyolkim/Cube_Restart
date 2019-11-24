using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCtrl : MonoBehaviour
{
    public GameObject sparkParticle;

    void Start()
    {
        GetComponent<Rigidbody>().AddForce(transform.forward * 3000f);
    }
    private void Update()
    {
        Destroy(this.gameObject, 5f);

    }

    private void OnCollisionEnter(Collision coll)
    {
        ContactPoint contact = coll.contacts[0];
        Vector3 _normal = contact.normal; //coll(부딫힌 Cube)의 법선벡터            

        GameObject _sparkParticle = Instantiate(sparkParticle, contact.point, Quaternion.LookRotation(_normal));
        Destroy(this.gameObject);
        Destroy(_sparkParticle, 1.5f);

        if (coll.transform.CompareTag("ENEMY"))
        {
            RedMonCtrl redMonCtrl = coll.transform.GetComponentInParent<RedMonCtrl>();

            //Debug.Log("BulletCtrl가 레드몬스크립트를 가져왔나요 : " + redMonCtrl);

            if (redMonCtrl.R_MonHP > 0)
            {
                //Debug.Log("StateTakeDamage가 실행되었습니다");

                redMonCtrl.StateTakeDamage();
            }

            if (redMonCtrl.R_MonHP <= 0)
            {
                //Debug.Log("레드몬스터가 사망했습니다 : " + name);

                redMonCtrl.animator.SetTrigger("Die");

            }
        }
        if(coll.collider.CompareTag("WEAKPOINT") && coll.gameObject.GetComponent<WallCubeController>().setCheck == true)
        {
            AttackController.cubeHp --;
            Material [] hitPointMat = coll.gameObject.GetComponent<MeshRenderer>().materials;
            hitPointMat[1].SetVector("_VectorAlpha", new Vector2(0, 0));
            coll.transform.GetChild(0).gameObject.GetComponent<ParticleSystem>().Play();
            coll.gameObject.GetComponent<WallCubeController>().setCheck = false;
            Debug.Log("CUBEHP" + AttackController.cubeHp);
        }
    }

    //if (coll.transform.CompareTag("REDMON"))
    //{
    //    ContactPoint contact = coll.contacts[0];
    //    Vector3 _normal = contact.normal; //coll(부딫힌 Cube)의 법선벡터            

    //    GameObject _sparkParticle = Instantiate(sparkParticle, contact.point, Quaternion.LookRotation(_normal));
    //    Destroy(this.gameObject);
    //    Destroy(_sparkParticle, 1.5f);
    //}
}
