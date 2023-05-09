using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class unapprochableGimic : MonoBehaviour
{
    TowerManager tm;


    private void Start() 
    {
        tm = FindObjectOfType<TowerManager>();
        tm.juanCheck = true;
    }

    private void OnDestroy() 
    {
        tm.juanCheck = false;
    }
}
