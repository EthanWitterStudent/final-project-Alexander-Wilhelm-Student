using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooter : MonoBehaviour

{

    [SerializeField] bool alwaysShoot;
    [SerializeField] Collider2D shootTrigger;

    AudioPlayer audioPlayer;
    [SerializeField] AudioClip shootSound;
    [SerializeField] float shootVolume = 1;

    [SerializeField] GameObject projectilePrefab;
    [SerializeField] float fireRate;
    [SerializeField] float fireRateVariance;
    [SerializeField] Vector3 offset;

    [SerializeField] bool aimAtPlayer;
    [SerializeField] float randomSpread = 0;
    [SerializeField] int shootCount = 1;
    [SerializeField] int burstCount = 1;
    [SerializeField] float burstRate;
    [SerializeField] float burstDelay;
    float fireDelay;

    GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        audioPlayer = FindObjectOfType<AudioPlayer>();
        player = FindObjectOfType<Player>().gameObject;
        StartCoroutine(FireStuff());
    }


    IEnumerator FireStuff()
    {
        while (true)
        {
            for (int i = 0; i < burstCount; i++)
            {
                if (alwaysShoot || (shootTrigger != null && shootTrigger.IsTouchingLayers(LayerMask.GetMask("Player"))))
                {
                    for (int j = 0; j < shootCount; j++)
                    {
                        float rot;

                        if (aimAtPlayer)
                        {
                            //rot = -Vector2.Angle(transform.position, player.transform.position) ; //todo : get player angle

                            Vector3 dir = player.transform.position - transform.position;
                            dir = player.transform.InverseTransformDirection(dir);
                            rot = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90;
                            //Debug.Log(rot);
                        }
                        else rot = transform.rotation.eulerAngles.z + 180;
                        rot += Random.Range(-randomSpread, randomSpread);

                        Quaternion finalAngle = Quaternion.Euler(0, 0, rot);

                        Instantiate(projectilePrefab, transform.position + offset, finalAngle);
                    }
                    audioPlayer.PlayClip(shootSound, shootVolume);
                    yield return new WaitForSeconds(fireRate + Random.Range(0, fireRateVariance));
                }
                else yield return null;
                yield return new WaitForSeconds(burstRate);
            }

            yield return new WaitForSeconds(burstDelay);
        }


    }
}
