using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    public List<WaveSO> waveList;
    //[SerializeField] float waveDelay = 0f;
    WaveSO currentWave;
    public bool spawningEnemies;

    public Transform[] spawnPoints;
    // START MORE LIKE DOO DOO FART HAHA!
    void Start()
    {
        //StartCoroutine(SpawnWaves());
    }


    public IEnumerator SpawnWaves()
    {
        spawningEnemies = true;

            for (int i = 0; i < waveList.Count; i++)
            {
                currentWave = waveList[i];
                if (currentWave.GetWaitToSpawn())
                { //wait until all enemies are gone before spawning the next wave
                    while (GameObject.FindGameObjectsWithTag("Enemy").Length > 0)
                    {
                        yield return null;
                    }
                }
                StartCoroutine(SpawnEnemies(currentWave));

                yield return new WaitForSeconds(currentWave.GetWaveDuration());
            }
        

        spawningEnemies = false;

    }

    IEnumerator SpawnEnemies(WaveSO wave)
    {
        Vector3 spawnloc;
        for (int j = 0; j < wave.GetEnemyCount(); j++)
        {
            EnemySpawn currentspawn = wave.GetEnemySpawn(j);

            switch (currentspawn.spawnLocation) {
                case SpawnLocation.Random:
                    spawnloc = spawnPoints[Random.Range(0, spawnPoints.Length)].position;
                    break;
                case SpawnLocation.Top:
                    spawnloc = spawnPoints[0].position;
                    break;
                case SpawnLocation.TopMiddle:
                    spawnloc = spawnPoints[1].position;
                    break;
                case SpawnLocation.Middle:
                    spawnloc = spawnPoints[2].position;
                    break;
                case SpawnLocation.BottomMiddle:
                    spawnloc = spawnPoints[3].position;
                    break;
                case SpawnLocation.Bottom:
                    spawnloc = spawnPoints[4].position;
                    break;
                default:
                    spawnloc = spawnPoints[Random.Range(0, spawnPoints.Length)].position;
                    break;
            }

            GameObject enemy = Instantiate(wave.GetEnemySpawn(j).enemy,
                        spawnloc,
                        Quaternion.identity,
                        transform);
            yield return new WaitForSeconds(wave.GetRandomSpawnInterval());
        }
    }






}
