using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{

    float shakeAmount;
    [SerializeField] float shakeDecay;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (shakeAmount > 0) {
            shakeAmount -= shakeDecay;
            Camera.main.transform.position = new Vector3(Random.Range(-shakeAmount, shakeAmount), Random.Range(-shakeAmount, shakeAmount), -10);
        } else Camera.main.transform.position = new Vector3(0, 0, -10);
    }

    public void setShake(float shake, float decay) {
        if (decay >= shakeDecay || shakeAmount < Mathf.Epsilon) shakeDecay = decay; //dont remember why i checked shakeamount here but im sure its helpful yessir
        if (shake >= shakeAmount) shakeAmount = shake;
    }
}
