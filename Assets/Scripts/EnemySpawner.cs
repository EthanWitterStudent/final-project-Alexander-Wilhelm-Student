using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    public List<WaveSO> waveList;
    //[SerializeField] float waveDelay = 0f;
    WaveSO currentWave;
    public bool spawningEnemies;
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
        for (int j = 0; j < wave.GetEnemyCount(); j++)
        {
            GameObject enemy = Instantiate(wave.GetEnemyPrefab(j),
                        wave.GetStartWaypoint().position,
                        Quaternion.identity,
                        transform);
            if (enemy.GetComponent<Pathfinder>() != null) enemy.GetComponent<Pathfinder>().wave = wave;
            if (enemy.GetComponent<HoverOverPlayer>() != null) enemy.GetComponent<HoverOverPlayer>().moveSpeed = wave.GetMoveSpeed();
            yield return new WaitForSeconds(wave.GetRandomSpawnInterval());
        }
    }






}
