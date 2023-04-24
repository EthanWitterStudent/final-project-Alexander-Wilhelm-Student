using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGScroll : MonoBehaviour
{

    [SerializeField] Vector2 offsetRate;

    Material material;
    //  fart is called
    void Start()
    {
        material=GetComponent<SpriteRenderer>().material;
    }

    // Update is c
    void Update()
    {
        material.mainTextureOffset += offsetRate * Time.deltaTime;
    }
}
