using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class unapprochableGimic : MonoBehaviour
{
    public bool socialable;

    private void OnEnable() 
    {
        socialable = false;
    }

    private void OnDestroy() 
    {
        socialable = true;
    }
}
