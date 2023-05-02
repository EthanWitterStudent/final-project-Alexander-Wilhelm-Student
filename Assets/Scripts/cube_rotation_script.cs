using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cube_rotation_script : MonoBehaviour
{
    public Vector3 rotateAmount;

    void Update()
    {
        transform.Rotate(rotateAmount * Time.deltaTime);
    }
}
