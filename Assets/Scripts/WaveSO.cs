using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Wave Congig", fileName = "New Wave Config")] //lole
public class WaveSO : ScriptableObject
{
    [SerializeField] List<GameObject> enemyPrefabs;
    [SerializeField] Transform pathPrefab;
    [SerializeField] float moveSpeed;    //TODO: move speed per enemy
    [SerializeField] float spawnInterval = 1f;
    [SerializeField] float spawnIntervalVariance = 0f;
    [SerializeField] float minSpawnInterval = 0.2f;
    [SerializeField] float waveDuration = 2f;

    [SerializeField] bool waitToSpawn;

    [SerializeField] float waypointRadius = 0.2f;
    [SerializeField] bool useEnemyRadius;


    public Transform GetStartWaypoint(){
        return pathPrefab.GetChild(0);
    }

    public List<Transform> GetWaypoints(){
        List<Transform> waypoints = new List<Transform>();
        foreach (Transform child in pathPrefab) {
            waypoints.Add(child);
        }
        return waypoints;
    }
    public float GetMoveSpeed() {
        return moveSpeed;
    }

    public int GetEnemyCount() {
        return enemyPrefabs.Count;
    }


    public GameObject GetEnemyPrefab(int x) {
        return enemyPrefabs[x];
    }

    public float GetRandomSpawnInterval() {
        float spawnTime = Random.Range(spawnInterval - spawnIntervalVariance,
                                        spawnInterval + spawnIntervalVariance);
        return Mathf.Clamp(spawnTime, minSpawnInterval, float.MaxValue);
    }

    public bool GetWaitToSpawn() {
        return waitToSpawn;
    }

    public float GetWaveDuration() {
        return waveDuration;
    }

    public float GetWaypointRadius() {
        return waypointRadius;
    }

    public bool GetUseEnemyRadius() {
        return useEnemyRadius;
    }
}
