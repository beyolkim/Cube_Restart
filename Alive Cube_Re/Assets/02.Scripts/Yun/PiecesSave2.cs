using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PiecesSave2 : MonoBehaviour
{
    public static PiecesSave2 instance = null;

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
