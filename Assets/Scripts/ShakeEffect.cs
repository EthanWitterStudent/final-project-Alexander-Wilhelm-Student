using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeEffect : MonoBehaviour
{  
    [SerializeField] float shakeAmount;
    [SerializeField] float shakeDecay;
    void Start()
    {
        FindObjectOfType<CameraShake>().setShake(shakeAmount, shakeDecay);
        Destroy(this);
    }
}
