using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverOverPlayer : MonoBehaviour
{
    //EnemySpawner enemyspawn;
    public float moveSpeed;


    Rigidbody2D rb;

    [SerializeField] Vector3 offset;
    [SerializeField] Vector2 randomOffsetRange;
    Vector2 randomOffset;

    [SerializeField] float randomOffsetDelay;
    [SerializeField] float randomOffsetVariance;
    GameObject player;
    void Awake()
    {
        //enemyspawn = FindObjectOfType<EnemySpawner>();
    }
    // Start is called before the first frame update


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = FindObjectOfType<Player>().gameObject;
        StartCoroutine(RandomPathUpdate());
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Move();
    }

    IEnumerator RandomPathUpdate()
    {

        while (true)
        {
            randomOffset = new Vector2(
                Random.Range(-randomOffsetRange.x, randomOffsetRange.x),
                Random.Range(-randomOffsetRange.y, randomOffsetRange.y)
            );
            yield return new WaitForSeconds(randomOffsetDelay + Random.Range(0, randomOffsetVariance));
        }
    }

    void Move()
    {
        Vector2 firstPos;
        Vector3 targetPos;
        Vector2 targetDir;

        firstPos = player.transform.position + offset;

        targetPos = firstPos + randomOffset;


        targetDir = (transform.position - targetPos).normalized;

        //Debug.Log($"FPOS {firstPos}  TPOS {targetPos} RPOS {randomOffset} TDIR {targetDir}");
        rb.AddForce(-targetDir * moveSpeed);

    }

}