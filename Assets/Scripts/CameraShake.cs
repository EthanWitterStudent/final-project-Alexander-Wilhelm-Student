using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{

    float shakeAmount;
    float shakeDecay;

    Transform tf;

    void Start() {
        tf = Camera.main.transform;
    }
    // Update is called once per frame
    void Update()
    {
        if (shakeAmount > 0) {
            shakeAmount -= shakeDecay * Time.deltaTime;
            tf.position = Random.insideUnitCircle * shakeAmount;
            tf.position = new Vector3(tf.position.x, tf.position.y, -10);
        } else tf.position = new Vector3(0, 0, -10);
    }

    public void setShake(float shake, float decay) {
        Debug.Log($"shake {shake} decay {decay}");
        if (shake >= shakeAmount || shakeAmount < Mathf.Epsilon) shakeDecay = decay; //dont remember why i checked shakeamount here but im sure its helpful yessir
        if (shake >= shakeAmount) shakeAmount = shake;
    }
}
