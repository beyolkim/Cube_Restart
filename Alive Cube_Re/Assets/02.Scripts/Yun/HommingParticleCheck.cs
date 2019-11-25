using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HommingParticleCheck : MonoBehaviour
{
    public GameObject Gameplayer;
    public Slider hpSlider;
    private void OnParticleCollision(GameObject coll)
    {
        if (coll.CompareTag("HOMMING"))
        {
            PlayerController.playerHp--;
            hpSlider.value = PlayerController.playerHp;
            StartCoroutine(Gameplayer.GetComponent<PlayerController>().HitEffect(this.gameObject));

        }
    }

}
