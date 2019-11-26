using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

//퍼플, 레드 조각들이 몬스터 생성 위치로 떠올라 반짝이고,
//그 위치에서 몬스터들이 생성되며 조각들은 사라짐
public class PieceCtrl : MonoBehaviour
{
    public static PieceCtrl instance = null;

    private GameObject[] P_Mons = new GameObject[3];      //퍼플몬스터 
    private GameObject[] P_pieces = new GameObject[3];   //퍼플조각
    private GameObject[] R_Mons = new GameObject[7];      //레드몬스터 
    private GameObject[] R_pieces = new GameObject[7];   //레드조각

    public GameObject Del_PurPlePieces;
    public GameObject Del_RedPieces;
    public GameObject[] nowR_Mons;
    private List<int> aliveR_Mons;

    void Start()
    {
        instance = this;

        P_Mons = GameObject.FindGameObjectsWithTag("PURPLE");
        P_pieces = GameObject.FindGameObjectsWithTag("PURPLEPIECE");
        R_Mons = GameObject.FindGameObjectsWithTag("RED");
        R_pieces = GameObject.FindGameObjectsWithTag("REDPIECE");

        for (int i = 0; i < P_pieces.Length; i++)
        {
            P_Mons[i].SetActive(false);
        }

        for (int i = 0; i < R_pieces.Length; i++)
        {
            R_Mons[i].SetActive(false);
        }

        Invoke("PieceRising", 5.0f);
        Invoke("SpawnPos", 5.0f);
        Invoke("Twinkling", 7.0f);
        Invoke("SpawnParticle", 8.0f);
        Invoke("RevealMons", 8.0f);
    }


    void PieceRising()
    {
        //PurplePiece 떠오름
        for (int i = 0; i < P_pieces.Length; i++)
        {
            P_pieces[i].transform.DOLocalMoveY(P_Mons[i].transform.position.y, 2.0f);
            //P_Mons[i].SetActive(false);
        }

        //RedPiece 떠오름
        for (int i = 0; i < R_pieces.Length; i++)
        {
            R_pieces[i].transform.DOLocalMoveY(R_Mons[i].transform.position.y, 2.0f);
            //R_Mons[i].SetActive(false);
        }
    }

    void SpawnPos()
    {
        //PurplePiece가 최종 spawn위치로 향함
        for (int i = 0; i < P_pieces.Length; i++)
        {
            Vector3 P_SpawnPos = P_Mons[i].transform.position + Vector3.down * 1.2f;
            P_pieces[i].transform.DOMove(P_SpawnPos, 2.0f);
        }

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

        Destroy(R_pieces[0].transform.parent.gameObject, 2.0f);
        Destroy(P_pieces[0].transform.parent.gameObject, 2.0f);
    }

    void SpawnParticle()
    {
        //몬스터 생성 파티클 (BeamUpRed)
        for (int i = 0; i < P_pieces.Length; i++)
        {
            P_pieces[i].gameObject.transform.GetChild(1).gameObject.SetActive(true);
        }

        for (int i = 0; i < R_pieces.Length; i++)
        {
            R_pieces[i].gameObject.transform.GetChild(1).gameObject.SetActive(true);
        }

        //조각들 꺼짐
        for (int i = 0; i < P_pieces.Length; i++)
        {
            P_pieces[i].gameObject.transform.GetChild(0).gameObject.SetActive(false);
        }

        for (int i = 0; i < R_pieces.Length; i++)
        {
            R_pieces[i].gameObject.transform.GetChild(0).gameObject.SetActive(false);
        }
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
    }
    public void GameOver_RedIdle()
    {
        nowR_Mons = GameObject.FindGameObjectsWithTag("RED");
        aliveR_Mons = new List<int>();
        int _aliveR_Mons = GameObject.FindGameObjectsWithTag("RED").Length;

        for (int i = 0; i < _aliveR_Mons; i++)
        {
                nowR_Mons[i].GetComponent<Animator>().SetTrigger("GameOver_Idle");
                GameObject homing = nowR_Mons[i].transform.GetChild(0).transform.GetChild(0).gameObject;
                homing.SetActive(false);
        }
    }
}