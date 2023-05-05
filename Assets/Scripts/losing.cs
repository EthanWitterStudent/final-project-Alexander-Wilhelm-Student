using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class losing : MonoBehaviour
{
    //If an enemy comes in contact with The Forever Box, load Game Over
    
    LevelManager lm; 
    
    private void Start() 
    {
        lm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<LevelManager>(); 
    }
    void OnCollisionEnter2D(Collision2D other) 
    {
        if(other.gameObject.tag == "Enemy")
        {
            Debug.Log("You lose!");
            lm.LoadGameOver();
        }
    }
}
