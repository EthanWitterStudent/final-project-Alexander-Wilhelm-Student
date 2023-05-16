using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cube_rotation_script : MonoBehaviour
{
    public float rotateMagnitude;
    Vector3 rotateAmount;

    void Start() {
        rotateAmount = Random.onUnitSphere * rotateMagnitude;
    }
    void Update()
    {
        transform.Rotate(rotateAmount * Time.deltaTime);
    }
}
