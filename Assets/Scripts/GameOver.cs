using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    LevelManager lm;

    void Start() {
        lm = FindObjectOfType<LevelManager>();
    }

    public void ForFredFucksSake() {  // "what a shitload of fuck"
        lm.LoadMainMenu();
    }
}
