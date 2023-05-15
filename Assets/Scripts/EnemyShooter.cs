using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooter : MonoBehaviour

{

    GameManager gm;
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
        gm = FindObjectOfType<GameManager>();
        audioPlayer = FindObjectOfType<AudioPlayer>();
        StartCoroutine(FireStuff());
    }


    //You may notice that most of this shit is completely unnecessary.
    //This math garbage is carried over from my Laser Defender project.
    //It is fucking expensive. It is fucking hideous to look at.
    //I can't be assed to fix it. Get mad. -AJW

    IEnumerator FireStuff()
    {
        while (true)
        {
            for (int i = 0; i < burstCount; i++)
            {
                if (gm.stagePlaying && (alwaysShoot || (shootTrigger != null && shootTrigger.IsTouchingLayers(LayerMask.GetMask("Enemy")))))
                {
                    for (int j = 0; j < shootCount; j++)                            
                    {
                    
                        float rot = transform.rotation.eulerAngles.z;
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
