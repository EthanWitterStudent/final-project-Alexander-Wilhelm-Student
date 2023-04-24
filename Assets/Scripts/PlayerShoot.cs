using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] float fireRate;
    [SerializeField] Vector3 offset;
    [SerializeField] AudioClip shootSound;
    float fireDelay;
    public bool shooting;

    AudioPlayer audioPlayer;

    // Start is called before the first frame update
    void Start()
    {
        audioPlayer = FindObjectOfType<AudioPlayer>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Fire();
        fireDelay -= Time.deltaTime;
    }

    void Fire() {                               //GARY WHAT IN THE FLYING FUCK ARE YOU DOING!!!!!!!!
        if (shooting && fireDelay < 0) {            // WHY WOULD YOU MAKE THIS A COROUTINE?????
            Instantiate(projectilePrefab, transform.position+offset, transform.rotation);  //WHY WOULD YOU HANDLE PROJECTILE MOVEMENT LOGIC FROM THE FUCKING SHOOT SCRIPT???
            fireDelay = fireRate;                   // MAKE ANOTHER CLASS FOR THE PROJECTILE!!!!!!!!!
            audioPlayer.PlayClip(shootSound, 1);
        } 
    }

}
