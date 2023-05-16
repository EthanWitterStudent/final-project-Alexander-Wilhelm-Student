using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterTime : MonoBehaviour
{
    [SerializeField] float time;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("thing", time);
    }

    // Update is called once per frame
    void thing()
    {
        Destroy(gameObject);
    }
}
