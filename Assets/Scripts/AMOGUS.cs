using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AMOGUS : MonoBehaviour
{
    [SerializeField] Sprite activeSprite;
    [SerializeField] float activationDelay;
    [SerializeField] AudioClip activationSound;
    [SerializeField] float activationVolume;

    SpriteRenderer sr;
    AudioPlayer audioplayer;
    DamageDealer damageDealer;
    Collider2D col;
    
    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponentInChildren<SpriteRenderer>();
        damageDealer = GetComponent<DamageDealer>();
        col = GetComponent<Collider2D>();

        audioplayer = FindObjectOfType<AudioPlayer>();

        Invoke("Activate", activationDelay);
    }

    void Activate() {
        if (activationSound != null) audioplayer.PlayClip(activationSound, activationVolume);
        sr.sprite = activeSprite;
        sr.sortingLayerName = "towers";
        sr.sortingOrder = 0;
        damageDealer.enabled = true;
        col.enabled = true;
    }
}
