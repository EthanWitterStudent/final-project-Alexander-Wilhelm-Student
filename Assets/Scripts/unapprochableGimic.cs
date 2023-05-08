using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class unapprochableGimic : MonoBehaviour
{
    public bool socialable;

    private void Start() 
    {
        socialable = false;
    }

    private void OnDestroy() 
    {
        socialable = true;
    }
}
