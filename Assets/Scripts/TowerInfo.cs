using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerInfo : MonoBehaviour //this is what transitions the tower selection to the game
{
    public List<GameObject> towers;
    public List<Sprite> towerImgs; //for ui later
    
    static TowerInfo instance;
    void Start() { //epic singleton pattern
        if(instance != null)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }  
}
