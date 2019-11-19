using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PieceCtrl : MonoBehaviour
{
    private GameObject[] P_Mons = new GameObject[3];      //퍼플몬스터 
    private GameObject[] P_pieces = new GameObject[3];   //퍼플조각
    private GameObject[] R_Mons = new GameObject[7];      //레드몬스터 
    private GameObject[] R_pieces = new GameObject[7];   //레드조각

    public GameObject Del_PurPlePieces;
    public GameObject Del_RedPieces;

    void Start()
    {
        P_Mons = GameObject.FindGameObjectsWithTag("PURPLEMON");
        P_pieces = GameObject.FindGameObjectsWithTag("PURPLEPIECE");
        R_Mons = GameObject.FindGameObjectsWithTag("REDMON");
        R_pieces = GameObject.FindGameObjectsWithTag("REDPIECE");
        
        //PurplePiece 떠오름
        for (int i = 0; i < P_pieces.Length; i++)
        {
            P_pieces[i].transform.DOLocalMoveY(P_Mons[i].transform.position.y, 2.0f);
            P_Mons[i].SetActive(false);
        }

        //RedPiece 떠오름
        for (int i = 0; i < R_pieces.Length; i++)
        {
            R_pieces[i].transform.DOLocalMoveY(R_Mons[i].transform.position.y, 2.0f);
            R_Mons[i].SetActive(false);

        }
        Invoke("P_SpawnPos", 2.5f);
        Invoke("R_SpawnPos", 2.5f);
        Invoke("Twinkling", 5.0f);

    }

    void P_SpawnPos()
    {
        //PurplePiece가 최종 spawn위치로 향함
        for (int i = 0; i < P_pieces.Length; i++)
        {
            Vector3 P_SpawnPos = P_Mons[i].transform.position + Vector3.down * 1.2f;
            P_pieces[i].transform.DOMove(P_SpawnPos, 2.0f);
        }
    }

    void R_SpawnPos()
    {
        //RedPiece가 최종 spawn위치로 향함
        for (int i = 0; i < R_pieces.Length; i++)
        {
            Vector3 R_SpawnPos = R_Mons[i].transform.position + Vector3.down * 1.2f;
            R_pieces[i].transform.DOMove(R_SpawnPos, 2.0f);
        }
    }

    void Twinkling()
    {
        //조각들이 제 위치로 가면 반짝임
        for (int i = 0; i < P_pieces.Length; i++)
        {
            P_pieces[i].gameObject.transform.GetChild(0).transform.GetChild(0).gameObject.SetActive(true);
            P_pieces[i].gameObject.transform.GetChild(0).transform.GetChild(3).gameObject.SetActive(true);
            P_pieces[i].gameObject.transform.GetChild(0).transform.GetChild(4).gameObject.SetActive(true);
            P_pieces[i].gameObject.transform.GetChild(0).transform.GetChild(5).gameObject.SetActive(true);
        }

        for (int i = 0; i < R_pieces.Length; i++)
        {
            R_pieces[i].gameObject.transform.GetChild(0).transform.GetChild(0).gameObject.SetActive(true);
            R_pieces[i].gameObject.transform.GetChild(0).transform.GetChild(3).gameObject.SetActive(true);
            R_pieces[i].gameObject.transform.GetChild(0).transform.GetChild(4).gameObject.SetActive(true);
            R_pieces[i].gameObject.transform.GetChild(0).transform.GetChild(5).gameObject.SetActive(true);
        }

        Invoke("RevealMons", 10.0f);
    }

    void RevealMons()
    {
        for (int i = 0; i < P_Mons.Length; i++)
        {
            P_Mons[i].SetActive(true);
        }

        for (int i = 0; i < R_Mons.Length; i++)
        {
            R_Mons[i].SetActive(true);
        }

        Destroy(Del_PurPlePieces, 0.5f);
        Destroy(Del_RedPieces, 0.5f);
    }
}
