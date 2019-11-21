using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PiecesSave : MonoBehaviour
{
    public static PiecesSave instance = null;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else if (instance != null)
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
    }

}
