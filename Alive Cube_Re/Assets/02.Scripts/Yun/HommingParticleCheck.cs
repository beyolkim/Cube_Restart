using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HommingParticleCheck : MonoBehaviour
{
    public GameObject Gameplayer;
    private void OnParticleCollision(GameObject coll)
    {
        if (coll.CompareTag("HOMMING"))
        {
            PlayerController.playerHp--;
            StartCoroutine(Gameplayer.GetComponent<PlayerController>().HitEffect(this.gameObject));

        }
    }

}
