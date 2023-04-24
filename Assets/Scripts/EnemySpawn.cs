using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public enum SpawnLocation {
    Random, Top, TopMiddle, Middle, BottomMiddle, Bottom
}

[System.Serializable]
public struct EnemySpawn
{
    public GameObject enemy;

    public SpawnLocation spawnLocation;
    
    
}


