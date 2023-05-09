using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class unapprochableGimic : MonoBehaviour
{
    TowerManager tm;


    private void Start() 
    {
        tm.socialable = false;
    }

    private void OnDestroy() 
    {
        tm.socialable = true;
    }
}
