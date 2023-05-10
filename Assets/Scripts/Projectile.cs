using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    [SerializeField] float homingFactor;
    [SerializeField] float moveSpeed;

    [SerializeField] float lifetime;

    [SerializeField] AudioClip impactSounds;
    [SerializeField] Color damageColor = Color.white;

    float timeAlive = 0;

    Vector2 angle;
    float radians;

    Rigidbody2D rb;

    DamageDealer damage;
    Collider2D collide;


    // Start is called before the first frame update
    void Start()
    {
        radians = Mathf.Deg2Rad * (transform.rotation.eulerAngles.z);
        angle = new Vector2((float)Mathf.Cos(radians), (float)Mathf.Sin(radians)) * Mathf.Rad2Deg;
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = angle.normalized * moveSpeed;
        damage = GetComponent<DamageDealer>();
        collide = GetComponent<Collider2D>();

        //TODO FOR FUTURE ALEX: make this a global method, headass
        ContactFilter2D filter = new ContactFilter2D(); filter.SetLayerMask(LayerMask.GetMask("ProjEffect")); filter.useTriggers = true;  //get a filter going
        List<Collider2D> results = new List<Collider2D>(); 
        
        
        /*foreach (GameObject gobj in GameObject.FindGameObjectsWithTag("keeves")) {   //stupid hack to find keeves
            gobj.layer = LayerMask.GetMask("Default");
        } */
        if (collide.OverlapCollider(filter, results) > 0)
        {
            foreach (Collider2D col in results)
            {
                Debug.Log(col.gameObject.name);
                if (col.isTrigger && col.gameObject.tag == "keeves")
                {
                    Debug.Log("what");
                    damage.SetDamage(damage.GetDamage() * 2);
                    GetComponentInChildren<SpriteRenderer>().color = damageColor;
                }//keeves damage multiplier

            }
        }
        Debug.Log(results.Count);
        

    }

    // Update is called once per frame
    void Update()
    {

        if (timeAlive > lifetime) Destroy(gameObject);
        timeAlive += Time.deltaTime;
        rb.velocity = angle.normalized * moveSpeed;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

    }
}
