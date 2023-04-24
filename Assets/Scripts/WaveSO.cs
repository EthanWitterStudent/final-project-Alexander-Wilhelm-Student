using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Wave Congig", fileName = "New Wave Config")] //lole
public class WaveSO : ScriptableObject
{
    [SerializeField] List<EnemySpawn> enemyPrefabs;

    [SerializeField] float spawnInterval = 1f;
    [SerializeField] float spawnIntervalVariance = 0f;
    [SerializeField] float waveDuration = 2f;

    [SerializeField] bool waitToSpawn;




    public int GetEnemyCount() {
        return enemyPrefabs.Count;
    }


    public EnemySpawn GetEnemySpawn(int x) {
        return enemyPrefabs[x];
    }

    public float GetRandomSpawnInterval() {
        return spawnInterval + Random.Range(0, spawnIntervalVariance);
    }

    public bool GetWaitToSpawn() {
        return waitToSpawn;
    }

    public float GetWaveDuration() {
        return waveDuration;
    }
}
