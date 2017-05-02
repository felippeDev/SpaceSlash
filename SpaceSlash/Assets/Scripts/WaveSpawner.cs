using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    public enum SpawnState
    {
        Spawning,
        Waiting,
        Counting
    };

    [System.Serializable]
    public class Wave
    {
        public string name;

        public Transform enemy;

        public int count;

        public float rate;
    }

    public Wave[] waves;

    private int nextWave = 0;

    public float timeBetweenWaves = 5f;

    public float waveCountdown;

    private SpawnState spawnState = SpawnState.Counting;

    private float searchCountdown = 1f;

    private void Start()
    {
        waveCountdown = timeBetweenWaves;
    }

    private void Update()
    {
        if(spawnState == SpawnState.Waiting)
        {
            //Check if wave are cleared
            if (!EnemyIsAlive())
            {
                //Wave cleared
                WaveCleared();
            }
            else
            {
                return;
            }
        }

        if (waveCountdown <= 0)
        {
            if (spawnState != SpawnState.Spawning)
            {
                //Start spawning wave
                StartCoroutine(SpawnWave(waves[nextWave]));
            }
        }
        else
        {
            waveCountdown -= Time.deltaTime;
        }
    }

    void WaveCleared()
    {
        Debug.Log("Wave cleared!");

        spawnState = SpawnState.Counting;

        waveCountdown = timeBetweenWaves;

        if(nextWave + 1 > waves.Length - 1)
        {
            nextWave = 0;
            Debug.Log("Stage Cleared!");
        }
        else
        {
            nextWave++;
        }
    }

    bool EnemyIsAlive()
    {
        searchCountdown -= Time.deltaTime;

        if(searchCountdown <= 0f)
        {
            searchCountdown = 1f;

            if(GameObject.FindGameObjectWithTag("EnemyShipTag") == null)
            {
                return false;
            }
        }
        return true;
    }

    IEnumerator SpawnWave(Wave wave)
    {
        Debug.Log("Spawning wave: " + wave.name);

        spawnState = SpawnState.Spawning;

        //Spawn
        for (int i = 0; i < wave.count; i++)
        {
            SpawnEnemy(wave.enemy);
            yield return new WaitForSeconds(1f / wave.rate);
        }

        spawnState = SpawnState.Waiting;

        yield break;
    }

    void SpawnEnemy(Transform enemy)
    {
        //Spawn an enemy
        Instantiate(enemy, transform.position, transform.rotation);

        Debug.Log("Spawning Enemy: " + enemy.name);
    }
}
