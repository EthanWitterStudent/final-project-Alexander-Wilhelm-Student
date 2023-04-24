using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    [SerializeField] float homingFactor;
    [SerializeField] float moveSpeed;

    [SerializeField] float lifetime;

    float timeAlive = 0;

    Vector2 angle;
    float radians;

    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        radians = Mathf.Deg2Rad*(transform.rotation.eulerAngles.z + 90);
        angle = new Vector2((float)Mathf.Cos(radians), (float)Mathf.Sin(radians)) * Mathf.Rad2Deg;
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = angle.normalized * moveSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (timeAlive > lifetime) Destroy(gameObject);
        timeAlive += Time.deltaTime;
    }
}
