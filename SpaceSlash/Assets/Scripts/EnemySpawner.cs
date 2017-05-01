using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject EnemyGO;
    float maxSpawnRateInSeconds = 5f;

    void Start()
    {
        StartEnemySpawner();
    }

    void SpawnEnemy()
    {
        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));

        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

        GameObject enemy = (GameObject)Instantiate(EnemyGO);

        enemy.transform.position = new Vector2(UnityEngine.Random.Range(min.x, max.x), max.y); ;

        ScheduleNextEnemySpawn();
    }

    void ScheduleNextEnemySpawn()
    {
        float spawnInSeconds;

        if(maxSpawnRateInSeconds > 1f)
        {
            spawnInSeconds = UnityEngine.Random.Range(1f, maxSpawnRateInSeconds); ;
        }
        else
        {
            spawnInSeconds = 1f;
        }

        Invoke("SpawnEnemy", spawnInSeconds);
    }

    void IncreaseSpawnRate()
    {
        Debug.Log("Increasing dificulty");

        if (maxSpawnRateInSeconds > 1f)
        {
            maxSpawnRateInSeconds--;
        }

        if(maxSpawnRateInSeconds == 1f)
        {
            Debug.Log("Freeze dificulty");

            CancelInvoke("IncreaseSpawnRate");
        }
    }

    public void StartEnemySpawner()
    {
        Debug.Log("Spawn Started");

        maxSpawnRateInSeconds = 5f;

        Invoke("SpawnEnemy", maxSpawnRateInSeconds);

        InvokeRepeating("IncreaseSpawnRate", 0f, 30f);
    }

    public void StopEnemySpawner()
    {
        Debug.Log("Spawn Stoped");

        CancelInvoke("SpawnEnemy");
        CancelInvoke("IncreaseSpawnRate");
    }
}
