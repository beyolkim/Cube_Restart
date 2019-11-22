using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletHit : MonoBehaviour
{
    private void OnCollisionEnter(Collision coll)
    {
        if (coll.transform.CompareTag("ENEMY"))
        {
            RedMonCtrl redMonCtrl = coll.transform.GetComponentInParent<RedMonCtrl>();
            
            Debug.Log("BulletCtrl가 레드몬스크립트를 가져왔나요 : " + redMonCtrl);

            if (redMonCtrl.R_MonHP > 0)
            {
                Debug.Log("StateTakeDamage가 실행되었습니다");

                redMonCtrl.animator.SetTrigger("Take Damage");
                redMonCtrl.R_MonHP--;
            }

            if (redMonCtrl.R_MonHP <= 0)
            {
                Debug.Log("레드몬스터가 사망했습니다 : " + name);

                redMonCtrl.animator.SetTrigger("Die");
            }
        }
    }
}
